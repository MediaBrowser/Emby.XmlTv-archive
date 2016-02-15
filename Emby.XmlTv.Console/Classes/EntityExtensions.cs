using System;
using System.Linq;
using System.Text;

using Emby.XmlTv.Entities;

namespace Emby.XmlTv.Console.Classes
{
    public static class EntityExtensions
    {
        public static string GetHeader(this string text)
        {
            var channelHeaderString = " " + text;

            var builder = new StringBuilder();
            builder.AppendLine("".PadRight(5 + channelHeaderString.Length + 5, Char.Parse("*")));
            builder.AppendLine("".PadRight(5, Char.Parse("*")) + channelHeaderString + "".PadRight(5, Char.Parse("*")));
            builder.AppendLine("".PadRight(5 + channelHeaderString.Length + 5, Char.Parse("*")));

            return builder.ToString();
        }

        public static string GetProgrammeDetail(this XmlTvProgram programme, XmlTvChannel channel)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("Channel:           {0}\r\n", channel);
            builder.AppendFormat("StartDate:         {0:G}\r\n", programme.StartDate);
            builder.AppendFormat("EndDate:           {0:G}\r\n", programme.EndDate);
            builder.AppendFormat("Name:              {0}\r\n", programme.Name);
            builder.AppendFormat("Episode Detail:    {0}\r\n", programme.Episode);
            builder.AppendFormat("Episode Title:     {0}\r\n", programme.Episode.Title);
            builder.AppendFormat("Short Overview:    {0}\r\n", programme.ShortOverview);
            builder.AppendFormat("Categories:        {0}\r\n", string.Join(", ", programme.Genres));
            builder.AppendFormat("Credits:           {0}\r\n", string.Join(", ", programme.Credits));
            builder.AppendFormat("PreviouslyShown:   {0:G}\r\n", programme.PreviouslyShown);
            builder.AppendFormat("CopyrightDate:     {0:G}\r\n", programme.CopyrightDate);
            builder.AppendFormat("IsRepeat:          {0}\r\n", programme.IsRepeat);
            builder.AppendLine("-------------------------------------------------------");
            return builder.ToString();
        }
    }
}
