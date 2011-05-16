namespace Cinar.Entities.Standart
{
    public class NamedEntity : BaseEntity
    {
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
