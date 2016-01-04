using System;
using System.Text;

namespace Emby.XmlTv.Entities
{
    public class XmlTvEpisode
    {
        public int? Series { get; set; }
        public int? SeriesCount { get; set; }
        public int? Episode { get; set; }
        public int? EpisodeCount { get; set; }
        public string Title { get; set; }
        public int? Part { get; set; }
        public int? PartCount { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            if (Series.HasValue || SeriesCount.HasValue)
            {
                builder.AppendFormat("Series {0} of {1}", Series, SeriesCount);
            }

            if (Episode.HasValue || EpisodeCount.HasValue)
            {
                builder.Append(builder.Length > 0 ? "," : String.Empty);
                builder.AppendFormat("Episode {0} of {1}", Episode, EpisodeCount);
            }

            if (Part.HasValue || PartCount.HasValue)
            {
                builder.Append(builder.Length > 0 ? "," : String.Empty);
                builder.AppendFormat("Part {0} of {1}", Part, PartCount);
            }

            return builder.ToString();
        }
    }

    
}
