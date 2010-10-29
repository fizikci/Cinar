using Cinar.Database;

namespace Cinar.Entities.Standart
{
    public class RoleRight : BaseEntity
    {
        [FieldDetail(References=typeof(Role))]
        public int RoleId
        {
            get;
            set;
        }

        [FieldDetail(References = typeof(Right))]
        public int RightId
        {
            get;
            set;
        }

        private Role role;
        public Role Role
        {
            get
            {
                if (role == null && RoleId > 0)
                    role = Context.Db.Read<Role>(RoleId);
                return role;
            }
            set 
            {
                role = value;
                RoleId = value == null ? 0 : value.Id;
            }
        }

        private Right right;
        public Right Right
        {
            get
            {
                if (right == null && RightId > 0)
                    right = Context.Db.Read<Right>(RightId);
                return right;
            }
            set
            {
                right = value;
                RightId = value == null ? 0 : value.Id;
            }
        }
    }
}
