﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Core
{
    public abstract partial class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreateTime
        {
            get
            {
                return DateTime.Now;
            }
        }

        public DateTime UpdateTime
        {
            get
            {
                return DateTime.Now;
            }
        }

        public bool IsDel { get; set; }

        public DateTime? DeleleTime { get; set; }



        public override bool Equals(object obj)
        {
            return Equals(obj as BaseEntity);
        }

        private static bool IsTransient(BaseEntity obj)
        {
            return obj != null && Equals(obj.Id, default(int));
        }
        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(BaseEntity other)
        {
            if (other == null)
                return false;
            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this) && !IsTransient(other) && Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                        otherType.IsAssignableFrom(thisType);
            }
            return false;
        }


        public override int GetHashCode()
        {
            if (Equals(Id, default(int)))
                return base.GetHashCode();
            return Id.GetHashCode();
        }

        public static bool operator ==(BaseEntity x, BaseEntity y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(BaseEntity x, BaseEntity y)
        {
            return !(x == y);
        }
    }
}