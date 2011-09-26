using Cinar.Database;

namespace Cinar.Entities.Standart
{
    public class RoleRight : BaseEntity
    {
        [ColumnDetail(References=typeof(Role))]
        public int RoleId
        {
            get;
            set;
        }

        [ColumnDetail(References = typeof(Right))]
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
                    role = CinarContext.Db.Read<Role>(RoleId);
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
                    right = CinarContext.Db.Read<Right>(RightId);
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
