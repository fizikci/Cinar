using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Cinar.DBTools.Controls
{
    class MyTabControl : TabControl
    {
        public MyTabControl()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        Pen penPassive = new Pen(Brushes.Gray, 2);
        Pen penActive = new Pen(Brushes.Crimson, 2);

        protected override void OnPaint(PaintEventArgs pevent)
        {
            this.SetStyle(ControlStyles.UserPaint, false);
            base.OnPaint(pevent);
            Rectangle o = pevent.ClipRectangle;
            Graphics.FromImage(buffer).Clear(SystemColors.Control);
            if (o.Width > 0 && o.Height > 0)
                DrawToBitmap(buffer, new Rectangle(0, 0, Width, o.Height));
            pevent.Graphics.DrawImageUnscaled(buffer, 0, 0);
            for (int i = 0; i < this.TabPages.Count; i++)
            {
                //pevent.Graphics.DrawImageUnscaled(FamFamFam.cross, getCloseButtonRect(i).Location);
                Rectangle rect = getCloseButtonRect(i);
                Pen p = this.SelectedIndex == i ? penActive : penPassive;
                pevent.Graphics.DrawLine(p, rect.Location, rect.Location + rect.Size);
                pevent.Graphics.DrawLine(p, rect.Location + new Size(rect.Size.Width, 0), rect.Location + new Size(0, rect.Size.Height));
            }
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        private Rectangle getCloseButtonRect(int tabPageIndex)
        {
            int a = 8;
            Rectangle rect = GetTabRect(tabPageIndex);
            return new Rectangle(new Point(rect.Left + rect.Width - a - 7, rect.Top + (rect.Height - a) / 2), new Size(a, a));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            buffer = new Bitmap(Width, Height);
        }
        private Bitmap buffer;

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (this.SelectedIndex < 0)
                return;

            Rectangle rect = getCloseButtonRect(this.SelectedIndex);
            if (rect.Contains(e.Location))
            {
                CancelEventArgs arg = new CancelEventArgs(false);
                if (CloseTab != null)
                    CloseTab(this, arg);
                if (!arg.Cancel && this.SelectedIndex>-1)
                    TabPages.RemoveAt(this.SelectedIndex);
            }
        }
        public event EventHandler<CancelEventArgs> CloseTab;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Cursor c = Cursors.Default;
            for (int i = 0; i < this.TabPages.Count; i++)
                if (getCloseButtonRect(i).Contains(e.Location))
                    c = Cursors.Hand;

            if (c != this.Cursor)
                this.Cursor = c;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            Cursor = Cursors.Default;
        }
        protected override void OnControlAdded(ControlEventArgs e)
        {
            this.Visible = TabPages.Count > 0;
        }
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            this.Visible = TabPages.Count-1 > 0;
        }
    }
    public class MyTabPage : TabPage
    {
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if(!string.IsNullOrEmpty(value))
                    value = value.Trim() + "    ";

                base.Text = value;
            }
        }
    }
}
