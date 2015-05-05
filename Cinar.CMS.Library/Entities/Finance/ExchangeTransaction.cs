using System;
using Cinar.Database;
using System.Text.RegularExpressions;

namespace Cinar.CMS.Library.Entities
{
    public class ExchangeTransaction : BaseEntity
    {
        public bool Buying { get; set; }
        public ExchangeTypes ExchangeType { get; set; }
        public int AmountInCents { get; set; }
        public int OpenRateInMiliCents { get; set; }
        public bool Closed { get; set; }
        public DateTime CloseDate { get; set; }
        public int CloseRateInMiliCents { get; set; }
    }
    public enum ExchangeTypes
    {
        USD,
        EUR
    }
}
