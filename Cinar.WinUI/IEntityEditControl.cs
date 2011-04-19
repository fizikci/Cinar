using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.UICommands;
using DevExpress.XtraGrid.Views.Grid;
using Cinar.Database;
using Cinar.Entities.Standart;

namespace Cinar.WinUI
{
    public interface ICinarForm
    {
        CommandCollection GetCommands();
        string GetTitle();
        void Initialize(CommandManager cmdMan);
        
    }

    public interface IEntityEditControl : ICinarForm
    {
        Type GetEntityType();
        List<ColumnDefinition> GetVisibleColumns();
        object GetEntityList(FilterExpression fExp, int pageNo, int pageSize);
        void ShowEntity(BaseEntity entity);
        BaseEntity CurrentEntity { get; }
        ListEntity ListForm { get; set; }
        FilterExpression GetFilter();
        Control GetControl(string fieldName);
        string GetControlLabel(string fieldName);
        void SetStyleOfGridCell(object data, RowStyleEventArgs args);
    }
}