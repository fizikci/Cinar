namespace Cinar.Entities
{
    public class ReportParam : NamedEntity
    {
		private ReportParamTypes pType;
        private string pEntityName = "";

		public string PName
		{
			get;
			set;
		}

        public ReportParamTypes PType
        {
            get { return pType; }
            set { pType = value; }
        }

		public string PEntityName
		{
			get { return pEntityName; }
			set { pEntityName = value; }
		}

		public string PDisplayField
		{
			get;
			set;
		}

		public string PValueField
		{
			get;
			set;
		}

        public int ReportId
        {
            get;
            set;
        }
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

