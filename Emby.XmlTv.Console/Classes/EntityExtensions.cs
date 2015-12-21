using System;
using System.Text;

using MediaBrowser.Controller.LiveTv;
using MediaBrowser.Model.Dto;

namespace Emby.XmlTv.Console.Classes
{
    public static class EntityExtensions
    {
        public static string GetChannelHeader(this NameIdPair channel)
        {
            var channelHeaderString = $" {channel.Id} - {channel.Name} ";

            var builder = new StringBuilder();
            builder.AppendLine("".PadRight(5 + channelHeaderString.Length + 5, Char.Parse("*")));
            builder.AppendLine("".PadRight(5, Char.Parse("*")) + channelHeaderString + "".PadRight(5, Char.Parse("*")));
            builder.AppendLine("".PadRight(5 + channelHeaderString.Length + 5, Char.Parse("*")));

            return builder.ToString();
        }

        public static string GetProgrammeDetail(this ProgramInfo programme)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"StartDate:         {programme.StartDate:G}");
            builder.AppendLine($"EndDate:           {programme.EndDate:G}");
            builder.AppendLine($"Name:              {programme.Name}");
            builder.AppendLine($"Episode Title:     {programme.EpisodeTitle}");
            builder.AppendLine($"Episode Num:       {programme.EpisodeNumber}");
            builder.AppendLine($"Short Overview:    {programme.ShortOverview}");
            builder.AppendLine($"Categories:        {string.Join(",", programme.Genres)}");
            builder.AppendLine($"OriginalAirDate:   {programme.OriginalAirDate:G}");
            builder.AppendLine($"IsRepeat:          {programme.IsRepeat}");
            builder.AppendLine("-------------------------------------------------------");
            return builder.ToString();
        }
    }
}
