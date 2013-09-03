using System;

namespace Cinar.CMS.Library
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class EditFormFieldPropsAttribute : Attribute
    {
        public EditFormFieldPropsAttribute()
        {
            Options = "";
            ControlType = ControlType.Undefined;
            Visible = true;
        }

        public bool Visible { get; set; }

        public ControlType ControlType { get; set; }

        public string Options { get; set; }

        public string Category { get; set; }

        public int OrderNo { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class ListFormPropsAttribute : Attribute
    {
        public ListFormPropsAttribute()
        {
            VisibleAtMainMenu = false;
        }

        public bool VisibleAtMainMenu { get; set; }

        public string QuerySelect { get; set; }

        public string QueryOrderBy { get; set; }
    }


    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class EditFormDetailsAttribute : Attribute
    {
        public EditFormDetailsAttribute() { }

        public Type DetailType { get; set; }

        public string RelatedFieldName { get; set; }
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
        LookUp,
        TagEdit
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ModuleInfoAttribute : Attribute
    {
        public ModuleInfoAttribute()
        {
            Visible = true;
            Grup = "Diğer";
        }

        //private string name = "İsimsiz Modül";

        public string Grup { get; set; }

        //public string Name
        //{
        //    get { return name; }
        //    set { name = value; }
        //}

        public bool Visible { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class PictureFieldPropsAttribute : Attribute
    {
        public PictureFieldPropsAttribute()
        {
            SpecialFolder = "";
            SpecialNameField = "";
        }

        public string SpecialNameField { get; set; }

        public string SpecialFolder { get; set; }

        public bool AddRandomNumber { get; set; }

        public bool UseYearMonthDayFolders { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class OverrideAttributeAttribute : Attribute
    {
        public OverrideAttributeAttribute()
        {
            AttribProps = "";
            FieldName = "";
        }

        public Type AttributeType { get; set; }

        public string FieldName { get; set; }

        /// <summary>
        /// Pipe seperated attribute property names
        /// </summary>
        public string AttribProps { get; set; }

        /// <summary>
        /// Pipe seperated attribute values
        /// </summary>
        public string NewValues { get; set; }
    }

}
