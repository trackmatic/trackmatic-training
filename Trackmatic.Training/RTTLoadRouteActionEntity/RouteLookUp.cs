using Trackmatic.Rest.Core;
using Trackmatic.Rest.Routing.Model;
using Trackmatic.Rest.Routing.Requests;

namespace RTTLoadRouteActionEntity
{
    public class RouteLookUp
    {
        public RouteLookUp(string routeId, string clientId)
        {
            RouteId = routeId;
            ClientId = clientId;

        }
        private string RouteId { get; set; }
        private string ClientId { get; set; }


        public RouteInstanceWithIncludes PullData()
        {
            var api = CreateLogin(ClientId);
            return  api.ExecuteRequest(new LoadRouteInstanceWithIncludes(api.Context, $"{ClientId}/{RouteId}", EInclude.Actions)).Data;

        }
        private Api CreateLogin(string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, "9807235061081");
            api.Authenticate("9CR_SRR3");
            return api;
        }
    }
}
