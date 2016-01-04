using System;
using System.Collections.Generic;

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

        public DateTime? OriginalAirDate { get; set; }

        public float? CommunityRating { get; set; }

        public bool IsRepeat { get; set; }

        public bool IsSeries { get; set; }

        public int? ProductionYear { get; set; }

        public XmlTvEpisode Episode { get; set; }

        public List<XmlTvCredit> Credits { get; set; }

        public XmlTvProgram()
        {
            Credits = new List<XmlTvCredit>();
            Genres = new List<string>();
            Episode = new XmlTvEpisode();
        }
    }
}
