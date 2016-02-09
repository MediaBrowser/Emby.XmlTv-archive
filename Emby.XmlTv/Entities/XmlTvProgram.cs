using System;
using System.Collections.Generic;
using System.Text;

namespace Emby.XmlTv.Entities
{
    public class XmlTvProgram
    {
        public string Id { get; set; }

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

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Id: \t\t{Id}");
            builder.AppendLine($"ChannelId: \t\t{ChannelId}");
            builder.AppendLine($"Name: \t\t{Name}");
            builder.AppendLine($"StartDate: \t\t{StartDate}");
            builder.AppendLine($"EndDate: \t\t{EndDate}");
            return builder.ToString();
        }
    }
}
