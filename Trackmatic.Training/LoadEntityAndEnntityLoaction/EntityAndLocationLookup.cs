using System.Collections.Generic;
using Trackmatic.Rest.Batch;
using Trackmatic.Rest.Batch.Queries;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Routing.Model;

namespace LoadEntityAndEntityLoaction
{
    public class EntityAndLocationLookup
    {
        private readonly string _mainClientId;
        private List<EntityAndLocationModel> Entities = new List<EntityAndLocationModel>();

        public EntityAndLocationLookup(string mainClientId)
        {
            _mainClientId = mainClientId;
        }

        public List<EntityAndLocationModel> PullData()
        {
            var api = CreateLogin(_mainClientId);
            var batch = new BatchQuery<Entity>(new BatchOptions
            {
                Write = 512,
                Read = 512
            }, api, new LoadEntitiesInBatches(), EntityLoad);
            batch.Execute();
            return Entities;
        }

        private void EntityLoad(Api api, Entity loadedEntity)
        {
            if (loadedEntity != null)
            {
                foreach (var Deco in loadedEntity.Decos)
                {
                    if (CheckCoord(Deco))
                    {
                        Entities.Add(new EntityAndLocationModel(loadedEntity.Name, loadedEntity.Reference, Deco.Name, Deco.Reference));  
                    }
                }
            }
        }

        private Api CreateLogin(string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, "9807235061081");
            api.Authenticate("9CR_SRR3");
            return api;
        }

        private bool CheckCoord(DecoAlias Deco)
        {
            if (Deco.Entrance == null) return false;
            var lat = Deco.Entrance.Latitude;
            var lon = Deco.Entrance.Longitude;
            if (lat == 0.0 || lon == 0.0) return false;
            else return true;
        }

    }
}
