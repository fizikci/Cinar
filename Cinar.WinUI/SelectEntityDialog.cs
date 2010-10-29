using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cinar.UICommands;
using DevExpress.XtraEditors;
using Cinar.Database;
using Cinar.Entities;

namespace Cinar.WinUI
{
    public partial class SelectEntityDialog : DevExpress.XtraEditors.XtraForm, IFormEntity
    {
        public SelectEntityDialog()
        {
            InitializeComponent();
            PageSize = 20;
        }
        public SelectEntityDialog(Type entityType, List<ColumnDefinition> visibleColumns, FilterExpression filter, string searchFieldName, Func<BaseEntity, string, BaseEntity> getCurrentEntity)
        {
            InitializeComponent();
            PageSize = 20;

            this.EntityType = entityType;
            this.VisibleColumns = visibleColumns;
            this.Filter = filter;
            this.SearchFieldName = searchFieldName;
            this.GetCurrentEntity = getCurrentEntity;
        }

        public Type EntityType { get; set; }
        public List<ColumnDefinition> VisibleColumns { get; set; }
        public FilterExpression Filter { get; set; }
        private int pageNo = 0;
        public int PageSize { get; set; }
        public string SearchFieldName { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            CommandManager cmdMan = new CommandManager();
            cmdMan.Commands = new CommandCollection { 
                 new Command(){
                                  Execute = cmdNextPage,
                                  Trigger = new CommandTrigger(){Control=btnNext},
                                  IsEnabled = () => gridView1.RowCount == PageSize
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
                 new Command(){
                                  Execute = cmdCloseOK,
                                  Trigger = new CommandTrigger(){Control=gridView1, Event="DoubleClick"},
                              },
                 new Command(){
                                  Execute = (arg)=>{pageNo=0; bindGrid();},
                                  Trigger = new CommandTrigger(){Control=editSearch, Event="EditValueChanged"},
                              },
            };
            cmdMan.SetCommandTriggers();


            if (string.IsNullOrEmpty(Text))
                Text = EntityType.Name + " seçiniz";

            bindGrid();
        }

        private void bindGrid()
        {
            if (!string.IsNullOrEmpty(SearchFieldName))
            {
                string search = editSearch.Text;
                if(!search.Contains("%"))
                    search = (editSearch.Text.Length>3 ? "%" : "") + editSearch.Text + "%";
                if (Filter[SearchFieldName] == null)
                    Filter = Filter.And(SearchFieldName, CriteriaTypes.Like, search);
                else
                    Filter[SearchFieldName].FieldValue = search;
            }
            Filter.PageNo = pageNo;
            Filter.PageSize = PageSize;
            gridControl1.DataSource = DMT.Provider.Db.ReadList(EntityType, Filter);
            DMT.Provider.PopulateGridColumns(VisibleColumns, gridView1);
        }

        public List<BaseEntity> GetSelectedEntities()
        {
            List<BaseEntity> list = new List<BaseEntity>();

            foreach (int handle in gridView1.GetSelectedRows())
                list.Add((BaseEntity)gridView1.GetRow(handle));

            return list;
        }

        BaseEntity currentEntity = null;
        public BaseEntity CurrentEntity
        {
            get {
                if (currentEntity == null)
                {
                    List<BaseEntity> list = GetSelectedEntities();

                    if (GetCurrentEntity != null)
                    {
                        if (list.Count > 0)
                            currentEntity = GetCurrentEntity(list[0], SearchString);
                        else
                            currentEntity = GetCurrentEntity(null, SearchString);
                    }
                }

                return currentEntity;
            }
        }
        public string SearchString
        {
            get { return editSearch.Text; }
            set { editSearch.Text = value; }
        }

        public Func<BaseEntity, string, BaseEntity> GetCurrentEntity;

        private void cmdCloseOK(string arg)
        {
            this.DialogResult = DialogResult.OK;
        }
        private void cmdNextPage(string arg)
        {
            pageNo++;
            bindGrid();
        }
        private void cmdPrevPage(string arg)
        {
            pageNo--;
            bindGrid();
        }
        private void cmdChangePageSize(string arg)
        {
            int pageSize = this.PageSize;
            bool changed = int.TryParse(cbPageSize.SelectedItem.ToString(), out pageSize);
            if (changed && pageSize != PageSize)
            {
                this.pageNo = 0;
                this.PageSize = pageSize;
                bindGrid();
            }
        }





    }
}