using System;
using System.Collections.Generic;
using System.Linq;
using Trackmatic.Excel;
using Trackmatic.Rest.Batch;
using Trackmatic.Rest.Batch.Queries;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Routing.Model;

namespace LoadVectorEntityAdn
{
    public class EntityLookup
    {
        private readonly string _mainClientId;
        private List<EntityAndContactModel> Entities = new List<EntityAndContactModel>();
        private List<string> CompareId = new List<string>();

        public EntityLookup(string mainClientId, List<string> comapareId)
        {
            _mainClientId = mainClientId;
            CompareId = comapareId;

        }

        public List<EntityAndContactModel> PullData()
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
            if (loadedEntity != null && loadedEntity.Reference != null && loadedEntity.Contacts != null)
            {
                if (CompareId.Contains(loadedEntity.Reference.Replace(" ", string.Empty)))
                {
                    Entities.Add(new EntityAndContactModel(loadedEntity.Name, loadedEntity.Reference, loadedEntity.Contacts.ToList(), _mainClientId, loadedEntity.Decos.ToList())); 
                }
            }
        }
        private Api CreateLogin(string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, "9807235061081");
            api.Authenticate("9CR_SRR3");
            return api;
        }
    }
}
