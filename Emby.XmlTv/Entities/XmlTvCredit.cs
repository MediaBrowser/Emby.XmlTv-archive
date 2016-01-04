using System.Web.UI.WebControls;

namespace Emby.XmlTv.Entities
{
    public class XmlTvCredit
    {
        public XmlTvCreditType Type { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name} - ({Type})";
        }
    }
}
