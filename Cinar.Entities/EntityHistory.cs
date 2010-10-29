using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cinar.Entities
{
    public class EntityHistory : BaseEntity
    {
        private string entityName = "";
        private string userName = "";
        private CRUDOperation operation;
        private string details = "";
        private long entityId = -1;

        public virtual string EntityName
        {
            get { return entityName; }
            set { entityName = value; }
        }

        public virtual long EntityId
        {
            get { return entityId; }
            set { entityId = value; }
        }

        public virtual string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public virtual CRUDOperation Operation
        {
            get { return operation; }
            set { operation = value; }
        }

        public virtual string Details
        {
            get { return details; }
            set { details = value; }
        }

    }

    public class EntityHistoryFields
    {
        public const string InsertDate = "InsertDate";
        public const string EntityName = "EntityName";
        public const string EntityId = "EntityId";
        public const string UserName = "UserName";
        public const string Operation = "Operation";
        public const string Details = "Details";
    }

    public enum CRUDOperation
    {
        Insert = 0,
        Update = 1,
        Delete = 2,
        Read = 2
    }
}

