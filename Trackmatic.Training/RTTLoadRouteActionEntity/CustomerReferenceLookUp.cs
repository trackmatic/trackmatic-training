using System.Collections.Generic;
using Trackmatic.Rest.Routing.Model;

namespace RTTLoadRouteActionEntity
{
    public class CustomerReferenceLookUp
    {
        public CustomerReferenceLookUp(List<HandlingUnit> handlingUnits)
        {
            HandlingUnits = handlingUnits;

        }

        private List<HandlingUnit> HandlingUnits { get; set; }

        public List<string> getCustomerReferences()
        {
            var CustomerRef = new List<string>();
            foreach (var Unit in HandlingUnits)
            {
                CustomerRef.Add(Unit.CustomerReference);
            }
            return CustomerRef;
        }
    }
}
