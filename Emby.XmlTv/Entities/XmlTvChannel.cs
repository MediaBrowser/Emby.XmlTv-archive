using System;

namespace Emby.XmlTv.Entities
{
    public class XmlTvChannel : IEquatable<XmlTvChannel>
    {
        public String Id { get; set; }
        public String Name { get; set; }

        public bool Equals(XmlTvChannel other)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            // If the other is null then return false
            if (other == null)
            {
                return false;
            }

            // Return true if the fields match:
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return (Id.GetHashCode() * 17);
        }

        public override string ToString()
        {
            return String.Format("{0} - {1} ", Id, Name);
        }
    }
}