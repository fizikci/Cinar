using System;
using System.Collections.Generic;
using System.ComponentModel;
using Cinar.CMS.Library;
using Cinar.CMS.Library.Entities;
using Cinar.Database;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select PaymentTransaction.Id, PaymentTransaction.InsertDate as [BaseEntity.InsertDate], PaymentTransaction.Visible as [BaseEntity.Visible] from [PaymentTransaction]", QueryOrderBy = "[BaseEntity.InsertDate] desc")]
    public class PaymentTransaction : BaseEntity
    {
        [Description("Amount as cents (or kuruş)")]
        public int Amount { get; set; }

        [ColumnDetail(ColumnType = DbType.VarChar, Length = 20)]
        public CheckoutTypes CheckoutType { get; set; }

        [ColumnDetail(ColumnType = DbType.Text)]
        public string Result { get; set; }

    }

    public enum CheckoutTypes
    {
        Manual,
        CreditCard
    }
}
