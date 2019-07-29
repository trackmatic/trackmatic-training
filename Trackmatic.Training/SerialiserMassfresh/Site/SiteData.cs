using System.Collections.Generic;

namespace Massfresh.Site
{
    public class SiteData
    {
        public string SiteId { get; set; }
        public string ApiKey { get; set; }
        public List<string> PickupTypes { get; set; }
        public List<string> DropoffTypes { get; set; }

        public static SiteData GetSites()
        {
            return new SiteData()
            {
                SiteId = "cn99tka060ns5cqy4al2h8d41", // Loads Testing JHB site ID.
                ApiKey = "20t3yoom909s6b8fa1ymyu198", // Loads Testing api key.
                PickupTypes = new List<string> { "collection" },
                DropoffTypes = new List<string> { "delivery" }
            };
        }
    }
}
