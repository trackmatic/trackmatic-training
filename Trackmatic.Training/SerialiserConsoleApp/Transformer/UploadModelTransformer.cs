using System;
using System.Collections.Generic;
using Trackmatic.Client.Routes.Integration.Data;
using Trackmatic.Client.Routes.Integration.Data.Requirements;
using Trackmatic.Ddd;
using SerialiserConsoleApp.Models;
using SerialiserConsoleApp.Site;

namespace SerialiserConsoleApp.Transformer
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
            var data = CreateConsignmentsData();
            return data;
        }

        public UploadRouteConsignmentsData CreateConsignmentsData()
        {
            return new UploadRouteConsignmentsData()
            {
                DataOwnership = CreateOwnershipData(),
                Consignments = CreateConsignmentData(),
                Entities = CreateEntityData(),
                Locations = CreateLocationData(),
                EntityContacts = CreateEntityContactData(),
                ShippingAddresses = CreateShippingAddressData(),
                Publish = true
            };
        }

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
                    Update = "Ignore"
                },
                Load = new DataOwnershipAreaData()
                {
                    Create = "Accept",
                    Delete = "Ignore",
                    Update = "Ignore"
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

        #region Consignment
        private List<ConsignmentData> CreateConsignmentData()
        {
            var data = new List<ConsignmentData>();
            foreach (var consignment in _stop.Consignments)
            {
                   data.Add(CreateDropOffData(consignment));
            }
            return data;
        }

        private ConsignmentData CreatePickupData(Consignment consignment)
        {
            var consignmentData = new ConsignmentData()
            {
                IntegrationKey = Utils.RemoveIllegalChars(consignment.Reference),
                Reference = Utils.RemoveIllegalChars(consignment.Reference),
                ConsigneeEntityIntegrationKey = _stop.Consignee.Reference,
                ConsignorEntityIntegrationKey = _stop.Consignor.Reference,
                AccessGroups = new List<string>(),
                Pickups = new List<PickupData>()
                {
                    new PickupData()
                    {
                        EntityContactIntegrationKeys = _entityContactIntegrationKeys,
                        Reference = Utils.RemoveIllegalChars(consignment.Reference),
                        InternalReference = consignment.InternalReference,
                        MaximumServiceTime = Utils.DetermineMst(_stop),
                        ExpectedCompletionDate = consignment.DueAtDateTime.ToUniversalTime(),
                        EntityIntegrationKey = _stop.Consignee.Reference,
                        ShippingAddressIntegrationKey = _stop.Consignee.Reference,
                        IntegrationKey = Utils.RemoveIllegalChars(consignment.Reference),
                        DueAt = consignment.DueAtDateTime.ToUniversalTime(),
                        TimerType = "Up",
                        HandlingUnits = CreateConsignmentHandlingUnitsData(consignment.HandlingUnits),
                        SpecialInstructions = consignment.SpecialInstructions,
                        Financial = new FinancialData()
                        {
                            AmountExcl = Convert.ToDecimal(consignment.AmountEx),
                            AmountIncl = Convert.ToDecimal(consignment.AmountInc)
                        },
                        Dimensions = new DimensionsData()
                        {
                            Volume = new VolumesData()
                            {
                                Volume = Utils.CheckDecimalEmpty(consignment.Volume)
                            },
                            VolumetricMass = Utils.CheckDecimalEmpty(consignment.Volume),
                            Pieces = Utils.CheckIntEmpty(consignment.Pieces),
                            Pallets = Convert.ToDecimal(consignment.Pallets),
                            Weight = Utils.CheckDecimalEmpty(consignment.Weight)
                        },

                        Requirements = new RequirementsData()
                        {
                            SuccessSequence = new List<string> { "odometer", "actiondebrief", "signature", "image", "servicerating" },
                            FailureSequence = new List<string> { "odometer", "image" },
                            OdometerReadings = new List<OdometerReadingData>
                            {
                                new OdometerReadingData
                                {
                                    IntegrationKey = "odometer",
                                    AssetIntegrationKey = null,
                                    AssetType = "Vehicle",
                                    Validate = false,
                                    Label = "Opening Odometer",
                                    IsOptional = false,
                                    AutoPopulateAsset = false
                                }
                            },
                            ActionDebriefs = new List<ActionDebriefData>
                            {
                                new ActionDebriefData
                                {
                                    IntegrationKey = "actiondebrief",
                                    Label ="Action Debrief",
                                    AllowPartialFailure = true
                                }
                            },
                            Signatures = new List<SignatureData>
                            {
                                new SignatureData
                                {
                                    IntegrationKey = "signature",
                                    Label = "Signature",
                                    IsOptional = false
                                }
                            },
                            Images = new List<ImageData>
                            {
                                new ImageData
                                {
                                    IntegrationKey = "image",
                                    MaxImages = 3,
                                    Label = "Photo of service",
                                    IsOptional = false
                                }
                            },
                            ServiceRatings = new List<ServiceRatingData>
                            {
                                new ServiceRatingData
                                {
                                    IntegrationKey = "servicerating",
                                    Type = "star",
                                    Label = "Service Rating",
                                    IsOptional = false
                                }
                            }
                        }
                    }
                },
                Dropoffs = new List<DropoffData>() { },
            };
            //switch (consignment.Nature.ToLowerInvariant())
            //{
            //    default:
            //        consignmentData.Reference = $"{Utils.RemoveIllegalChars(consignment.Reference)}_Collection";
            //        consignmentData.IntegrationKey = $"{consignmentData.IntegrationKey}_Col";
            //        break;
            //}
            return consignmentData;
        }

        private ConsignmentData CreateDropOffData(Consignment consignment)
        {
            var consignmentData = new ConsignmentData()
            {
                IntegrationKey = Utils.RemoveIllegalChars(consignment.Reference),
                Reference = Utils.RemoveIllegalChars(consignment.Reference),
                ConsigneeEntityIntegrationKey = _stop.Consignee.Reference,
                ConsignorEntityIntegrationKey = _stop.Consignor.Reference,
                AccessGroups = new List<string>(),
                Dropoffs = new List<DropoffData>()
                {
                    new DropoffData()
                    {
                        EntityContactIntegrationKeys = _entityContactIntegrationKeys,
                        IntegrationKey = $"{Utils.RemoveIllegalChars(consignment.Reference)}",
                        Reference = Utils.RemoveIllegalChars(consignment.Reference),
                        InternalReference = consignment.InternalReference,
                        MaximumServiceTime = Utils.DetermineMst(_stop),
                        ExpectedCompletionDate = consignment.DueAtDateTime.ToUniversalTime(),
                        DueAt = consignment.DueAtDateTime.ToUniversalTime(),
                        EntityIntegrationKey = $"{_stop.Consignee.Reference}",
                        ShippingAddressIntegrationKey = $"{_stop.Consignee.Reference}",
                        TimerType = "Up",
                        HandlingUnits = CreateConsignmentHandlingUnitsData(consignment.HandlingUnits),
                        SpecialInstructions = consignment.SpecialInstructions,
                        Financial = new FinancialData()
                        {
                                AmountExcl = Convert.ToDecimal(consignment.AmountEx),
                                AmountIncl = Convert.ToDecimal(consignment.AmountInc)
                        },
                        Dimensions = new DimensionsData()
                        {
                                Volume = new VolumesData()
                                {
                                    Volume = Utils.CheckDecimalEmpty(consignment.Volume)
                                },
                                VolumetricMass = Utils.CheckDecimalEmpty(consignment.Volume),
                                Pieces = Utils.CheckIntEmpty(consignment.Pieces),
                                Pallets = Convert.ToDecimal(consignment.Pallets),
                                Weight = Utils.CheckDecimalEmpty(consignment.Weight)
                        },
                        Requirements = new RequirementsData()
                        {
                            SuccessSequence = new List<string> { "odometer", "actiondebrief", "signature", "image", "servicerating" },
                            FailureSequence = new List<string>{ "odometer", "image"},
                            OdometerReadings = new List<OdometerReadingData>
                            {
                                new OdometerReadingData
                                {
                                    IntegrationKey = "odometer",
                                    AssetIntegrationKey = null,
                                    AssetType = "Vehicle",
                                    Validate = false,
                                    Label = "Opening Odometer",
                                    IsOptional = false,
                                    AutoPopulateAsset = false
                                }
                            },
                            ActionDebriefs = new List<ActionDebriefData>
                            {
                                new ActionDebriefData
                                {
                                    IntegrationKey = "actiondebrief",
                                    Label = "Action Debrief",
                                    AllowPartialFailure = true
                                }
                            },
                            Signatures = new List<SignatureData>()
                            {
                                new SignatureData()
                                {
                                    IntegrationKey = "signature",
                                    Label = "Signature",
                                    IsOptional = false
                                }
                            },
                            Images = new List<ImageData>()
                            {
                                new ImageData()
                                {
                                    IntegrationKey = "image",
                                    MaxImages = 3,
                                    Label = "Photo of service",
                                    IsOptional = false
                                }
                            },
                            ServiceRatings = new List<ServiceRatingData>()
                            {
                                new ServiceRatingData()
                                {
                                    IntegrationKey = "servicerating",
                                    Type = "star",
                                    Label = "Service Rating",
                                    IsOptional = false
                                }
                            }
                        }
                    }
                },
            };

            return consignmentData;
        }
        #endregion

        #region HandlingUnit Data
        private List<HandlingUnitData> CreateConsignmentHandlingUnitsData(List<HandlingUnit> handlingUnits)
        {
            var handlingUnitData = new List<HandlingUnitData>();
            foreach (var handlingUnit in handlingUnits)
            {
                handlingUnitData.Add(new HandlingUnitData()
                {
                    IntegrationKey = handlingUnit.Barcode,
                    Barcode = handlingUnit.Barcode,
                    Reference = handlingUnit.Reference,
                    CustomerReference = _stop.Consignor.Reference,
                    Description = handlingUnit.Description,
                    Financial = new FinancialData()
                    {
                        AmountExcl = Convert.ToDecimal(handlingUnit.AmountEx),
                        AmountIncl = Convert.ToDecimal(handlingUnit.AmountIncl)
                    },
                    Dimensions = new DimensionsData()
                    {
                        Pieces = Convert.ToInt32(handlingUnit.Pieces),
                        Weight = Convert.ToDecimal(handlingUnit.Weight),
                        Volume = new VolumesData()
                        {
                            Height = Convert.ToDecimal(handlingUnit.Height),
                            Length = Convert.ToDecimal(handlingUnit.Length),
                            Width = Convert.ToDecimal(handlingUnit.Width),
                            Volume = Convert.ToDecimal(handlingUnit.Volume)
                        },
                    }
                });
            }
            return handlingUnitData;
        }
        #endregion

        #region Entity Data
        private List<EntityData> CreateEntityData()
        {
            var data = new List<EntityData>
            {
                CreateConsigneeEntity(),
                CreateConsignorEntity()
            };
            return data;
        }

        private EntityData CreateConsigneeEntity()
        {
            var consignee = new EntityData()
            {
                IntegrationKey = _stop.Consignee.Reference,
                Name = _stop.Consignee.Name,
                IsAdhoc = false,
                Reference = _stop.Consignee.Reference
            };
            return consignee;
        }

        private EntityData CreateConsignorEntity()
        {
            var consignor = new EntityData()
            {
                IntegrationKey = _stop.Consignor.Reference,
                Name = _stop.Consignor.Name,
                IsAdhoc = false,
                Reference = _stop.Consignor.Reference
            };
            return consignor;
        }
        #endregion

        #region Entity Contact Data
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
            var data = new EntityContactData()
            {
                IntegrationKey = IdentityGenerator.NewTinySequentialId(),
                Email = contact.Email,
                Work = contact.Work,
                Mobile = contact.Mobile,
                LastName = contact.LastName,
                FirstName = contact.FirstName,
                IsAdhoc = false
            };
            return data;
        }
        #endregion

        #region Location Data
        private List<LocationData> CreateLocationData()
        {
            var data = new List<LocationData>
            {
                CreateConsigneeLocation()
            };
            return data;
        }

        private LocationData CreateConsigneeLocation()
        {
            var lat = Convert.ToDouble(_stop.Consignee.Position.Latitude);
            var lon = Convert.ToDouble(_stop.Consignee.Position.Longitude);
            var consignee = new LocationData()
            {
                IntegrationKey = _stop.Consignee.Reference,
                IsAdhoc = false,
                Reference = _stop.Consignee.Reference,
                Name = _stop.Consignee.Name,
                Entrance = new double[] { lon, lat },
                Marker = new double[] { lon, lat },
                Markers = new List<double[]>()
                {
                    new double[] { lon, lat }
                },
                Size = 100
            };
            return consignee;
        }

        private LocationData CreateConsignorLocation()
        {
            var consignor = new LocationData()
            {
                IntegrationKey = _stop.Consignor.Reference,
                Reference = _stop.Consignor.Reference,
                Name = _stop.Consignor.Name,
                Entrance = new double[] { 0, 0 },
                Type = LocationTypeData.Radius,
                Marker = new double[] { 0, 0 },
                IsAdhoc = true
            };
            return consignor;
        }
        #endregion

        #region Shipping Address Data
        private List<ShippingAddressData> CreateShippingAddressData()
        {
            var data = new List<ShippingAddressData>
            {
                CreateConsigneeShippingAddressData()
            };
            return data;
        }

        private ShippingAddressData CreateConsigneeShippingAddressData()
        {
            var shippingAddressData = new ShippingAddressData()
            {
                IntegrationKey = _stop.Consignee.Reference,
                LocationIntegrationKey = _stop.Consignee.Reference,
                IsAdhoc = false,
                Name = _stop.Consignee.Name,
                Reference = _stop.Consignee.Reference,
                BuildingName = _stop.Consignee.Address.BuildingName,
                City = _stop.Consignee.Address.City,
                MapCode = _stop.Consignee.Address.MapCode,
                PostalCode = _stop.Consignee.Address.PostalCode,
                Province = _stop.Consignee.Address.Province,
                Street = _stop.Consignee.Address.Street,
                StreetNo = _stop.Consignee.Address.StreetNo,
                SubDivisionNumber = _stop.Consignee.Address.SubDivisionNumber,
                Suburb = _stop.Consignee.Address.Suburb,
                UnitNo = _stop.Consignee.Address.UnitNo
            };
            return shippingAddressData;
        }
        #endregion
    }
}
