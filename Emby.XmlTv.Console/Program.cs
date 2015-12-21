using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Emby.XmlTv.Classes;
using Emby.XmlTv.Console.Classes;

using MediaBrowser.Model.Dto;

namespace Emby.XmlTv.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var filename = @"C:\Program Files (x86)\XMLTV GUI\data.xml";

            if (args.Length == 1 && File.Exists(args[0]))
            {
                filename = args[0];
            }

            var timer = Stopwatch.StartNew();
            System.Console.WriteLine("Running XMLTv Parsing");

            // var resultsFile = $"{Path.GetDirectoryName(filename)}\\{Path.GetFileNameWithoutExtension(filename)}_Results_{DateTime.UtcNow:hhmmss}.txt";
            var resultsFile = $"C:\\Temp\\{Path.GetFileNameWithoutExtension(filename)}_Results_{DateTime.UtcNow:hhmmss}.txt";

            ReadSourceXmlTvFile(filename, resultsFile).Wait();

            System.Console.WriteLine($"Completed in {timer.Elapsed:g} - press any key to open the file...");
            System.Console.ReadKey();

            Process.Start(resultsFile);
        }

        public static async Task ReadSourceXmlTvFile(string filename, string resultsFile)
        {
            System.Console.WriteLine($"Writing to file: {resultsFile}");

            using (var resultsFileStream = new StreamWriter(resultsFile) { AutoFlush = true })
            {
                var reader = new XmlTvReader(filename);
                await ReadOutChannels(reader, resultsFileStream);

                resultsFileStream.Close();
            }
        }

        public static async Task ReadOutChannels(XmlTvReader reader, StreamWriter resultsFileStream)
        {
            foreach (var channel in reader.GetChannels())
            {
                resultsFileStream.Write(channel.GetChannelHeader());
                await ReadOutChannelProgrammes(reader, channel, resultsFileStream);
            }
        }

        private static async Task ReadOutChannelProgrammes(XmlTvReader reader, NameIdPair channel, StreamWriter resultsFileStream)
        {
            //var startDate = new DateTime(2015, 11, 28);
            //var endDate = new DateTime(2015, 11, 29);
            var startDate = DateTime.MinValue;
            var endDate = DateTime.MaxValue;

            foreach (var programme in reader.GetProgrammes(null, channel.Id, null, startDate, endDate, new CancellationToken()))
            {
                await resultsFileStream.WriteLineAsync(programme.GetProgrammeDetail());
            }
        }
    }
}