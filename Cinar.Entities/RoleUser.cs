﻿using Cinar.Database;

namespace Cinar.Entities
{
    public class RoleUser : BaseEntity
    {
        [FieldDetail(References = typeof(User))]
        public int UserId
        {
            get;
            set;
        }

        [FieldDetail(References = typeof(Role))]
        public int RoleId
        {
            get;
            set;
        }

        private User user;
        public User User
        {
            get
            {
                if (user == null && UserId > 0)
                    user = Context.Db.Read<User>(UserId);
                return user;
            }
            set
            {
                user = value;
                UserId = value == null ? 0 : value.Id;
            }
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
    }
}
