using System;
using System.Collections.Generic;
using Trackmatic.Client.Routes.Integration.Data;
using Trackmatic.Client.Routes.Integration.Data.Requirements;
using Massfresh.Models;
using Massfresh.Site;

namespace Massfresh.Transformer
{
    public class UploadModelTransformer
    {
        private readonly SiteData _site;
        private readonly Stop _stop;
        private List<string> _entityContactIntegrationKeys = new List<string>();

        public UploadModelTransformer(SiteData site, Stop stop)
        {
            _site = site;
            _stop = stop;
        }

        public UploadRouteConsignmentsData Transform()
        {
            return CreateConsignmentsData();
        }

        private UploadRouteConsignmentsData CreateConsignmentsData()
        {
            return new UploadRouteConsignmentsData()
            {
                DataOwnership = CreateOwnershipData(),
                Consignments = CreateConsignmentData(), 
                ShippingAddresses = CreateShippingAddressData(), 
                Entities = CreateEntityData(), 
                Locations = CreateLocationData(), 
                EntityContacts = CreateEntityContactData(), 
                Publish = true
            };
        }
        
        #region Consignment Data
        private List<ConsignmentData> CreateConsignmentData()
        {
            var data = new List<ConsignmentData>();
            foreach (var consignment in _stop.Consignments)
            {
                if (_site.PickupTypes.Contains(consignment.Type.ToLower()))
                {
                    data.Add(CreatePickupData(consignment));
                }
                else if (_site.DropoffTypes.Contains(consignment.Type.ToLower()))
                {
                    data.Add(CreateDropOffData(consignment));
                }
            }
            return data;
        }

        private ConsignmentData CreatePickupData(Consignment consignment)
        {
            return new ConsignmentData
            {
                IntegrationKey = consignment.Reference,
                Reference = _stop.Consignee.Reference,
                CargoType = CargoTypeData.Freight,
                ConsignorEntityIntegrationKey = _stop.Consignor.Reference,
                ConsigneeEntityIntegrationKey = _stop.Consignee.Reference,
                Pickups = new List<PickupData>()
                {
                    new PickupData()
                    {
                        IntegrationKey = consignment.Reference,
                        DueAt = consignment.DueAtDateTime.ToUniversalTime(),
                        EntityIntegrationKey = _stop.Consignee.Reference,
                        ShippingAddressIntegrationKey = _stop.Consignee.Reference,
                        EntityContactIntegrationKeys = _entityContactIntegrationKeys,
                        MaximumServiceTime = TimeSpan.FromMinutes(_stop.Consignee.MST),
                        ExpectedCompletionDate = consignment.DueAtDateTime.ToUniversalTime(),
                        SpecialInstructions = consignment.SpecialInstructions,
                        TimerType = "Up",
                        Reference = consignment.Reference,
                        InternalReference = consignment.ReferenceInternal,
                        Requirements = CreateRequirementData(),
                        Dimensions = CreateDimensionData(consignment),
                        Financial = CreateFinancialData(consignment),
                        HandlingUnits = CreateHandlingUnitData(consignment)
                    }
                },
                Dropoffs = new List<DropoffData>() { }
            };
        }

        private ConsignmentData CreateDropOffData(Consignment consignment)
        {
            return new ConsignmentData
            {
                IntegrationKey = consignment.Reference,
                Reference = consignment.Reference,
                CargoType = CargoTypeData.Freight,
                ConsignorEntityIntegrationKey = _stop.Consignor.Reference,
                ConsigneeEntityIntegrationKey = _stop.Consignee.Reference,
                Dropoffs = new List<DropoffData>()
                {
                    new DropoffData()
                    {
                        IntegrationKey = consignment.Reference,
                        DueAt = consignment.DueAtDateTime.ToUniversalTime(),
                        EntityIntegrationKey = _stop.Consignee.Reference,
                        ShippingAddressIntegrationKey = _stop.Consignee.Reference,
                        EntityContactIntegrationKeys = _entityContactIntegrationKeys,
                        MaximumServiceTime = TimeSpan.FromMinutes(_stop.Consignee.MST),
                        ExpectedCompletionDate = consignment.DueAtDateTime.ToUniversalTime(),
                        SpecialInstructions = consignment.SpecialInstructions,
                        TimerType = "Up",
                        Reference = consignment.Reference,
                        InternalReference = consignment.ReferenceInternal,
                        Requirements = CreateRequirementData(),
                        Dimensions = CreateDimensionData(consignment),
                        Financial = CreateFinancialData(consignment),
                        HandlingUnits = CreateHandlingUnitData(consignment)
                    }
                },
                Pickups = new List<PickupData>() { }
            };
        }

        private List<HandlingUnitData> CreateHandlingUnitData(Consignment consignment)
        {
            var handlingUnits = new List<HandlingUnitData>();
            foreach (var handlingUnit in consignment.HandlingUnits)
            {
                handlingUnits.Add(new HandlingUnitData
                {
                    IntegrationKey = handlingUnit.Reference,
                    Reference = handlingUnit.Reference,
                    Description = handlingUnit.Description,
                    Barcode = handlingUnit.Barcode,
                    CustomerReference = handlingUnit.Reference,
                    Financial = new FinancialData
                    {
                        AmountExcl = Convert.ToDecimal(handlingUnit.AmountEx),
                        AmountIncl = Convert.ToDecimal(handlingUnit.AmountIncl)
                    },
                    Dimensions = new DimensionsData
                    {
                        Pieces = handlingUnit.Pieces,
                        Volume = new VolumesData
                        {
                            Volume = Convert.ToDecimal(handlingUnit.Volume),
                            Height = Convert.ToDecimal(handlingUnit.Height),
                            Width = Convert.ToDecimal(handlingUnit.Width),
                            Length = Convert.ToDecimal(handlingUnit.Length),
                        },
                        Weight = Convert.ToDecimal(handlingUnit.Weight)
                    }
                });
            }
            return handlingUnits;
        }


        private FinancialData CreateFinancialData(Consignment consignment)
        {
            return new FinancialData
            {
                AmountExcl = Convert.ToDecimal(consignment.AmountEx),
                AmountIncl = Convert.ToDecimal(consignment.AmountIncl)
            };
        }

        private DimensionsData CreateDimensionData(Consignment consignment)
        {
            return new DimensionsData
            {
                Volume = new VolumesData
                {
                    Volume = Convert.ToDecimal(consignment.Volume)
                },
                Weight = Convert.ToDecimal(consignment.Weight),
                Pallets = Convert.ToDecimal(consignment.Pallets),
                Pieces = consignment.Pieces
            };
        }

        private RequirementsData CreateRequirementData()
        {
            return new RequirementsData
            {
                SuccessSequence = new List<string> { "SOG", "AD" },
                FailureSequence = new List<string> { "SOG", "AD" },
                Signatures = new List<SignatureData>
                {
                    new SignatureData
                    {
                        IntegrationKey = "SOG",
                        Label = "Sign on Glass",
                    }
                },
                ActionDebriefs = new List<ActionDebriefData>
                {
                    new ActionDebriefData
                    {
                        IntegrationKey = "AD",
                        Label = "Action Debrief"
                    }
                }
            };
        }
        #endregion

        #region Shipping Address Data
        private List<ShippingAddressData> CreateShippingAddressData()
        {
            return new List<ShippingAddressData>
            {
                new ShippingAddressData
                {
                    Name = _stop.Consignee.Name,
                    LocationIntegrationKey = _stop.Consignee.Reference,
                    IntegrationKey = _stop.Consignee.Reference,
                    Reference = _stop.Consignee.Reference,
                    UnitNo = _stop.Consignee.Address.UnitNo,
                    BuildingName = _stop.Consignee.Address.BuildingName,
                    StreetNo = _stop.Consignee.Address.StreetNo,
                    SubDivisionNumber = _stop.Consignee.Address.SubDivisionNumber,
                    Street = _stop.Consignee.Address.Street,
                    Suburb = _stop.Consignee.Address.Suburb,
                    City = _stop.Consignee.Address.City,
                    Province = _stop.Consignee.Address.Province,
                    MapCode = _stop.Consignee.Address.MapCode,
                    IsAdhoc = false
                }
            };
        }
        #endregion

        #region Entity Data
        private List<EntityData> CreateEntityData()
        {
            return new List<EntityData>
            {
                CreateEntityConsignee(),
                CreateEntityConsignor()
            };
        }
        
        private EntityData CreateEntityConsignee()
        {
            return new EntityData
            {
                IntegrationKey = _stop.Consignee.Reference,
                Name = _stop.Consignee.Name,
                Reference = _stop.Consignee.Reference,
                IsAdhoc = false
            };
        }

        private EntityData CreateEntityConsignor()
        {
            return new EntityData
            {
                IntegrationKey = _stop.Consignor.Reference,
                Name = _stop.Consignor.Name,
                Reference = _stop.Consignor.Reference,
                IsAdhoc = false
            };
        }
        #endregion

        #region Location Data
        private List<LocationData> CreateLocationData()
        {
            var data = new List<LocationData>();
            data.Add(CreateConsigneeLocationData());
            return data;
        }

        private LocationData CreateConsigneeLocationData()
        {
            var lat = _stop.Consignee.Address.Position.Latitude;
            var lon = _stop.Consignee.Address.Position.Longitude;
            return new LocationData
            {
                IntegrationKey = _stop.Consignee.Reference,
                Type = LocationTypeData.Radius,
                Entrance = new double[] { lon, lat },
                Name = _stop.Consignee.Name,
                Marker = new double[] { lon, lat },
                Size = 100,
                IsAdhoc = false
            };
        }

        private LocationData CreateConsignorLocationData()
        {
            var lat = _stop.Consignor.Address.Position.Latitude;
            var lon = _stop.Consignor.Address.Position.Longitude;
            return new LocationData
            {
                IntegrationKey = _stop.Consignor.Reference,
                Type = LocationTypeData.Radius,
                Entrance = new double[] { lon, lat },
                Name = _stop.Consignor.Name,
                Markers = new List<double[]>()
                {
                    new double[] { lon, lat },
                },
                Marker = new double[] { lon, lat },
                Size = 100,
                IsAdhoc = false
            };
        }
        #endregion

        #region Entity Contacts
        private List<EntityContactData> CreateEntityContactData()
        {
            var data = new List<EntityContactData>();
            foreach (var contact in _stop.Consignee.Contacts)
            {
                var entityContactData = CreateEntityContact(contact);
                data.Add(entityContactData);
                _entityContactIntegrationKeys.Add(entityContactData.IntegrationKey);
            }
            return data;
        }

        private EntityContactData CreateEntityContact(Contact contact)
        {
            return new EntityContactData()
            {
                IntegrationKey = "",//IdentityGenerator.NewTinySequentialId(),
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Mobile = contact.Mobile,
                Work = contact.Work,
                Department = contact.Department,
                Email = contact.Email,
                IsAdhoc = false
            };
        }
        #endregion

        #region Data Ownership
        private DataOwnershipData CreateOwnershipData()
        {
            return new DataOwnershipData()
            {
                Consignment = new DataOwnershipAreaData()
                {
                    Create = "Accept",
                    Delete = "Ignore",
                    Update = "Accept"
                },
                Entity = new DataOwnershipAreaData()
                {
                    Create = "Accept",
                    Delete = "Ignore",
                    Update = "Ignore"
                },
                EntityContact = new DataOwnershipAreaData()
                {
                    Create = "Accept",
                    Delete = "Ignore",
                    Update = "Ignore",
                },
                Load = new DataOwnershipAreaData()
                {
                    Create = "Accept",
                    Delete = "Ignore",
                    Update = "Accept"
                },
                Location = new DataOwnershipAreaData()
                {
                    Create = "Accept",
                    Delete = "Ignore",
                    Update = "Ignore"
                },
                ShippingAddress = new DataOwnershipAreaData()
                {
                    Create = "Accept",
                    Delete = "Ignore",
                    Update = "Ignore"
                },
                TravelPlan = new DataOwnershipAreaData()
                {
                    Create = "Accept",
                    Delete = "Ignore",
                    Update = "Ignore"
                }
            };
        }
        #endregion
    }
}
