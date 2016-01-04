using System;
using System.Linq;
using System.Text;

using Emby.XmlTv.Entities;

namespace Emby.XmlTv.Console.Classes
{
    public static class EntityExtensions
    {
        public static string GetChannelHeader(this XmlTvChannel channel)
        {
            var channelHeaderString = $" {channel.Id} - {channel.Name} ";

            var builder = new StringBuilder();
            builder.AppendLine("".PadRight(5 + channelHeaderString.Length + 5, Char.Parse("*")));
            builder.AppendLine("".PadRight(5, Char.Parse("*")) + channelHeaderString + "".PadRight(5, Char.Parse("*")));
            builder.AppendLine("".PadRight(5 + channelHeaderString.Length + 5, Char.Parse("*")));

            return builder.ToString();
        }

        public static string GetProgrammeDetail(this XmlTvProgram programme, XmlTvChannel channel)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Channel:           {channel.Id} - {channel.Name}");
            builder.AppendLine($"StartDate:         {programme.StartDate:G}");
            builder.AppendLine($"EndDate:           {programme.EndDate:G}");
            builder.AppendLine($"Name:              {programme.Name}");
            builder.AppendLine($"Episode Detail:    {programme.Episode}");
            builder.AppendLine($"Episode Title:     {programme.Episode.Title}");
            builder.AppendLine($"Short Overview:    {programme.ShortOverview}");
            builder.AppendLine($"Categories:        {string.Join(", ", programme.Genres)}");
            builder.AppendLine($"Credits:           {string.Join(", ", programme.Credits)}");
            builder.AppendLine($"OriginalAirDate:   {programme.OriginalAirDate:G}");
            builder.AppendLine($"ProductionYear:    {programme.ProductionYear}");
            builder.AppendLine($"IsRepeat:          {programme.IsRepeat}");
            builder.AppendLine("-------------------------------------------------------");
            return builder.ToString();
        }
    }
}
