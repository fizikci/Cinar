namespace Cinar.CMS.Library.Entities
{
    public class UserPrefferedAuthor : BaseEntity
    {
        private int userId;
        /*[ColumnDetail(References = typeof(User)), EditFormFieldProps(ControlType = ControlType.LookUp)]*/
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private int authorId;
        /*[ColumnDetail(References = typeof(Author)), EditFormFieldProps(ControlType = ControlType.LookUp)]*/
        public int AuthorId
        {
            get { return authorId; }
            set { authorId = value; }
        }

        public override string GetNameValue()
        {
            return this.Id.ToString();
        }
        public override string GetNameColumn()
        {
            return "Id";
        }


    }
}
