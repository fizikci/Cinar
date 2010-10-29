using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Cinar.Database
{
    public class FilterExpression
    {
        public FilterExpression()
        {
            Criterias = new CriteriaList();
            Orders = new OrderList();
        }

        public int PageNo { get; set; }
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
                    new Criteria(fieldName, criteriaType, fieldValue.ToString())
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
            if (criterias != null && criterias.Count > 0)
                return String.Join(" AND ", criterias.ConvertAll(c => c.ToString()).ToArray()); 

            return "";
        }
        public string ToParamString(string tableNameAlias, string parameterPrefix)
        {
            if (criterias != null && criterias.Count > 0)
            {
                string[] sList = new string[criterias.Count];
                for (int i = 0; i < sList.Length; i++)
                    sList[i] = criterias[i].ToParamString(tableNameAlias, parameterPrefix + i);
                return string.Join(" AND ", sList);
            }

            return "";
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
                    new Criteria(fieldName, criteriaType, fieldValue.ToString())
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

        string fieldValue = "-1";
        public string FieldValue
        {
            get { return fieldValue; }
            set { fieldValue = value; }
        }

        public Criteria() { }
        public Criteria(string fieldName, CriteriaTypes criteriaType, object fieldValue)
        {
            this.fieldName = fieldName;
            this.criteriaType = criteriaType;
            this.fieldValue = fieldValue.ToString();
        }

        public string ToParamString(string tableNameAlias, string parameterName)
        {
            string str = (!string.IsNullOrEmpty(tableNameAlias) ? tableNameAlias + "." : "") + fieldName + " ";
            switch (criteriaType)
            {
                case CriteriaTypes.Eq:
                    str += " = :" + parameterName;
                    break;
                case CriteriaTypes.NotEq:
                    str += " <> :" + parameterName;
                    break;
                case CriteriaTypes.Gt:
                    str += " > :" + parameterName;
                    break;
                case CriteriaTypes.Ge:
                    str += " >= :" + parameterName;
                    break;
                case CriteriaTypes.Lt:
                    str += " < :" + parameterName;
                    break;
                case CriteriaTypes.Le:
                    str += " <= :" + parameterName;
                    break;
                case CriteriaTypes.IsNull:
                    str += " is null ";
                    break;
                case CriteriaTypes.IsNotNull:
                    str += " is not null ";
                    break;
                case CriteriaTypes.Like:
                    str += " like :" + parameterName;
                    break;
                case CriteriaTypes.NotLike:
                    str += " not like :" + parameterName;
                    break;
                case CriteriaTypes.In:
                    str += " in :" + parameterName;
                    break;
                case CriteriaTypes.NotIn:
                    str += " not in :" + parameterName;
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
            string str = "order by " + fieldName + (ascending ? "" : " desc");
            return str;
        }
    }

    public class OrderList : List<Order>
    {
    }
}
