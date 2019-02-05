using System;
using System.Collections.Generic;
using System.Linq;
using Trackmatic.Excel;
using Action = Trackmatic.Rest.Routing.Model.Action;

namespace RTTLoadRouteActionEntity
{
    class Program
    {
        static void Main(string[] args)
        {
            var clientId = "335";
            var routeIds = new List<string>() { "9568886", "9571244" };
            var fileName = "RTTRouteActionEntity";
            var headings = new List<string>() { "Route Id", "Action Id", "Entity Id" };
            var path = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Temp";
            var collection = new List<string[]>();
            foreach (var routeId in routeIds)
            {
                var refsToQuery = getRouteCustomerReference("RTT Route Infomation", $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Temp");
                var route = new RouteLookUp(routeId, clientId).PullData();
                var handlingIds = route.Actions.SelectMany(x => x.HandlingUnitIds).ToList();
                var handlingUnits = new HandlingUnitLookUp(handlingIds, clientId).PullData();
                var matchingUnits = handlingUnits.Where(x => refsToQuery.Contains(x.CustomerReference)).ToList();
                var actionsIds = matchingUnits.Select(x => x.Allocations.Last().ActionId).ToList();
                var actions = route.Actions.Where(x => actionsIds.Contains(x.Id)).ToList();
                collection.AddRange(CreateCollection(actions, routeId));
            }
            new WriteToFile(fileName, path, headings, collection).Write();
        }

        public static List<string> getRouteCustomerReference(string fileName, string path)
        {
            var routeCustomerRef = new List<string>();
            var openFile = new WriteToExcel();
            openFile.Open(fileName, path, "Sheet1");
            var importCellsRef = openFile.WorkSheet.Cells["B2:B"];
            foreach (var Cell in importCellsRef)
            {
                if (Cell.GetValue<string>() != "" && Cell.GetValue<string>() != null)
                {
                    routeCustomerRef.Add(Cell.GetValue<string>());
                }
            }
            return routeCustomerRef;
        }


        public static List<string[]> CreateCollection(List<Action> Actions, string RouteID)
        {
            var collection = new List<string[]>();
            foreach (var action in Actions)
            {
                collection.Add(new string[] { RouteID, action.Id, action.EntityReference.Id });
            }
            return collection;
        }
    }
}
