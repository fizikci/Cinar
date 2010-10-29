using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Cinar.UICommands;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using Cinar.Database;
using Cinar.Entities.Standart;

namespace Cinar.WinUI
{
    public partial class ListEntitySimple : DevExpress.XtraEditors.XtraUserControl
    {
        public ListEntitySimple()
        {
            InitializeComponent();

            btnPrev.Image = Properties.Resources.go_prev;
            btnNext.Image = Properties.Resources.go_next;
            btnAddEntity.Image = menuAddEntity.Image = Properties.Resources.add;
            btnDeleteEntity.Image = menuDeleteEntity.Image = Properties.Resources.delete;
            btnEditEntity.Image = menuEditEntity.Image = Properties.Resources.application_form;
            btnExcel.Image = menuExcel.Image = Properties.Resources.table;
            menuHistory.Image = Properties.Resources.book_open;
            menuCopy.Image = Properties.Resources.page_copy;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.DesignMode)
                showSampleDate();
            if(!this.DesignMode)
                pnlTitle.BackColor = DMT.SkinColors[this.LookAndFeel.ActiveSkinName];
        }

        private void showSampleDate()
        {
            List<BaseEntity> sampleDataSource = new List<BaseEntity>();
            for (int i = 0; i < PageSize % 50; i++)
                sampleDataSource.Add(new BaseEntity { Id = -1 });
            grid.DataSource = sampleDataSource;
        }

        public void WhenStyleChanged()
        {
            pnlTitle.BackColor = DMT.SkinColors[this.LookAndFeel.ActiveSkinName];
        }

        private string _title;
        [Category("Cinar")]
        public string Title
        {
            get { return _title; }
            set { 
                _title = value;
                lblTitle.Text = "     " + value;
            }
        }

        [Category("Cinar")]
        public Bitmap ImageForEntity 
        {
            get { return (Bitmap) lblTitle.Image; }
            set { lblTitle.Image = value; } 
        }

        [Category("Cinar")]
        public string DeleteWarningMessage { get; set; }

        private bool showAddButton;
        private bool showEditButton;
        private bool showDeleteButton;
        private bool showExcelButton;
        private bool showHistoryButton;

        [Category("Cinar")]
        public bool ShowAddButton { get { return showAddButton; } set { btnAddEntity.Visible = showAddButton = value; resizeGrid(); } }

        [Category("Cinar")]
        public bool ShowEditButton { get { return showEditButton; } set { btnEditEntity.Visible = showEditButton = value; resizeGrid(); } }

        [Category("Cinar")]
        public bool ShowDeleteButton { get { return showDeleteButton; } set { btnDeleteEntity.Visible = showDeleteButton = value; resizeGrid(); } }

        [Category("Cinar")]
        public bool ShowExcelButton { get { return showExcelButton; } set { btnExcel.Visible = showExcelButton = value; resizeGrid(); } }

        [Category("Cinar")]
        public bool ShowHistoryButton { get { return showHistoryButton; } set { menuHistory.Visible = showHistoryButton = value; resizeGrid(); } }

        private void resizeGrid()
        {
            if (ShowAddButton || ShowEditButton || ShowDeleteButton || ShowExcelButton)
                grid.Height = this.Height - 63;
            else
                grid.Height = this.Height - 36;
        }

        public CommandCollection GetCommands()
        {
            return new CommandCollection { 
                 new Command {
                                  Execute = cmdAddEntity,
                                  Triggers = new List<CommandTrigger>{ 
                                      new CommandTrigger {Control=btnAddEntity},
                                      new CommandTrigger {Control=menuAddEntity},
                                  },
                                  IsVisible = () => showAddButton
                              },
                 new Command {
                                  Execute = cmdEditEntity,
                                  Triggers = new List<CommandTrigger>{ 
                                      new CommandTrigger {Control=btnEditEntity},
                                      new CommandTrigger {Control=menuEditEntity},
                                  },
                                  IsEnabled = () => SelectedEntity!=null,
                                  IsVisible = () => showEditButton
                              },
                 new Command {
                                  Execute = cmdDeleteEntity,
                                  Triggers = new List<CommandTrigger>{ 
                                      new CommandTrigger {Control=btnDeleteEntity},
                                      new CommandTrigger {Control=menuDeleteEntity},
                                  },
                                  IsEnabled = () => SelectedEntity!=null,
                                  IsVisible = () => showDeleteButton
                              },
                 new Command {
                                  Execute = cmdExcel,
                                  Triggers = new List<CommandTrigger>{ 
                                      new CommandTrigger {Control=btnExcel},
                                      new CommandTrigger {Control=menuExcel},
                                  },
                                  IsEnabled = () => gridView.RowCount > 0,
                                  IsVisible = () => showExcelButton
                              },
                 new Command {
                                  Execute = cmdHistory,
                                  Triggers = new List<CommandTrigger>{ 
                                      new CommandTrigger {Control=menuHistory},
                                  },
                                  IsEnabled = () => gridView.RowCount > 0,
                                  IsVisible = () => showHistoryButton
                              },
                 new Command {
                                  Execute = cmdNextPage,
                                  Trigger = new CommandTrigger {Control=btnNext},
                                  IsEnabled = () => gridView.RowCount == PageSize,
                                  IsVisible = () => PageSize > 0
                              },
                 new Command {
                                  Execute = cmdPrevPage,
                                  Trigger = new CommandTrigger {Control=btnPrev},
                                  IsEnabled = () => pageNo > 0,
                                  IsVisible = () => PageSize > 0
                              },
                 new Command {
                                  Execute = cmdDoubleClick,
                                  Trigger = new CommandTrigger(){Control=gridView, Event="DoubleClick"}
                              },
                 new Command {
                                  Execute = cmdCopyGridData,
                                  Trigger = new CommandTrigger(){Control=menuCopy},
                                  IsEnabled = () => gridView.RowCount > 0
                              },
             };
        }

        [Category("Cinar")]
        public Func<ColumnDefinitionList> GetVisibleColumns;
        private ColumnDefinitionList getVisibleColumns() {
            if (this.GetVisibleColumns != null)
                return GetVisibleColumns();
            else
                return new ColumnDefinitionList { new ColumnDefinition{DisplayName="Id", Name="Id", Width=100} };
        }
        private bool gridColumnsPopulated = false;

        [Category("Cinar")]
        public Func<FilterExpression> GetFilter;

        private FilterExpression getFilter() {
            if (GetFilter != null)
                return GetFilter();
            return null;
        }

        private Type entityType;
        [Browsable(false)]
        public Type EntityType {
            get {
                return entityType;
            }
            set { 
                entityType = value;
            }
        }

        [Category("Cinar")]
        public Func<int, int, IList> GetDataSource;


        private int pageNo = 0;

        private int pageSize = 6;
        [Category("Cinar"), DefaultValue(6)]
        public int PageSize {
            get { return pageSize; }
            set
            {
                pageSize = value;
                if (this.DesignMode)
                    showSampleDate();
            }
        }

        [Category("Cinar"), DefaultValue(false)]
        public bool ShowTotalCount { get; set; }

        private int lastTotalRecordCount = 0;
        private IList getDataSource()
        {
            if (GetDataSource != null)
            {
                IList list = GetDataSource(pageNo, PageSize);
                lastTotalRecordCount = list.Count;
                return list;
            }

            FilterExpression filter = getFilter();
            filter.PageNo = pageNo;
            filter.PageSize = PageSize;

            if (ShowTotalCount && PageSize == 0)
                lastTotalRecordCount = DMT.Provider.Db.ReadCount(EntityType, filter);

            return DMT.Provider.Db.ReadList(EntityType, filter).ToList();
        }

    	public IList DataSource
    	{
			get { return (IList)grid.DataSource; }
    	}

        public void BindData()
        {
            IList list = getDataSource();
            while ((list==null || list.Count == 0) && pageNo > 0)
            {
                pageNo--;
                list = getDataSource();
            }
            lblTitle.Text = "     " + Title + (ShowTotalCount ? " (" + lastTotalRecordCount + ")" : "");

            if (!gridColumnsPopulated)
            {
                DMT.Provider.PopulateGridColumns(getVisibleColumns(), gridView);
                gridColumnsPopulated = true;
            }
            grid.DataSource = list;
        }

        [Category("Cinar")]
        public Func<BaseEntity, IFormEntity> CreateEntityForm;

        public Func<BaseEntity, bool> BeforeAddEntity;
        public Action AddEntityCallback;
        public Action EditEntityCallback;
        public Action DeleteEntityCallback;

        private void cmdAddEntity(string arg)
        {
            if (AddEntityCallback != null)
            {
                AddEntityCallback();
                BindData();
                return;
            }

            if (CreateEntityForm == null)
            {
                DMT.Provider.Alert("EditForm henüz hazýrlanmamýþ! Lütfen bu hatayý yazýlým bölümüne bildiriniz.");
                return;
            }

            IFormEntity fma = CreateEntityForm(null);
            if ((fma as Form).ShowDialog() == DialogResult.OK && fma.CurrentEntity != null)
            {
                bool canBeAdded = true;
                if (this.BeforeAddEntity != null)
                    canBeAdded = this.BeforeAddEntity(fma.CurrentEntity);
                if (canBeAdded)
                {
                    DMT.Provider.Db.Save(fma.CurrentEntity);
                    BindData();
                }
            }
        }
        private void cmdEditEntity(string arg)
        {
            if (EditEntityCallback != null)
            {
                EditEntityCallback();
                BindData();
                return;
            }

            BaseEntity mk = SelectedEntity;
            if (CreateEntityForm == null)
            {
                DMT.Provider.FeedBack(mk.GetType().Name + " isimli entity için henüz form hazýrlanmamýþ.");
                return;
            }

            IFormEntity fmk = CreateEntityForm(mk);
            if ((fmk as Form).ShowDialog() == DialogResult.OK)
            {
                DMT.Provider.Db.Save(fmk.CurrentEntity);
                BindData();
            }
        }

        public BaseEntity SelectedEntity
        {
            get
            {
                if (gridView.FocusedRowHandle > -1)
                    return (BaseEntity)gridView.GetRow(gridView.FocusedRowHandle);

                return null;
            }
        }




        private void cmdDeleteEntity(string arg)
        {
            if (DMT.Provider.Confirm(DeleteWarningMessage))
            {
                if (DeleteEntityCallback != null)
                {
                    DeleteEntityCallback();
                    return;
                }

                if (gridView.SelectedRowsCount > 1)
                {
                    foreach (int rowHandle in gridView.GetSelectedRows())
                    {
                        BaseEntity ma = (BaseEntity)gridView.GetRow(rowHandle);
                        if (ma == null) continue;
                        DMT.Provider.Db.ExecuteNonQuery("delete from " + EntityType.Name + " where Id={0}", ma.Id);
                    }
                    BindData();
                }
                else
                {
                    BaseEntity ma = SelectedEntity;
                    DMT.Provider.Db.ExecuteNonQuery("delete from " + EntityType.Name + " where Id={0}", ma.Id);
                    BindData();
                }
            }
        }

        private void cmdNextPage(string arg)
        {
            pageNo++;
            BindData();
        }
        private void cmdPrevPage(string arg)
        {
            pageNo--;
            BindData();
        }

        private void cmdHistory(string arg)
        {
            FormEntityHistory feh = new FormEntityHistory(this.EntityType.Name, this.SelectedEntity.Id);
            feh.ShowDialog();
        }

        private void cmdExcel(string arg)
        {
            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                EntityType.Name + (DateTime.Now.Millisecond % 1000) + ".xls");

            gridView.ExportToXls(path, new DevExpress.XtraPrinting.XlsExportOptions());
            Process.Start(path);
        }

        public void ExportToXls()
        {
            cmdExcel(null);
        }

        private void gridView_StartSorting(object sender, EventArgs e)
        {
            FilterExpression fExp = getFilter();
            if (fExp == null)
                fExp = new FilterExpression();
            else if (
                gridView.SortInfo.Count > 0 &&
                fExp.Orders.Count > 0 &&
                gridView.SortInfo[0].Column.FieldName == fExp.Orders[0].FieldName &&
                fExp.Orders[0].Ascending == (gridView.SortInfo[0].SortOrder == ColumnSortOrder.Ascending)
            )
                return; //***

            fExp.Orders = new OrderList();
            foreach (GridColumnSortInfo item in gridView.SortInfo)
            {
                Order o = fExp.Orders.Count > 0 ? fExp.Orders.FirstOrDefault(ord => ord.FieldName == item.Column.FieldName) : null;
                if (o == null) { o = new Order(); fExp.Orders.Add(o); }
                o.FieldName = item.Column.FieldName;
                o.Ascending = item.SortOrder == ColumnSortOrder.Ascending;
            }
            BindData();
            gridView.Columns[fExp.Orders[0].FieldName].SortOrder = fExp.Orders[0].Ascending ? ColumnSortOrder.Ascending : ColumnSortOrder.Descending;

        }

        public Action DoubleClick;
        private void cmdDoubleClick(string arg)
        {
            if (DoubleClick != null)
                DoubleClick();
        }

        private void cmdCopyGridData(string arg)
        {
            object o = gridView.GetRowCellValue(gridView.FocusedRowHandle, gridView.FocusedColumn);
            if (o != null)
                Clipboard.SetDataObject(o.ToString());
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle > -1 && SetStyleOfGridRow != null && e.RowHandle != gridView.FocusedRowHandle)
            {
                BaseEntity data = (BaseEntity)gridView.GetRow(e.RowHandle);
                SetStyleOfGridRow(data, e);
            }
        }

        public Action<BaseEntity, RowStyleEventArgs> SetStyleOfGridRow;
    }
}