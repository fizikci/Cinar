using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Reflection;

namespace Cinar.TemplateDesign
{
    public partial class PageDesigner : UserControl
    {
        public PageDesigner()
        {
            InitializeComponent();
        }

        public string ClickToAdd { get; set; }

        public Action OnElementAdded = null;
        public Action<Element> OnSelectedElementChanged = null;
        public Action<Element> OnSelectedElementMoved = null;
        public Action<Element> OnSelectedElementResized = null;

        private Element selectedElement = null;
        public Element SelectedElement 
        {
            get
            {
                return selectedElement;
            }
            set
            {
                if (selectedElement != value && OnSelectedElementChanged != null)
                    OnSelectedElementChanged(value);
                selectedElement = value;
                foreach (Element elm in this.page.Elements)
                    elm.Selected = false;
                if (selectedElement != null)
                {
                    selectedElement.Selected = true;
                    showHandles();
                }
                else
                {
                    hideHandles();
                }
            }
        }

        private Point toRealPoint(Point point)
        {
            return new Point(Convert.ToInt32((float)point.X / this.page.ScaleFactor), Convert.ToInt32((float)point.Y / this.page.ScaleFactor));
        }
        private Size toRealSize(Size size)
        {
            return new Size(Convert.ToInt32((float)size.Width / this.page.ScaleFactor), Convert.ToInt32((float)size.Height / this.page.ScaleFactor));
        }

        private Point toScaledPoint(Point point)
        {
            return new Point(Convert.ToInt32((float)point.X * this.page.ScaleFactor), Convert.ToInt32((float)point.Y * this.page.ScaleFactor));
        }
        private Size toScaledSize(Size size)
        {
            return new Size(Convert.ToInt32((float)size.Width * this.page.ScaleFactor), Convert.ToInt32((float)size.Height * this.page.ScaleFactor));
        }

        #region handles
        private void showHandles()
        {
            handle.Location = selectedElement.Location + new Size(selectedElement.Width, selectedElement.Height);
            handle.Visible = true;
        }
        private void hideHandles()
        {
            handle.Visible = false;
        }
        
        private bool handleDragging = false;
        private Point handleStartPoint;

        private void handle_MouseDown(object sender, MouseEventArgs e)
        {
            if (setAsActivePage())
                return;

            handleDragging = true;
            handleStartPoint = toScaledPoint( this.SelectedElement.Location );
        }

        private bool setAsActivePage()
        {
            TemplateDesigner td = this.Parent.Parent.Parent as TemplateDesigner;
            if (td.ActivePage == this)
                return false;

            td.ActivePage = this;
            return true;
        }

        private void handle_MouseMove(object sender, MouseEventArgs e)
        {
            if (handleDragging)
            {
                Point posToScreen = this.PointToScreen(new Point(0,0));
                Point mp = MousePosition - new Size(posToScreen.X, posToScreen.Y);
                handle.Location = new Point(mp.X - handle.Width / 2, mp.Y - handle.Height / 2);

                Element elm = this.SelectedElement;
                int dx = mp.X - handleStartPoint.X;
                int dy = mp.Y - handleStartPoint.Y;
                elm.Location = toRealPoint( new Point(mp.X - (dx < 0 ? 0 : dx), mp.Y - (dy < 0 ? 0 : dy)) );
                elm.Size = toRealSize( new Size(Convert.ToInt32(Math.Abs(dx)), Convert.ToInt32(Math.Abs(dy))) );
                if (elm is Line) (elm as Line).Up = dx * dy < 0;
                this.refreshNeeded = true;
            }
        }

        private void handle_MouseUp(object sender, MouseEventArgs e)
        {
            if (handleDragging && OnSelectedElementResized != null)
                this.OnSelectedElementResized(this.SelectedElement);

            handleDragging = false;
            PutHandleToTheCornerOfSelectedElement();
        }
        #endregion

        public Page page = new Page();

        #region Control adding and dragging
        private bool adding = false;
        private Point startPoint;

        private bool dragging = false;
        private Size delta;

        private void PageDesigner_MouseDown(object sender, MouseEventArgs e)
        {
            if (setAsActivePage())
                return;

            if (ClickToAdd != "None")
            {
                try
                {
                    Element elm = null;
                    try
                    {
                        elm = (Element)Activator.CreateInstance(null, "Cinar.TemplateDesign." + ClickToAdd).Unwrap();
                    }
                    catch {
                        elm = (Element)Type.GetType(ClickToAdd).GetConstructor(Type.EmptyTypes).Invoke(null);
                    }
                    elm.Location = toRealPoint( e.Location );
                    startPoint = e.Location;
                    this.page.Elements.Add(elm);
                    elm.ZIndex = this.page.Elements.Count-1;
                    this.SelectedElement = elm;
                    adding = true;
                }
                catch
                {
                    adding = false;
                    ClickToAdd = "None";
                    if (OnElementAdded != null)
                        OnElementAdded();
                }
            }
            else
            {
                Element elm = this.page.HitTest(toRealPoint( e.Location ));
                if (elm != null)
                {
                    Point elmScaledLoc = toScaledPoint(elm.Location);
                    delta = new Size(e.X - elmScaledLoc.X, e.Y - elmScaledLoc.Y);
                    dragging = true;
                    this.SelectedElement = elm;
                    this.refreshNeeded = true;
                }
            }
        }

        private void PageDesigner_MouseMove(object sender, MouseEventArgs e)
        {
            if (ClickToAdd != "None" && adding)
            {
                Element elm = this.SelectedElement;
                int dx = e.Location.X - startPoint.X;
                int dy = e.Location.Y - startPoint.Y;
                elm.Location = toRealPoint( new Point(e.Location.X - (dx < 0 ? 0 : dx), e.Location.Y - (dy < 0 ? 0 : dy)) );
                elm.Size = toRealSize( new Size(Convert.ToInt32(Math.Abs(dx)), Convert.ToInt32(Math.Abs(dy))) );
                if (elm is Line) (elm as Line).Up = dx * dy < 0;
                PutHandleToTheCornerOfSelectedElement();
                this.refreshNeeded = true;
            }

            if (dragging)
            {
                this.SelectedElement.Location = toRealPoint( e.Location - delta );
                PutHandleToTheCornerOfSelectedElement();
                this.refreshNeeded = true;
            }

        }

        public void PutHandleToTheCornerOfSelectedElement()
        {
            if(this.SelectedElement!=null)
                handle.Location = toScaledPoint( this.SelectedElement.Location + this.SelectedElement.Size);
        }

        private void PageDesigner_MouseUp(object sender, MouseEventArgs e)
        {
            if (ClickToAdd != "None" && adding)
            {
                Element elm = this.SelectedElement;
                if (elm.Width < 10)
                    elm.Width = 10;
                if (elm.Height < 10)
                    elm.Height = 10;

                adding = false;
                ClickToAdd = "None";
                if (OnElementAdded != null)
                    OnElementAdded();
                this.refreshNeeded = true;

                if(OnSelectedElementResized != null)
                    this.OnSelectedElementResized(elm);
            }

            if (dragging && OnSelectedElementMoved != null)
                this.OnSelectedElementMoved(this.SelectedElement);

            dragging = false;
        } 
        #endregion

        private void PageDesigner_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Delete:
                    deleteSelectedElement();
                    break;
            }
        }

        private void deleteSelectedElement()
        {
            if (this.SelectedElement != null)
            {
                this.page.Elements.Remove(this.SelectedElement);
                if (this.page.Elements.Count > 0)
                    this.SelectedElement = this.page.Elements[this.page.Elements.Count - 1];
                else
                    handle.Visible = false;
            }
            this.refreshNeeded = true;
        }

        #region Painting
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            this.page.Draw(e.Graphics);
        }

        private bool refreshNeeded;
        public void DoRefresh()
        {
            if (refreshNeeded)
            {
                this.Refresh();
                refreshNeeded = false;
                modified = true;
            }
        }
        #endregion

        private bool modified;
        public bool Modified
        {
            get
            {
                return modified;
            }
            set 
            {
                modified = value;
            }
        }

        #region send back, bring to front
        public void SendSelectedBack()
        {
            if (this.SelectedElement == null)
                return;

            page.SendBack(this.SelectedElement);

            this.refreshNeeded = true;
        }

        public void BringSelectedToFront()
        {
            if (this.SelectedElement == null)
                return;

            page.BringToFront(this.SelectedElement);

            this.refreshNeeded = true;
        }
        #endregion
    }

    //public enum ControlTypes
    //{
    //    None,
    //    Text,
    //    Picture,
    //    Line,
    //    Rectangle,
    //    Circle
    //}
}
