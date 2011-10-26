using Cinar.Database;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Lang.Id, Lang.Name as [Lang.Name], Lang.Visible as [BaseEntity.Visible] from [Lang]", QueryOrderBy = "Lang.Name")]
    [DefaultData(ColumnList="Code, Name", ValueList="'tr-TR', 'Türkçe'")]
    public class Lang : BaseEntity
    {
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:_LANGS_")]
        public string Code { get; set; }

        [ColumnDetail(IsNotNull=true), EditFormFieldProps(Options = "isHTML:false")]
        public string Name { get; set; }

        public override string GetNameValue()
        {
            return this.Name;
        }
        public override string GetNameColumn()
        {
            return "Name";
        }

        internal static string GetLangsJSON()
        {
            //if (Provider.Items["_LANGS_"] == null)
            //{
            //    ArrayList al = new ArrayList();
            //    foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            //    {
            //        al.Add(String.Format("[{0},{1}],", Provider.ToJS(ci.Name), Provider.ToJS(ci.DisplayName)));
            //    }
            //    al.Sort();

            //    StringBuilder sb = new StringBuilder();

            //    sb.Append("[");
            //    foreach (string lang in al)
            //    {
            //        sb.AppendFormat("{0}", lang);
            //    }
            //    sb.Remove(sb.Length - 1, 1);
            //    sb.Append("]");

            //    Provider.Items["_LANGS_"] = sb;
            //}

            //return Provider.Items["_LANGS_"].ToString();

            return @"[['sq-AL','Albanian (Albania)'],['ar-SA','Arabic (Saudi Arabia)'],['hy-AM','Armenian (Armenia)'],['eu-ES','Basque (Basque)'],['bg-BG','Bulgarian (Bulgaria)'],['ca-ES','Catalan (Catalan)'],['zh-CN','Chinese (China)'],['hr-HR','Croatian (Croatia)'],['cs-CZ','Czech (Czech Republic)'],['da-DK','Danish (Denmark)'],['en-US','English (United States)'],['et-EE','Estonian (Estonia)'],['fi-FI','Finnish (Finland)'],['fr-FR','French (France)'],['ka-GE','Georgian (Georgia)'],['de-DE','German (Germany)'],['el-GR','Greek (Greece)'],['he-IL','Hebrew (Israel)'],['hi-IN','Hindi (India)'],['hr-BA','Hırvatça (Bosna-Hersek)'],['hu-HU','Hungarian (Hungary)'],['id-ID','Indonesian (Indonesia)'],['it-IT','Italian (Italy)'],['ja-JP','Japanese (Japan)'],['kk-KZ','Kazakh (Kazakhstan)'],['kn-IN','Kannada (India)'],['ko-KR','Korean (Korea)'],['ky-KG','Kyrgyz (Kyrgyzstan)'],['lt-LT','Lithuanian (Lithuania)'],['fa-IR','Persian (Iran)'],['pl-PL','Polish (Poland)'],['pt-PT','Portuguese (Portugal)'],['ro-RO','Romanian (Romania)'],['ru-RU','Russian (Russia)'],['es-ES','Spanish (Spain)'],['sv-SE','Swedish (Sweden)'],['th-TH','Thai (Thailand)'],['tr-TR','Turkish (Turkey)'],['uk-UA','Ukrainian (Ukraine)'],['vi-VN','Vietnamese (Vietnam)']]";
        }
    }
}
