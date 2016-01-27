using System;
using System.IO;
using System.Linq;
using System.Threading;

using Emby.XmlTv.Classes;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emby.XmlTv.Test
{
    [TestClass]
    public class XmlTvReaderTests
    {
        [TestMethod]
        [DeploymentItem("Xml Files\\UK_Data.xml")]
        public void UKDataTest()
        {
            var testFile = Path.GetFullPath(@"UK_Data.xml");
            var reader = new XmlTvReader(testFile);

            var channels = reader.GetChannels().ToList();
            Assert.AreEqual(5, channels.Count);

            // Pick a channel to check the data for
            var channel = channels.SingleOrDefault(c => c.Id == "UK_RT_2056" && c.Name == "Channel 4 HD");
            Assert.IsNotNull(channel);

            var startDate = new DateTime(2015, 11, 26);
            var cancellationToken = new CancellationToken();
            var programmes = reader.GetProgrammes(channel.Id, startDate, startDate.AddDays(1), cancellationToken).ToList();

            Assert.AreEqual(27, programmes.Count);
            var programme = programmes.SingleOrDefault(p => p.Name == "The Secret Life of");

            Assert.IsNotNull(programme);
            Assert.AreEqual(new DateTime(2015, 11, 26, 20, 0, 0), programme.StartDate);
            Assert.AreEqual(new DateTime(2015, 11, 26, 21, 0, 0), programme.EndDate);
            Assert.AreEqual("Cameras follow the youngsters' development after two weeks apart and time has made the heart grow fonder for Alfie and Emily, who are clearly happy to be back together. And although Alfie struggled to empathise with the rest of his peers before, a painting competition proves to be a turning point for him. George takes the children's rejection of his family recipe to heart, but goes on to triumph elsewhere, and romance is in the air when newcomer Sienna captures Arthur's heart.", programme.ShortOverview);
            Assert.AreEqual("Documentary", programme.Genres.Single());
            Assert.IsNotNull(programme.Episode);
            Assert.AreEqual("The Secret Life of 5 Year Olds", programme.Episode.Title);
            Assert.AreEqual(1, programme.Episode.Series);
            Assert.IsNull(programme.Episode.SeriesCount);
            Assert.AreEqual(4, programme.Episode.Episode);
            Assert.AreEqual(6, programme.Episode.EpisodeCount);
        }
    }
}
