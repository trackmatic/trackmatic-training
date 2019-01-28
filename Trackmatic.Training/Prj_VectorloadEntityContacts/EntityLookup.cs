using System.Collections.Generic;
using System.Linq;
using Trackmatic.Rest.Batch;
using Trackmatic.Rest.Batch.Queries;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Routing.Model;

namespace Prj_VectorloadEntityContacts
{
    public class EntityLookup
    {
        private readonly string _mainClientId;
        private List<EntityAndContactModel> entities = new List<EntityAndContactModel>();

        public List<EntityAndContactModel> Entities
        {
            get { return entities; }
            set { entities = value; }
        }


        public EntityLookup(string mainClientId)
        {
            _mainClientId = mainClientId;
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
            return entities;

        }

        private void EntityLoad(Api api, Entity loadedEntity)
        {
            if (loadedEntity != null)
            {
                Entities.Add(new EntityAndContactModel(loadedEntity.Name, loadedEntity.Reference, loadedEntity.Contacts.ToList()));
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
