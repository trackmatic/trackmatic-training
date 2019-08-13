using System.Collections.Generic;

namespace SerialiserConsoleApp.Site
{
    public class SiteData
    {
        public string SiteId { get; set; }
        public string ApiKey { get; set; }
        
        public static SiteData GetSites()
        {
            var site = new SiteData()
            {
                SiteId = "cn99tka060ns5cqy4al2h8d41",
                ApiKey = "a9rjc0yh84ftmqksb8kq2e7vm",
            };

            return site;
        }
    }
}
