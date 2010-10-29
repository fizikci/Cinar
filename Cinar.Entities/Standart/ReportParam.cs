namespace Cinar.Entities.Standart
{
    public class ReportParam : NamedEntity
    {
		public string PName	{ get; set; }

        public ReportParamTypes	PType { get; set; }

        public string PModuleName	{ get; set; }

        public string PEntityName	{ get; set; }

		public string PDisplayField	{ get; set; }

		public string PValueField	{ get; set; }

        public int ReportId	{ get; set; }
    }
    public enum ReportParamTypes
    {
        TamSayı = 1,
        Tarih = 2,
        Metin = 3,
        OndalikliSayi = 4,
        EvetHayir = 5,
        Entity = 6
    }
}

