using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml;

using MediaBrowser.Controller.LiveTv;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.LiveTv;

using Patterns.Logging;

namespace Emby.XmlTv.Classes
{
    // Reads an XmlTv file
    public class XmlTvReader
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlTvReader"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public XmlTvReader(ILogger logger)
        {
            _logger = logger;
        }

        private readonly string _fileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlTvReader"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public XmlTvReader(string fileName)
        {
            _fileName = fileName;
        }
        
        /// <summary>
        /// Gets the channels.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NameIdPair> GetChannels()
        {
            var reader = new XmlTextReader(_fileName);

            if (reader.ReadToDescendant("tv"))
            {
                if (reader.ReadToDescendant("channel"))
                {
                    do
                    {
                        var channel = GetChannel(reader);
                        if (channel != null)
                        {
                            yield return channel;
                        }
                    }
                    while (reader.ReadToFollowing("channel"));
                }
            }
        }

        public NameIdPair GetChannel(XmlReader reader)
        {
            var id = reader.GetAttribute("id");
            
            if (string.IsNullOrEmpty(id))
            {
                _logger.Error("No id found for channel row");
                // Log.Error("  channel#{0} doesnt contain an id", iChannel);
                return null;
            }

            string displayName;
            if (reader.ReadToDescendant("display-name"))
            {
                displayName = reader.ReadElementContentAsString();
            }
            else
            {
                _logger.Error($"No display-name found for channel {id}");
                return null;
            }

            if (string.IsNullOrEmpty(displayName))
            {
                _logger.Error($"No display-name found for channel {id}");
                // Log.Error("  channel#{0} xmlid:{1} doesnt contain an displayname", iChannel, id);
                return null;
            }

            return new NameIdPair() { Id = id, Name = displayName };
        }

        public IEnumerable<ProgramInfo> GetProgrammes(
            ListingsProviderInfo info,
            string channelNumber,
            string channelName,
            DateTime startDateUtc,
            DateTime endDateUtc,
            CancellationToken cancellationToken)
        {
            var reader = new XmlTextReader(_fileName);

            if (reader.ReadToDescendant("tv"))
            {
                if (reader.ReadToDescendant("programme"))
                {
                    do
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            continue; // Break out
                        }

                        var programme = GetProgramme(reader, channelNumber, startDateUtc, endDateUtc);
                        if (programme != null)
                        {
                            yield return programme;
                        }
                    }
                    while (reader.ReadToFollowing("programme"));
                }
            }
        }

        public ProgramInfo GetProgramme(XmlReader reader, string channelNumber, DateTime startDateUtc, DateTime endDateUtc)
        {
            
            var result = new ProgramInfo();

            PopulateHeader(reader, result);

            // First up, validate that this is the correct channel, and programme is within the time we are expecting
            if (string.IsNullOrEmpty(result.ChannelId) || result.ChannelId != channelNumber)
            {
                return null;
            }

            if (result.StartDate < startDateUtc || result.StartDate >= endDateUtc)
            {
                return null;
            }

            var xmlProg = reader.ReadSubtree();
            xmlProg.ReadStartElement(); // now, xmlProg is positioned on the first sub-element of <programme>

            // Read out the data for each node and process individually
            while (!xmlProg.EOF)
            {
                if (xmlProg.NodeType == XmlNodeType.Element)
                {
                    switch (xmlProg.Name)
                    {
                        case "title":
                            ProcessTitleNode(xmlProg, result);
                            break;
                        case "category":
                            ProcessCategory(xmlProg, result);
                            break;
                        case "desc":
                            ProcessDescription(xmlProg, result);
                            break;
                        case "sub-title":
                            ProcessSubTitle(xmlProg, result); 
                            break;
                        case "previously-shown":
                            ProcessPreviouslyShown(xmlProg, result);
                            break;
                        case "episode-num":
                            ProcessEpisodeNum(xmlProg, result);
                            break;
                        //case "date": // Copyright date
                        //    xmlProg.Skip();
                        //    break;
                        //case "star-rating": // Community Rating
                        //    ProcessStarRating(xmlProg, result);
                        //    break;
                        //case "rating": // Certification Rating
                        //    xmlProg.Skip();
                        //    break;
                        //case "credits":
                        //    ProcessCredits(xmlProg, result);
                        //    break;
                        default:
                            // unknown, skip entire node
                            xmlProg.Skip();
                            break;
                    }
                }
                else
                    xmlProg.Read();
            }

            xmlProg.Close();
            return result;
        }

        public void ProcessCredits(XmlReader xmlProg, ProgramInfo result)
        {
            //var creditsXml = xmlProg.ReadSubtree();
            //creditsXml.ReadStartElement();

            //while (!xmlProg.EOF)
            //{
                
            //}
            xmlProg.Skip();
        }

        public void ProcessStarRating(XmlReader reader, ProgramInfo result)
        {
            reader.Skip();
        }

        public void ProcessEpisodeNum(XmlReader reader, ProgramInfo result)
        {
            /*
            <episode-num system="dd_progid">EP00003026.0666</episode-num>
            <episode-num system="onscreen">2706</episode-num>
            <episode-num system="xmltv_ns">.26/0.</episode-num>
            */

            var episodeSystem = reader.GetAttribute("system");
            if (!String.IsNullOrEmpty(episodeSystem))
            {
                if (episodeSystem == "xmltv_ns")
                {
                    ParseEpisodeDataForXmlTvNs(reader, result);
                }
                else if (episodeSystem == "onscreen")
                {
                    ParseEpisodeDataForOnScreen(reader, result);
                }
            }
            else
            {
                reader.Skip();
            }
        }

        public void ParseEpisodeDataForOnScreen(XmlReader reader, ProgramInfo result)
        {
            //// example: 'Episode #FFEE' 
            //serEpNum = ConvertHTMLToAnsi(nodeEpisodeNum);
            //int num1 = serEpNum.IndexOf("#", 0);
            //if (num1 < 0) num1 = 0;
            //episodeNum = CorrectEpisodeNum(serEpNum.Substring(num1, serEpNum.Length - num1), 0);

            var value = reader.ReadElementContentAsString();
            // value = HttpUtility.HtmlDecode(value);
            value = value.Replace(" ", "");

            var hashIndex = value.IndexOf("#", StringComparison.Ordinal);
            if (hashIndex > -1)
            {
                // Take everything from the hash to the end.
                //TODO: This could be textual - how do we populate an Int32
                // result.EpisodeNumber
            }
        }

        public void ParseEpisodeDataForXmlTvNs(XmlReader reader, ProgramInfo result)
        {
            var value = reader.ReadElementContentAsString();
            // value = HttpUtility.HtmlDecode(value);
            value = value.Replace(" ", "");

            // Episode
            var components = value.Split(new[] { "." }, StringSplitOptions.None);
            if (!String.IsNullOrEmpty(components[1]))
            {
                // Handle either "5/12" or "5"
                var episodeComponents = components[1].Split(new [] { "/" }, StringSplitOptions.None);
                result.EpisodeNumber = Int32.Parse(episodeComponents[0]) + 1; // handle the zero basing!
            }
        }

        public void ProcessPreviouslyShown(XmlReader reader, ProgramInfo result)
        {
            // <previously-shown start="20070708000000" />
            var value = reader.ReadElementContentAsString();
            if (!String.IsNullOrEmpty(value))
            {
                // TODO: this may not be correct = validate it
                result.OriginalAirDate = ParseDate(value);
                if (result.OriginalAirDate != result.StartDate)
                {
                    result.IsRepeat = true;
                }
            }
        }

        public void ProcessCategory(XmlReader reader, ProgramInfo result)
        {
            /*
            <category lang="en">News</category>
            */

            result.Genres = result.Genres ?? new List<string>();
            result.Genres.Add(reader.ReadElementContentAsString());
        }

        public void ProcessSubTitle(XmlReader reader, ProgramInfo result)
        {
            /*
            <sub-title lang="en">Gino&apos;s Italian Escape - Islands in the Sun: Southern Sardinia Celebrate the Sea</sub-title>
            <sub-title lang="en">8782</sub-title>
            */
            result.EpisodeTitle = reader.ReadElementContentAsString();
        }

        public void ProcessDescription(XmlReader reader, ProgramInfo result)
        {

            result.ShortOverview = reader.ReadElementContentAsString();
        }

        public void ProcessTitleNode(XmlReader reader, ProgramInfo result)
        {
            // <title lang="en">Gino&apos;s Italian Escape</title>
            result.Name = reader.ReadElementContentAsString();
        }
        
        private void PopulateHeader(XmlReader reader, ProgramInfo result)
        {
            result.ChannelId = reader.GetAttribute("channel");

            var startValue = reader.GetAttribute("start");
            if (string.IsNullOrEmpty(startValue))
            {
                // Log.Error("  programme#{0} doesnt contain a start date", iChannel);
                result.StartDate = DateTime.MinValue;
            }
            else
            {
                result.StartDate = ParseDate(startValue);
            }

            
            var endValue = reader.GetAttribute("stop");
            if (string.IsNullOrEmpty(endValue))
            {
                // Log.Error("  programme#{0} doesnt contain an end date", iChannel);
                result.EndDate = DateTime.MinValue;
            }
            else
            {
                result.EndDate = ParseDate(endValue);
            }            
        }

        private DateTime ParseDate(string startValue)
        {
            // TODO: Determine the date format and parse accordingly
            return ParseLongToDate(long.Parse(startValue.Substring(0, 14)));
        }

        public DateTime ParseLongToDate(long ldate)
        {
            try
            {
                if (ldate < 0) return DateTime.MinValue;
                ldate /= 100L;
                var minute = (int)(ldate % 100L);
                ldate /= 100L;
                var hour = (int)(ldate % 100L);
                ldate /= 100L;
                var day = (int)(ldate % 100L);
                ldate /= 100L;
                var month = (int)(ldate % 100L);
                ldate /= 100L;
                var year = (int)ldate;
                return new DateTime(year, month, day, hour, minute, 0, 0);
            }
            catch (Exception) { }
            return DateTime.MinValue;
        }
    }
}