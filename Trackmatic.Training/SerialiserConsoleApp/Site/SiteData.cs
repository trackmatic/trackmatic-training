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
                ApiKey = "20t3yoom909s6b8fa1ymyu198",
            };

            return site;
        }
    }
}
