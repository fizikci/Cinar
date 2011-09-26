using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Database;
using Cinar.Entities.Standart;

namespace Cinar.Entities.IssueTracking
{
    [DefaultData(ColumnList = "ShowFields, Name", ValueList = "'Name, Status, Priority, Component, AssignedTo, CreatedOn', 'All Tickets'")]
    public class TicketQuery : NamedEntity
    {
        public string ValType { get; set; } // Bug, Task

        public string ValStatus { get; set; } // New, Accepted, Rejected, Resolved
        public string ValPriority { get; set; } // Low, Normal, High
        public string ValComponent { get; set; }

        public int ValReportedById { get; set; }
        public int ValAssignedToId { get; set; }

        public string ValName { get; set; }
        public string ValDescription { get; set; }

        public string ShowFields { get; set; }
        public string GroupByField { get; set; }
        public string OrderByField { get; set; }
        public int PageSize { get; set; }

        public List<Ticket> ExecuteQuery(int pageNo)
        {
            FilterExpression where = new FilterExpression();

            if (!string.IsNullOrWhiteSpace(ValType))
                where.Criterias.Add(new Criteria { ColumnName = "Type", CriteriaType = CriteriaTypes.Eq, ColumnValue = ValType });
            if (!string.IsNullOrWhiteSpace(ValStatus))
                where.Criterias.Add(new Criteria { ColumnName = "Status", CriteriaType = CriteriaTypes.Eq, ColumnValue = ValStatus });
            if (!string.IsNullOrWhiteSpace(ValPriority))
                where.Criterias.Add(new Criteria { ColumnName = "Priority", CriteriaType = CriteriaTypes.Eq, ColumnValue = ValPriority });
            if (!string.IsNullOrWhiteSpace(ValComponent))
            {
                if (!ValComponent.Contains("%"))
                    ValComponent = "%" + ValComponent + "%";
                where.Criterias.Add(new Criteria { ColumnName = "Component", CriteriaType = CriteriaTypes.Like, ColumnValue = ValComponent });
            }

            if (ValReportedById > 0)
                where.Criterias.Add(new Criteria { ColumnName = "ReportedById", CriteriaType = CriteriaTypes.Eq, ColumnValue = ValReportedById });
            if (ValAssignedToId > 0)
                where.Criterias.Add(new Criteria { ColumnName = "AssignedToId", CriteriaType = CriteriaTypes.Eq, ColumnValue = ValAssignedToId });

            if (!string.IsNullOrWhiteSpace(ValName))
            {
                if (!ValName.Contains("%"))
                    ValName = "%" + ValName + "%";
                where.Criterias.Add(new Criteria { ColumnName = "Name", CriteriaType = CriteriaTypes.Like, ColumnValue = ValName });
            }
            if (!string.IsNullOrWhiteSpace(ValDescription))
            {
                if (!ValDescription.Contains("%"))
                    ValDescription = "%" + ValDescription + "%";
                where.Criterias.Add(new Criteria { ColumnName = "Description", CriteriaType = CriteriaTypes.Like, ColumnValue = ValDescription });
            }

            if(string.IsNullOrWhiteSpace(ShowFields)) ShowFields = "Id,Name";
            if (!string.IsNullOrWhiteSpace(GroupByField) && !ShowFields.Contains(GroupByField)) ShowFields += "," + GroupByField;

            if (!string.IsNullOrWhiteSpace(OrderByField))
            {
                where.Orders = new OrderList();
                where.Orders.Add(new Order {ColumnName = OrderByField});
            }

            where.PageNo = pageNo;
            where.PageSize = PageSize;

            return CinarContext.Db.ReadList<Ticket>(where);
        }

        public static TicketQuery GetDefault()
        {
            return new TicketQuery {
                ShowFields = "Name, Status, Priority, Component, AssignedTo, CreatedOn",
                OrderByField = "Status",
                PageSize = 25,
                Name = "All Tickets"
            };
        }
    }
}
