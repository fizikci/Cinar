using Cinar.Database;

namespace Cinar.Entities.Standart
{
    public class NamedEntity : BaseEntity
    {
        [ColumnDetail(Length = 150)]
        public string Name { get; set; }

        public override string GetNameColumn()
        {
            return "Name";
        }
        public override string GetNameValue()
        {
            return Name;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
