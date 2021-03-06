﻿using Cinar.Database;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Author.Id, Author.Name as [Author.Name], Author.Visible as [Author.Visible] from [Author]", QueryOrderBy = "Author.Name")]
    [DefaultData(ColumnList = "Name", ValueList = "'Editorial'")]
    [OverrideAttribute(AttributeType = typeof(PictureFieldPropsAttribute), FieldName = "Picture", AttribProps = "SpecialFolder|AddRandomNumber|UseYearMonthDayFolders", NewValues = "authorDir|false|false")]
    public class Author : UserRelatedEntity
    {
        public string Title { get; set; }

        private bool disableAutoContent;
        [ColumnDetail(IsNotNull = true, DefaultValue = "0")]
        public bool DisableAutoContent
        {
            get { return disableAutoContent; }
            set { disableAutoContent = value; }
        }
    }
}
