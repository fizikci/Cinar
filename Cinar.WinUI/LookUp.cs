using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Cinar.Entities;
using Cinar.Database;

namespace Cinar.WinUI
{
    public class LookUp : ButtonEdit
    {
        public LookUp()
        {
            this.DisplayFieldName = "Name";
            this.ValueFieldName = "Id";
        }

        private bool hasEllipsisButton()
        {
            if (Properties==null || Properties.Buttons == null || Properties.Buttons.Count == 0)
                return false;

            foreach (EditorButton btn in Properties.Buttons)
                if(btn.Kind== ButtonPredefines.Ellipsis)
                    return true;
            return false;
        }
        [Category("Cinar"), DefaultValue(true)]
        public bool ShowEllipsisButton
        {
            get
            {
                return hasEllipsisButton();
            }
            set
            {
                if (value)
                {
                    if (!hasEllipsisButton())
                        Properties.Buttons.Add(new EditorButton(ButtonPredefines.Ellipsis));
                }
                else
                {
                    if (hasEllipsisButton())
                    {
                        EditorButton btn = Properties.Buttons.OfType<EditorButton>().First(eb => eb.Kind == ButtonPredefines.Ellipsis);
                        Properties.Buttons.RemoveAt(Properties.Buttons.IndexOf(btn));
                    }
                }
            }
        }

        private bool hasComboButton()
        {
            if (Properties==null || Properties.Buttons == null || Properties.Buttons.Count == 0)
                return false;

            foreach (EditorButton btn in Properties.Buttons)
                if (btn.Kind == ButtonPredefines.Combo)
                    return true;
            return false;
        }
        [Category("Cinar"), DefaultValue(false)]
        public bool ShowComboButton
        {
            get
            {
                return hasComboButton();
            }
            set
            {
                if (value)
                {
                    if (!hasComboButton())
                        Properties.Buttons.Add(new EditorButton(ButtonPredefines.Combo));
                }
                else
                {
                    if (hasComboButton())
                    {
                        EditorButton btn = Properties.Buttons.OfType<EditorButton>().First(eb => eb.Kind == ButtonPredefines.Combo);
                        Properties.Buttons.RemoveAt(Properties.Buttons.IndexOf(btn));
                    }
                }
            }
        }

        [Category("Cinar")]
        public Type EntityType { get; set; }
        [Category("Cinar"), DefaultValue("Name")]
        public string DisplayFieldName { get; set; }
        [Category("Cinar"), DefaultValue("Id")]
        public string ValueFieldName { get; set; }

        protected override void OnClickButton(DevExpress.XtraEditors.Drawing.EditorButtonObjectInfoArgs buttonInfo)
        {
            base.OnClickButton(buttonInfo);

            if (buttonInfo.Button.Kind == ButtonPredefines.Ellipsis)
            {

                FormEntity f = DMT.Provider.UIMetaData.CreateFormFor(this.EntityType, this.SelectedItem as BaseEntity);
                if(f==null)
                {
                    DMT.Provider.FeedBack(this.EntityType.Name + " isimli entity için form hazýrlanmamýþ");
                    return;
                }

                if (f.ShowDialog() == DialogResult.OK)
                    this.SelectedItem = f.EditControl.CurrentEntity;
            }
            else if (buttonInfo.Button.Kind == ButtonPredefines.Combo)
            {
                showPopup(true);
            }
        }

        private object selectedItem;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem 
        {
            get {
                return selectedItem;
            }
            set
            {
                object oldVal = selectedItem;
                EditValue = selectedItem = value;
                if(oldVal!=selectedItem && itemSelected!=null)
                    itemSelected(this, EventArgs.Empty);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BaseEntity SelectedEntity 
        {
            get { return SelectedItem as BaseEntity; }
            set { SelectedItem = value; }
        }

        [Browsable(false)]
        public FilterExpression Filter { get; set; }      

        private bool textChanged = false;
        protected override void OnTextChanged(EventArgs e)
        {
            textChanged = true;
            base.OnTextChanged(e);
        }

        [Category("Cinar"), DefaultValue(0)]
        public int PopupAfterNChars { get; set; }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && lbc != null)
            {
                lbc.Focus();
                lbc.SelectedIndex = 0;
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (this.Text != null && textChanged && this.Text.Length > PopupAfterNChars)
                showPopup();
            textChanged = false;
        }

        ListBoxControl lbc = null;
        private void showPopup()
        {
            showPopup(false);
        }

        public Func<string,IList<BaseEntity>> GetDataSource;
        

        private void showPopup(bool showAll)
        {
            Form form = this.FindForm();
            if (form == null)
                return;

            buildFilterExp(showAll, false);

            IList<BaseEntity> items = null;

            if (GetDataSource != null)
            {
                items = GetDataSource(this.Text);
            }
            else
            {
                Filter.PageNo = 0; Filter.PageSize = 0;
                items = DMT.Provider.Db.ReadList(EntityType, Filter).Cast<BaseEntity>().ToList();
                if (items == null || items.Count == 0)
                {
                    buildFilterExp(showAll, true);
                    Filter.PageSize = 0; Filter.PageNo = 0;
                    items = DMT.Provider.Db.ReadList(EntityType, Filter).Cast<BaseEntity>().ToList();
                }
            }

            if (items.Count == 0)
            {
                if (lbc != null)
                {
                    form.Controls.Remove(lbc);
                    lbc = null;
                }
                return;
            }

            form.SuspendLayout();
            if (lbc == null)
            {
                lbc = new ListBoxControl();
                form.Controls.Add(lbc);
                lbc.KeyUp += (sender, e) =>
                                 {
                                     if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
                                     {
                                         if (e.KeyCode == Keys.Enter)
                                             SelectedItem = lbc.SelectedItem;

                                         form.Controls.Remove(lbc);
                                         lbc = null;
                                         this.Focus();
                                     }
                                 };
                lbc.DoubleClick += delegate
                {
                    SelectedItem = lbc.SelectedItem;

                    form.Controls.Remove(lbc);
                    lbc = null;
                    this.Focus();
                };

                lbc.LostFocus += delegate
                {
                    if (lbc.SelectedItem != null)
                        SelectedItem = lbc.SelectedItem;
                    else
                        this.Text = "";

                    form.Controls.Remove(lbc);
                    lbc = null;
                };

                lbc.DrawItem += lbc_DrawItem;
                lbc.BringToFront();
            }

            lbc.Width = this.Width;
            lbc.Location = form.PointToClient(this.PointToScreen(new Point(0, this.Height)));
            if (lbc.Location.Y + lbc.Height > form.Height)
                lbc.Location = new Point(lbc.Location.X, lbc.Location.Y - (lbc.Height + this.Height));


            lbc.DataSource = items;
            lbc.SelectedIndex = -1;

            form.ResumeLayout();
        }

        private void buildFilterExp(bool showAll, bool likeMeansContaining)
        {
            if(Filter==null)
                Filter = new FilterExpression();

            string searchStr = null;
            if(this.Text.Length>3 || likeMeansContaining)
                searchStr = "%" + this.Text + "%";
            else
                searchStr = this.Text + "%";

            if (DependsOn != null && DependsOn.EditValue != null && DependsOn.EditValue is BaseEntity)
            {
                if (Filter[dependentFieldName] == null)
                {
                    Filter.Criterias.Add(new Criteria(dependentFieldName, CriteriaTypes.Eq, ((BaseEntity)DependsOn.EditValue).Id));
                }
                else
                    Filter[dependentFieldName].FieldValue = ((BaseEntity)DependsOn.EditValue).Id.ToString();
            }

            if (!showAll)
            {
                if (Filter[DisplayFieldName] == null)
                    Filter.Criterias.Add(new Criteria(DisplayFieldName, CriteriaTypes.Like, searchStr));
                Filter[DisplayFieldName].FieldValue = searchStr;
                Filter[DisplayFieldName].CriteriaType = CriteriaTypes.Like;
            }
            Filter.Orders = new OrderList
                                {
                                    new Order(DisplayFieldName, true)
                                };
        }

        void lbc_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            if (DrawItem != null)
                DrawItem(e);
            else
                e.Appearance.ForeColor = (e.Item as BaseEntity).Deleted ? Color.Gray : Color.Black;

        }

        public Action<ListBoxDrawItemEventArgs> DrawItem;

        private event EventHandler itemSelected;
        public event EventHandler ItemSelected
        {
            add { itemSelected += value; }
            remove { itemSelected -= value; }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            Form form = this.FindForm();
            if (form == null)
                return;

            if (lbc != null && !lbc.Focused)
            {
                form.Controls.Remove(lbc);
                lbc = null;
            }
        }

        [Category("Cinar")]
        public LookUp DependsOn { get; set; }

        private string dependentFieldName;
        [Category("Cinar")]
        public string DependentFieldName
        {
            get
            {
                return dependentFieldName;
            }
            set
            {
                dependentFieldName = value;
                
                if(Site!=null && Site.DesignMode)
                    return;

                if (!DesignMode && DependsOn != null && DependsOn.EditValue != null)
                {
                    if (Filter[dependentFieldName] == null)
                    {
                        Filter.Criterias.Add(new Criteria(dependentFieldName, CriteriaTypes.Eq, ((BaseEntity)DependsOn.EditValue).Id));
                    }
                    else
                        Filter[dependentFieldName].FieldValue = ((BaseEntity)DependsOn.EditValue).Id.ToString();
                }
            }
        }

        protected override void OnEditValueChanged()
        {
            base.OnEditValueChanged();

            Form f = this.FindForm();
            if (f == null) return;

            foreach (Control item in f.Controls)
            {
                if (item is LookUp && (item as LookUp).DependsOn == this)
                    (item as LookUp).EditValue = null;
            }
        }
    }
}