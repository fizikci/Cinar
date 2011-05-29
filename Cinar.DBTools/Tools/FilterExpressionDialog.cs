using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.Database;

namespace Cinar.DBTools.Tools
{
    public partial class FilterExpressionDialog : Form
    {
        public FilterExpressionDialog(Table table)
        {
            InitializeComponent();

            foreach (Column column in table.Columns)
                colColumnName.Items.Add(column.Name);

            colCriteriaType.DisplayMember = "Name";
            colCriteriaType.ValueMember = "Value";
            colCriteriaType.DataSource = new List<CriteriaView> { 
                new CriteriaView(CriteriaTypes.Eq, "="),
                new CriteriaView(CriteriaTypes.NotEq, "<>"),
                new CriteriaView(CriteriaTypes.Gt, ">"),
                new CriteriaView(CriteriaTypes.Ge, ">="),
                new CriteriaView(CriteriaTypes.Lt, "<"),
                new CriteriaView(CriteriaTypes.Le, "<="),
                new CriteriaView(CriteriaTypes.Like, "LIKE"),
                new CriteriaView(CriteriaTypes.NotLike, "NOT LIKE"),
                new CriteriaView(CriteriaTypes.In, "IN"),
                new CriteriaView(CriteriaTypes.NotIn, "NOT IN"),
                new CriteriaView(CriteriaTypes.IsNull, "IS NULL"),
                new CriteriaView(CriteriaTypes.IsNotNull, "IS NOT NULL")
            };

            criteriaBindingSource.DataSource = new List<Criteria>();
        }

        FilterExpression initialFExp;

        public FilterExpression FilterExpression 
        {
            get 
            {
                if(initialFExp==null)
                    initialFExp = new FilterExpression();
                initialFExp.Criterias = new CriteriaList();
                if(criteriaBindingSource.DataSource is List<Criteria>)
                foreach (Criteria criteria in criteriaBindingSource.DataSource as List<Criteria>)
                    initialFExp.Criterias.Add(criteria);
                return initialFExp;
            }

            set 
            {
                initialFExp = value;
                criteriaBindingSource.DataSource = value.Criterias.ToList();
            }
        }

        private void grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            //MessageBox.Show(e.Exception.Message);
        }
    }

    public class CriteriaView
    {
        public CriteriaView(CriteriaTypes cr, string name)
        {
            Value = cr;
            Name = name;
        }

        public CriteriaTypes Value { get; set; }
        public string Name { get; set; }
    }
}
