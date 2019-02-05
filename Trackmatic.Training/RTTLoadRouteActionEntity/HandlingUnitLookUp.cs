using System.Collections.Generic;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Routing.Model;
using Trackmatic.Rest.Routing.Requests;

namespace RTTLoadRouteActionEntity
{
    class HandlingUnitLookUp
    {
        public HandlingUnitLookUp(List<string> handlingUnitID, string clientId)
        {
            HandlingUnitID = handlingUnitID;
            ClientId = clientId;

        }
        private List<string> HandlingUnitID { get; set; }
        private string ClientId { get; set; }


        public List<HandlingUnit> PullData()
        {
            var api = CreateLogin(ClientId);
            return api.ExecuteRequest(new LoadHandlingUnits(api.Context, HandlingUnitID)).Data;

        }
        private Api CreateLogin(string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, "9807235061081");
            api.Authenticate("9CR_SRR3");
            return api;
        }
    }
}
