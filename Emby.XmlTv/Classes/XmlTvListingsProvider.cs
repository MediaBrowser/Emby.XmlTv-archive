using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediaBrowser.Controller.LiveTv;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.LiveTv;

namespace Emby.XmlTv.Classes
{
    public class XmlTvListingsProvider : IListingsProvider
    {
        public Task<IEnumerable<ProgramInfo>> GetProgramsAsync(
            ListingsProviderInfo info,
            string channelNumber,
            string channelName,
            DateTime startDateUtc,
            DateTime endDateUtc,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task AddMetadata(ListingsProviderInfo info, List<ChannelInfo> channels, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Validate(ListingsProviderInfo info, bool validateLogin, bool validateListings)
        {
            throw new NotImplementedException();
        }

        public Task<List<NameIdPair>> GetLineups(ListingsProviderInfo info, string country, string location)
        {
            throw new NotImplementedException();
        }

        public string Name { get; }

        public string Type { get; }
    }
}
