namespace Cinar.Entities
{
    public class NamedEntity : BaseEntity
    {
        public string Name { get; set; }

        public override string GetNameField()
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
