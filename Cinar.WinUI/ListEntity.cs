using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Cinar.UICommands;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Cinar.Database;
using Cinar.Entities.Standart;

namespace Cinar.WinUI
{
    public partial class ListEntity : XtraForm, ICinarForm
    {
        public ServiceProvider Provider
        {
            get
            {
                return DMT.Provider;
            }
        }

        public IEntityEditControl EntityEditControl;

        public ListEntity()
        {
            InitializeComponent();
            cbPageSize.EditValue = PageSize = 30;
            splitContainer.Horizontal = DMT.Provider.ListEntityViewHorizontal;
        }

        public ListEntity(IEntityEditControl entityControl)
        {
            InitializeComponent();
            cbPageSize.EditValue = PageSize = 30;
            entityControl.ListForm = this;
            this.EntityEditControl = entityControl;
            splitContainer.Horizontal = DMT.Provider.ListEntityViewHorizontal;
        }

        FilterExpression fExp = null;
        private void buildFilterControls(CommandManager cmdMan)
        {
            fExp = EntityEditControl.GetFilter();
            if (fExp == null)
            {
                removeLayoutFilter();
                return;
            }

            int panelWidth = panelHeader.Width < 250 ? panelHeader.Width : 250;
            int panelHeight = 20;
            int labelWidth = 70;

            this.SuspendLayout();
            List<CommandTrigger> triggers = new List<CommandTrigger>();
            foreach (Criteria criteria in fExp.Criterias)
            {
                BaseEdit c = (BaseEdit)EntityEditControl.GetControl(criteria.FieldName);
                if (c == null)
                    continue;

                BaseEdit control = (BaseEdit)Activator.CreateInstance(c.GetType());
                foreach (PropertyInfo pi in c.GetType().GetProperties())
                    if ((pi.PropertyType.IsValueType || pi.PropertyType == typeof(string)) && pi.GetSetMethod()!=null)
                        pi.SetValue(control, pi.GetValue(c, null), null);

                PropertyInfo piControl = EntityEditControl.GetEntityType().GetProperty(criteria.FieldName);
                switch (control.GetType().Name)
                {
                    case "CheckEdit":
                        DMT.Provider.SetValueOfEditControl(piControl, control, criteria.FieldValue);
                        (control as CheckEdit).Properties.AllowGrayed = true;
                        break;
                    case "TextEdit":
                        control.EditValue = criteria.FieldValue;
                        break;
                    case "SpinEdit":
                        control.EditValue = criteria.FieldValue;
                        break;
                    case "DateEdit":
                        control.EditValue = criteria.FieldValue;
                        break;
                    case "ComboBoxEdit":
                        (control as ComboBoxEdit).Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                        (control as ComboBoxEdit).Properties.Items.Clear();
                        (control as ComboBoxEdit).Properties.Items.Add("TÜMÜ");
                        (control as ComboBoxEdit).Properties.Items.AddRange((c as ComboBoxEdit).Properties.Items);
                        control.EditValue = criteria.FieldValue;
                        break;
                    case "LookUp":
                        if (piControl.PropertyType == typeof(string))
                        {
                            control = new TextEdit();
                            control.Name = "edit" + piControl.Name;
                            control.Text = criteria.FieldValue.ToString();
                        }
                        break;
                }

                if (control.GetType().Name != "CheckEdit")
                {
                    Panel p = new Panel();
                    p.Size = new Size(panelWidth, panelHeight);
                    Label lbl = new Label();
                    lbl.Size = new Size(labelWidth, panelHeight);
                    lbl.Location = new Point(0, 0);
                    lbl.Text = EntityEditControl.GetControlLabel(criteria.FieldName);
                    lbl.TextAlign = ContentAlignment.MiddleLeft;
                    p.Controls.Add(lbl);

                    control.Size = new Size(panelWidth - labelWidth, panelHeight);
                    control.Location = new Point(labelWidth, 0);
                    p.Controls.Add(control);
                    panelHeader.Controls.Add(p);
                }
                else
                {
                    panelHeader.Controls.Add(control);
                }

                triggers.Add(new CommandTrigger { 
                    Control = control,
                    Event = "EditValueChanged"
                });
            }

            if (panelHeader.Controls.Count == 0)
            {
                removeLayoutFilter();
                return;
            }

            this.ResumeLayout();

            cmdMan.Commands.Add(new Command
                                    {
                                        Execute = arg => { pageNo = 0; BindGrid(); showEntity(); },
                                        Triggers = triggers
                                    });
        }

        private void removeLayoutFilter()
        {
            int height = panelHeader.Height;
            splitContainer.Panel1.Controls.Remove(panelHeader);
            grid.Height += height;
            grid.Top -= height;
        }

        public void Initialize(CommandManager cmdMan)
        {
            EntityEditControl.Initialize(cmdMan);
            buildFilterControls(cmdMan);
            this.gridView.FocusedRowChanged += this.gridView_FocusedRowChanged;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Control editControl = (Control)EntityEditControl;
            splitContainer.Panel2.Controls.Add(editControl);
            editControl.Top = 0;
            editControl.Width = splitContainer.Panel2.Width - splitContainer.Panel2.Margin.Left - splitContainer.Panel2.Margin.Right;
            editControl.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            BindGrid();
            showEntity();

            pnlTitle.BackColor = DMT.SkinColors[this.LookAndFeel.ActiveSkinName];

            LookAndFeel.StyleChanged += delegate {
                pnlTitle.BackColor = DMT.SkinColors[this.LookAndFeel.ActiveSkinName];
            };
        }

        private int pageNo = 0;
        [Category("Cinar")]
        public int PageSize { get; set; }

        private bool gridColumnsPopulated = false;

        public void BindGrid()
        {
            List<ColumnDefinition> listCols = EntityEditControl.GetVisibleColumns();
            if (!gridColumnsPopulated)
            {
                DMT.Provider.PopulateGridColumns(listCols, gridView);
                gridColumnsPopulated = true;
            }

            FilterExpression fExp2 = getFilter(false);
            IList list = (IList)EntityEditControl.GetEntityList(fExp2, pageNo, PageSize);
            if (list == null || list.Count == 0)
            {
                fExp2 = getFilter(true);
                list = (IList)EntityEditControl.GetEntityList(fExp2, pageNo, PageSize);
            }

            this.gridView.FocusedRowChanged -= this.gridView_FocusedRowChanged;
            grid.DataSource = list;
            this.gridView.FocusedRowChanged += this.gridView_FocusedRowChanged;

        }

        private FilterExpression getFilter(bool likeMeansContaining)
        {
            FilterExpression fExp2 = new FilterExpression();
            if (fExp != null)
            {
                fExp2.Orders = fExp.Orders;
                foreach (Criteria cr in fExp.Criterias)
                {
                    Control[] controls = panelHeader.Controls.Find("edit" + cr.FieldName, true);
                    if (controls.Length == 0)
                        continue;

                    BaseEdit control = (BaseEdit)controls.First();
                    PropertyInfo piControl = EntityEditControl.GetEntityType().GetProperty(cr.FieldName);
                    object val = DMT.Provider.GetValueOfEditControl(piControl, control);

                    if (control is ComboBoxEdit && val.Equals("TÜMÜ"))
                        continue;
                    else if (control is CheckEdit && (control as CheckEdit).CheckState == CheckState.Indeterminate)
                        continue;

                    if (cr.CriteriaType == CriteriaTypes.Like)
                    {
                        string search = val==null ? "" : val.ToString();
                        if (!search.Contains("%"))
                            val = ((search.Length > 3 || likeMeansContaining) ? "%" : "") + search + "%";
                    }

                    //cr.FieldValue = val.ToString();
                    fExp2.Criterias.Add(new Criteria(cr.FieldName, cr.CriteriaType, val.ToString()));
                }
            }
            return fExp2;
        }


        private void gridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            showEntity();
        }

        private void showEntity()
        {
            BaseEntity entity = (BaseEntity)gridView.GetFocusedRow();
            if (entity != null)
            {
                EntityEditControl.ShowEntity(entity);
                setTitle();
            }
        }

        private void setTitle()
        {
            lblTitle.Text = EntityEditControl.GetTitle();
        }

        public void FocusCurrentEntity()
        {
            IList list = gridView.DataSource as IList;
            if (EntityEditControl.CurrentEntity == null || list==null)
                return;
            int index = list.OfType<BaseEntity>().IndexOf(e => e.Id == EntityEditControl.CurrentEntity.Id);
            int rowHandle = gridView.GetRowHandle(index);
            if(rowHandle!=gridView.FocusedRowHandle)
                gridView.FocusedRowHandle = rowHandle;
            //else
            //    showEntity();
        }

        public void FocusFirstEntity()
        {
            int rowHandle = gridView.GetRowHandle(0);
            if (rowHandle != gridView.FocusedRowHandle)
                gridView.FocusedRowHandle = gridView.GetRowHandle(0);
            else
                showEntity();
        }

        public bool Horizontal
        {
            get { return splitContainer.Horizontal; }
            set { splitContainer.Horizontal = DMT.Provider.ListEntityViewHorizontal = value; }
        }

        public void ChangeView()
        {
            Horizontal = !Horizontal;
        }

        public CommandCollection GetCommands()
        {
            CommandCollection cc = EntityEditControl.GetCommands();
            if (cc == null) cc = new CommandCollection();

            cc.AddRange(new CommandCollection { 
                 new Command(){
                                  Execute = cmdNextPage,
                                  Trigger = new CommandTrigger(){Control=btnNext},
                                  IsEnabled = () => gridView.RowCount == PageSize
                              },
                 new Command(){
                                  Execute = cmdPrevPage,
                                  Trigger = new CommandTrigger(){Control=btnPrev},
                                  IsEnabled = () => pageNo>0
                              },
                 new Command(){
                                  Execute = cmdChangePageSize,
                                  Trigger = new CommandTrigger(){Control=cbPageSize, Event="SelectedIndexChanged"},
                              },
            });

            return cc;
        }

        private void cmdNextPage(string arg)
        {
            pageNo++;
            BindGrid();
            showEntity();
        }
        private void cmdPrevPage(string arg)
        {
            pageNo--;
            BindGrid();
            showEntity();
        }

        private void cmdChangePageSize(string arg)
        {
            int pageSize = this.PageSize;
            bool changed = int.TryParse(cbPageSize.SelectedItem.ToString(), out pageSize);
            if (changed && pageSize != PageSize)
            {
                this.pageNo = 0;
                this.PageSize = pageSize;
                this.BindGrid();
                showEntity();
            }
        }

        public string GetTitle()
        {
            return EntityEditControl.GetTitle();
        }

        private void layoutFilter_SizeChanged(object sender, EventArgs e)
        {
            grid.Height = splitContainer.Panel1.Height - (panelHeader.Height + panelFooter.Height);
            grid.Top = panelHeader.Height;
        }

        private void gridView_StartSorting(object sender, EventArgs e)
        {
            if (fExp == null)
                fExp = new FilterExpression();
            else if (
                        gridView.SortInfo.Count > 0 && 
                        fExp.Orders.Count >0 &&
                        gridView.SortInfo[0].Column.FieldName == fExp.Orders[0].FieldName && 
                        fExp.Orders[0].Ascending == (gridView.SortInfo[0].SortOrder == ColumnSortOrder.Ascending)
                    )
                        return; //***
            fExp.Orders = new OrderList();
            foreach (GridColumnSortInfo item in gridView.SortInfo)
            {
                Order o = fExp.Orders.Count>0 ? fExp.Orders.FirstOrDefault(ord => ord.FieldName == item.Column.FieldName) : null;
                if (o == null) { o = new Order(); fExp.Orders.Add(o); }
                o.FieldName = item.Column.FieldName;
                o.Ascending = item.SortOrder == ColumnSortOrder.Ascending;
            }
            BindGrid();
            showEntity();
            gridView.Columns[fExp.Orders[0].FieldName].SortOrder = fExp.Orders[0].Ascending ? ColumnSortOrder.Ascending : ColumnSortOrder.Descending;
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle > -1 && e.RowHandle != gridView.FocusedRowHandle)
            {
                object data = gridView.GetRow(e.RowHandle);
                EntityEditControl.SetStyleOfGridCell(data, e);
            }
        }

    }

    public class ColumnDefinition
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int Width { get; set; }
        public bool Visible { get; set; }
        public object ColumnEdit { get; set; }
        public HAlign HAlign { get; set; }
        public FormatType FormatType { get; set; }
        public string FormatString { get; set; }

        public ColumnDefinition()
        {
            Visible = true;
        }
    }
    public enum HAlign {Near, Center, Far}
    public enum FormatType {Custom, DateTime, None, Numeric}

    public class ColumnDefinitionList : List<ColumnDefinition> { }
}