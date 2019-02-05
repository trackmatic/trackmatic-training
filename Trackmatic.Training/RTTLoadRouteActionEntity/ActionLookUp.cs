using System;
using System.Collections.Generic;
using Trackmatic.Rest.Batch;
using Trackmatic.Rest.Batch.Queries;
using Trackmatic.Rest.Core;
using Action = Trackmatic.Rest.Routing.Model.Action;

namespace RTTLoadRouteActionEntity
{
    class ActionLookUp
    {
        public ActionLookUp(string clientId, List<string> actionIds)
        {
            ClientId = clientId;
            ActionIds = actionIds;
        }

        public string ClientId { get; set; }
        public List<string> ActionIds { get; set; }
        private List<Action> Actions = new List<Action>();
        public List<Action> PullData()
        {
            var from = DateTime.Now.AddDays(-5);
            var api = CreateLogin(ClientId);
            var batch = new BatchQuery<Action>(new BatchOptions
            {
                Write = 512,
                Read = 512
            }, api, new LoadActionsInBatches(from, DateTime.Now), ActionLoad);
            batch.Execute();
            return Actions;

        }

        private void ActionLoad(Api api, Action loadedAction)
        {
            if (loadedAction != null)
            {
                if (ActionIds.Contains(loadedAction.Id))
                {
                    Actions.Add(loadedAction); 
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
