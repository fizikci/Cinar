using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Linq;

namespace Cinar.Database
{
    public class FilterExpression
    {
        public FilterExpression()
        {
            Criterias = new CriteriaList();
            Orders = new OrderList();
        }

        private int pageNo = 0;
        public int PageNo
        {
            get { return pageNo; }
            set { pageNo = value < 0 ? 0 : value; }
        }

        public int PageSize { get; set; }

        CriteriaList criterias;
        public CriteriaList Criterias
        {
            get { return criterias; }
            set { criterias = value; }
        }

        OrderList orders;
        public OrderList Orders
        {
            get { return orders; }
            set { orders = value; }
        }

		public static FilterExpression Create(string fieldName, CriteriaTypes criteriaType, object fieldValue)
		{
			return new FilterExpression
			{
				Criterias = new CriteriaList { 
                    new Criteria(fieldName, criteriaType, fieldValue)
                }
			};
		}
    	public static FilterExpression Create(Criteria criteria)
        {
            return new FilterExpression
            {
                Criterias = new CriteriaList { 
                    criteria
                }
            };
        }
        public static FilterExpression Create(CriteriaList criteriaList)
        {
            return new FilterExpression
            {
                Criterias = criteriaList
            };
        }

        public override string ToString()
        {
            string res = "";
            if (criterias != null)
                res += criterias.ToString();
            if (orders != null)
                res += " " + orders;
            if (PageSize > 0)
                res += " limit " + PageSize + " offset " + (PageSize * PageNo);
            return res;
        }
        public string ToParamString()
        {
            string res = "";
            if (criterias != null)
                res += criterias.ToParamString();
            if (orders != null)
                res += orders;
            if (PageSize > 0)
                res += " limit " + PageSize + " offset " + (PageSize * PageNo);

            return res;
        }
        public object[] GetParamValues()
        {
            return this.Criterias.Select(c => c.FieldValue).ToArray();
        }

        public Criteria this[string fieldName]
        {
            get
            {
                foreach (var criteria in Criterias)
                    if (criteria.FieldName == fieldName)
                        return criteria;
                return null;
            }
        }

        public static FilterExpression Where(string fieldName, CriteriaTypes criteriaType, object fieldValue)
        {
            return new FilterExpression
            {
                Criterias = new CriteriaList { 
                    new Criteria(fieldName, criteriaType, fieldValue)
                }
            };
        }
        public FilterExpression And(string fieldName, CriteriaTypes criteriaType, object fieldValue)
        {
            this.Criterias.Add(new Criteria { 
                FieldName = fieldName,
                CriteriaType = criteriaType,
                FieldValue = fieldValue.ToString()
            });
            return this;
        }
        public FilterExpression OrderBy(string fieldName, bool ascending)
        {
            this.Orders.Add(new Order
            {
                FieldName = fieldName,
                Ascending = ascending
            });
            return this;
        }
        public FilterExpression OrderBy(string fieldName)
        {
            return OrderBy(fieldName, true);
        }
    }

    public class Criteria
    {
        string fieldName = "Id";
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        CriteriaTypes criteriaType = CriteriaTypes.Eq;
        public CriteriaTypes CriteriaType
        {
            get { return criteriaType; }
            set { criteriaType = value; }
        }

        object fieldValue = "-1";
        public object FieldValue
        {
            get { return fieldValue; }
            set { fieldValue = value; }
        }

        public Criteria() { }
        public Criteria(string fieldName, CriteriaTypes criteriaType, object fieldValue)
        {
            this.fieldName = fieldName;
            this.criteriaType = criteriaType;
            this.fieldValue = fieldValue;
        }

        public string ToParamString(int index)
        {
            string str = fieldName + " ";
            switch (criteriaType)
            {
                case CriteriaTypes.Eq:
                    str += " = {" + index + "}";
                    break;
                case CriteriaTypes.NotEq:
                    str += " <> {" + index + "}";
                    break;
                case CriteriaTypes.Gt:
                    str += " > {" + index + "}";
                    break;
                case CriteriaTypes.Ge:
                    str += " >= {" + index + "}";
                    break;
                case CriteriaTypes.Lt:
                    str += " < {" + index + "}";
                    break;
                case CriteriaTypes.Le:
                    str += " <= {" + index + "}";
                    break;
                case CriteriaTypes.IsNull:
                    str += " is null ";
                    break;
                case CriteriaTypes.IsNotNull:
                    str += " is not null ";
                    break;
                case CriteriaTypes.Like:
                    str += " like {" + index + "}";
                    break;
                case CriteriaTypes.NotLike:
                    str += " not like {" + index + "}";
                    break;
                case CriteriaTypes.In:
                    str += " in {" + index + "}";
                    break;
                case CriteriaTypes.NotIn:
                    str += " not in {" + index + "}";
                    break;
            }

            return str;
        }
        public override string ToString()
        {
            string str = fieldName + " ";
            switch (criteriaType)
            {
                case CriteriaTypes.Eq:
                    str += " = " + fieldValue;
                    break;
                case CriteriaTypes.NotEq:
                    str += " <> " + fieldValue;
                    break;
                case CriteriaTypes.Gt:
                    str += " > " + fieldValue;
                    break;
                case CriteriaTypes.Ge:
                    str += " >= " + fieldValue;
                    break;
                case CriteriaTypes.Lt:
                    str += " < " + fieldValue;
                    break;
                case CriteriaTypes.Le:
                    str += " <= " + fieldValue;
                    break;
                case CriteriaTypes.IsNull:
                    str += " is null ";
                    break;
                case CriteriaTypes.IsNotNull:
                    str += " is not null ";
                    break;
                case CriteriaTypes.Like:
                    str += " like " + fieldValue;
                    break;
                case CriteriaTypes.NotLike:
                    str += " not like " + fieldValue;
                    break;
                case CriteriaTypes.In:
                    str += " in " + fieldValue;
                    break;
                case CriteriaTypes.NotIn:
                    str += " not in " + fieldValue;
                    break;
            }

            return str;
        }
    }

    public enum CriteriaTypes
    {
        /// <summary>
        /// equal
        /// </summary>
        Eq,
        /// <summary>
        /// not equal
        /// </summary>
        NotEq,
        /// <summary>
        /// greater then
        /// </summary>
        Gt,
        /// <summary>
        /// greater than or equal to
        /// </summary>
        Ge,
        /// <summary>
        /// less then
        /// </summary>
        Lt,
        /// <summary>
        /// less than or equal to
        /// </summary>
        Le,
        IsNull,
        IsNotNull,
        Like,
        NotLike,
        In,
        NotIn
    }

    public class CriteriaList : List<Criteria>
    {
        public override string ToString()
        {
            if (this.Count > 0)
                return " where " + String.Join(" AND ", this.ConvertAll(c => c.ToString()).ToArray());
            return "";
        }
        public string ToParamString()
        {
            string res = "";
            if (this.Count > 0)
            {
                string[] sList = new string[this.Count];
                for (int i = 0; i < sList.Length; i++)
                    sList[i] = this[i].ToParamString(i);
                res += " where " + string.Join(" AND ", sList);
            }
            return res;
        }
    }

    public class Order
    {
        string fieldName = "Id";
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        bool ascending = true;
        public bool Ascending
        {
            get { return ascending; }
            set { ascending = value; }
        }

        public Order() { }
        public Order(string fieldName, bool ascending)
        {
            this.fieldName = fieldName;
            this.ascending = ascending;
        }

        public override string ToString()
        {
            string str = fieldName + (ascending ? "" : " desc");
            return str;
        }
    }

    public class OrderList : List<Order>
    {
        public override string ToString()
        {
            if (this.Count > 0)
                return " order by " + string.Join(", ", this.Select(o => o.ToString()).ToArray());
            return "";
        }
    }
}
