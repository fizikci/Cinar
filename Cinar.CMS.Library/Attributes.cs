using System;

namespace Cinar.CMS.Library
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class EditFormFieldPropsAttribute : Attribute
    {
        public EditFormFieldPropsAttribute()
        {
        }

        private bool visible = true;
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        private ControlType controlType = ControlType.Undefined;
        public ControlType ControlType
        {
            get { return controlType; }
            set { controlType = value; }
        }

        private string options = "";
        public string Options
        {
            get { return options; }
            set { options = value; }
        }

    }

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class ListFormPropsAttribute : Attribute
    {
        public ListFormPropsAttribute(){}

        private bool visible = false;
        public bool VisibleAtMainMenu
        {
            get { return visible; }
            set { visible = value; }
        }

        private string querySelect;
        public string QuerySelect
        {
            get { return querySelect; }
            set { querySelect = value; }
        }

        private string queryOrderBy;
        public string QueryOrderBy
        {
            get { return queryOrderBy; }
            set { queryOrderBy = value; }
        }
    }


    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class EditFormDetailsAttribute : Attribute
    {
        public EditFormDetailsAttribute() { }

        private Type detailType;
        public Type DetailType
        {
            get { return detailType; }
            set { detailType = value; }
        }

        private string relatedFieldName;
        public string RelatedFieldName
        {
            get { return relatedFieldName; }
            set { relatedFieldName = value; }
        }

    }

    public enum ControlType
    {
        Undefined,
        StringEdit,
        IntegerEdit,
        DecimalEdit,
        DateTimeEdit,
        PictureEdit,
        ComboBox,
        CSSEdit,
        MemoEdit,
        FilterEdit,
        LookUp
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ModuleInfoAttribute : Attribute
    {
        public ModuleInfoAttribute() { }

        private string grup = "Diğer";
        //private string name = "İsimsiz Modül";
        private bool visible = true;

        public string Grup
        {
            get { return grup; }
            set { grup = value; }
        }

        //public string Name
        //{
        //    get { return name; }
        //    set { name = value; }
        //}

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }
    }
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class PictureFieldPropsAttribute : Attribute
    {
        public PictureFieldPropsAttribute()
        {
        }

        private string specialNameField = "";
        public string SpecialNameField
        {
            get { return specialNameField; }
            set { specialNameField = value; }
        }

        private string specialFolder = "";
        public string SpecialFolder
        {
            get { return specialFolder; }
            set { specialFolder = value; }
        }

        private bool addRandomNumber;
        public bool AddRandomNumber
        {
            get { return addRandomNumber; }
            set { addRandomNumber = value; }
        }

        private bool useYearMonthDayFolders;
        public bool UseYearMonthDayFolders
        {
            get { return useYearMonthDayFolders; }
            set { useYearMonthDayFolders = value; }
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class OverrideAttributeAttribute : Attribute
    {
        public OverrideAttributeAttribute()
        {
        }

        private Type attributeType;
        public Type AttributeType
        {
            get { return attributeType; }
            set { attributeType = value; }
        }

        private string fieldName = "";
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        private string attribProps = "";
        /// <summary>
        /// Pipe seperated attribute property names
        /// </summary>
        public string AttribProps
        {
            get { return attribProps; }
            set { attribProps = value; }
        }

        private string newValues;
        /// <summary>
        /// Pipe seperated attribute values
        /// </summary>
        public string NewValues
        {
            get { return newValues; }
            set { newValues = value; }
        }
    }

}
