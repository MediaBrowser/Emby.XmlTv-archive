using System;
using System.Collections.Generic;
using System.Text;

namespace Emby.XmlTv.Entities
{
    public class XmlTvProgram : IEquatable<XmlTvProgram>
    {
        public string ChannelId { get; set; }

        public string Name { get; set; }

        public string OfficialRating { get; set; }

        public string Overview { get; set; }

        public string ShortOverview { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<string> Genres { get; set; }

        public DateTime? PreviouslyShown { get; set; }

        public float? CommunityRating { get; set; }

        public bool IsRepeat { get; set; }

        public bool IsSeries { get; set; }

        public DateTime? CopyrightDate { get; set; }

        public XmlTvEpisode Episode { get; set; }

        public List<XmlTvCredit> Credits { get; set; }

        public XmlTvProgram()
        {
            Credits = new List<XmlTvCredit>();
            Genres = new List<string>();
            Episode = new XmlTvEpisode();
        }

        public bool Equals(XmlTvProgram other)
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
            return ChannelId == other.ChannelId &&
                StartDate == other.StartDate &&
                EndDate == other.EndDate;
        }

        public override int GetHashCode()
        {
            return (ChannelId.GetHashCode() * 17) + (StartDate.GetHashCode() * 17) + (EndDate.GetHashCode() * 17);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("ChannelId: \t\t{0}\r\n", ChannelId);
            builder.AppendFormat("Name: \t\t{0}\r\n", Name);
            builder.AppendFormat("StartDate: \t\t{0}\r\n", StartDate);
            builder.AppendFormat("EndDate: \t\t{0}\r\n", EndDate);
            return builder.ToString();
        }
    }
}
