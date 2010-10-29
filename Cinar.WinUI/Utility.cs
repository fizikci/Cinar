using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using DevExpress.XtraEditors;
using System.Web;
using System.Xml.Serialization;
using System.Xml;
using Cinar.Entities.Standart;

namespace Cinar.WinUI
{
    public static class Utility
    {
        #region binding
        public static void BindTo(this ComboBoxEdit combo, IEnumerable list)
        {
            combo.Properties.Items.Clear();
            foreach (var item in list)
                combo.Properties.Items.Add(item);
        }

        #endregion

        public static string CompareFields(this BaseEntity obj1, BaseEntity obj2)
        {
            // InsertDate, UpdateDate, InsertUserId","UpdateUserId" alanlarındaki değişiklikleri değişiklikten saymıyoruz
            return  System.Utility.CompareFields(obj1, obj2, pi => !new[] { "InsertDate", "UpdateDate", "InsertUserId", "UpdateUserId" }.Contains(pi.Name));
        }

        public static string ChangeToTurkish(this string word)
        {
            return word
                .Replace("Ü", "U")
                .Replace("Ç", "C")
                .Replace("Ş", "S")
                .Replace("Ğ", "G")
                .Replace("Ö", "O")
                .Replace("İ", "I")
                .Replace("ı", "i")
                .Replace("ş", "s")
                .Replace("ğ", "g")
                .Replace("ö", "o")
                .Replace("ü", "u")
                .Replace("ç", "c")
                .Replace(" ", "_")
                .Trim();
        }
        public static int GetTimeSpanOfTotalDays(this DateTime date)
        {
            return (int)new TimeSpan(date.Ticks).TotalDays;
        }
    }

    public class VisibleToClientAttribute : Attribute
    {
        public VisibleToClientAttribute()
        {
        }
        public VisibleToClientAttribute(bool visible)
        {
            Visible = visible;
        }

        public bool Visible { get; set; }
    }

    public class ObjectStringPair
    {
        public object Value { get; set; }
        public string Display { get; set; }
        public override string ToString()
        {
            return Display;
        }
    }
}