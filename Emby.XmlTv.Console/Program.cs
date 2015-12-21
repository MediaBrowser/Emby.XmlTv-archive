using System;
using System.IO;
using System.Linq;
using System.Threading;

using Emby.XmlTv.Classes;

using MediaBrowser.Model.Dto;

namespace Emby.XmlTv.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var filename = @"C:\Program Files (x86)\XMLTV GUI\data.xml";

            if (args.Count() == 1 && File.Exists(args[0]))
            {
                filename = args[0];
            }

            var reader = new XmlTvReader(filename);
            ReadOutChannels(reader);

            System.Console.ReadKey();
        }

        public static void ReadOutChannels(XmlTvReader reader)
        {
            foreach (var channel in reader.GetChannels())
            {
                System.Console.WriteLine($"****** START {channel.Id} - {channel.Name} ******");
                ReadOutChannelProgrammes(reader, channel);
                System.Console.WriteLine($"****** END   {channel.Id} - {channel.Name} ******");
            }
        }

        private static void ReadOutChannelProgrammes(XmlTvReader reader, NameIdPair channel)
        {
            var startDate = new DateTime(2015, 11, 28);
            var endDate = new DateTime(2015, 11, 29);

            foreach (var programme in reader.GetProgrammes(null, channel.Id, null, startDate, endDate, new CancellationToken()))
            {
                System.Console.WriteLine($"{programme.Name} - Episode: {programme.EpisodeNumber} - Subtitle: {programme.EpisodeTitle} - Genres: {String.Join(", ", programme.Genres)}");
            }
        }
    }
}