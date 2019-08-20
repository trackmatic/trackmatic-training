using System;
using System.Collections.Generic;
using System.Linq;
using TeljoySerialiser.Model;
using TeljoySerialiser.Site;
using Trackmatic.Client.Routes.Integration.Data;
using Trackmatic.Client.Routes.Integration.Data.Requirements;
using Trackmatic.Ddd;
using Action = TeljoySerialiser.Model.Action;

namespace TeljoySerialiser.Transformer
{
    public class UploadModelTransformer
    {
        private readonly SiteData _site;
        private readonly Stops _stops;
        private Dictionary<string, List<string>> _entityContactIntegrationKeys = new Dictionary<string, List<string>>();

        public UploadModelTransformer(SiteData site, Stops stops)
        {
            _site = site;
            _stops = stops;
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
                Entities = CreateEntityData(),
                Locations = CreateLocationData(),
                EntityContacts = CreateEntityContactData(),
                Consignments = CreateConsignmentData(),
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
                    Update = "Accept"
                },
                EntityContact = new DataOwnershipAreaData()
                {
                    Create = "Accept",
                    Delete = "Ignore",
                    Update = "Accept"
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
                    Update = "Accept"
                },
                ShippingAddress = new DataOwnershipAreaData()
                {
                    Create = "Accept",
                    Delete = "Ignore",
                    Update = "Accept"
                },
                TravelPlan = new DataOwnershipAreaData()
                {
                    Create = "Accept",
                    Delete = "Ignore",
                    Update = "Accept"
                }
            };
        }
        #endregion

        #region Consignment
        private List<ConsignmentData> CreateConsignmentData()
        {
            var data = new List<ConsignmentData>();
            foreach (var consignment in _stops.Actions)
            {
                if (consignment.Type == "Pickup")
                {
                    data.Add(CreatePickupData(consignment));
                }
                else
                {
                    data.Add(CreateDropOffData(consignment));
                }
            }
            return data;
        }

        private ConsignmentData CreatePickupData(Action consignment)
        {
            var consignee = _stops.Consignees.First(x => x.Id.Equals(consignment.ConsigneeID));
            var consignmentData = new ConsignmentData()
            {
                IntegrationKey = Utils.RemoveIllegalChars(consignment.ActionId),
                Reference = Utils.RemoveIllegalChars(consignment.ActionReference),
                ConsigneeEntityIntegrationKey = consignee.Id,
                ConsignorEntityIntegrationKey = "Teljoy",
                AccessGroups = new List<string>(),
                Pickups = new List<PickupData>()
                {
                    new PickupData()

                    {
                        EntityContactIntegrationKeys = _entityContactIntegrationKeys.First(x=> x.Key.Equals(consignee.Id)).Value,
                        Reference = Utils.RemoveIllegalChars(consignment.ActionReference),
                        InternalReference = consignment.ActionInternalReferemce,
                        MaximumServiceTime = Utils.DetermineMst(consignment),
                        ExpectedCompletionDate = DateTime.Parse(consignment.Dates.DueAt).ToUniversalTime(),
                        EntityIntegrationKey = consignee.Id,
                        ShippingAddressIntegrationKey = consignee.Id,
                        IntegrationKey = Utils.RemoveIllegalChars(consignment.ActionId),
                        DueAt = DateTime.Parse(consignment.Dates.DueAt).ToUniversalTime(),
                        TimerType = "Up",
                        HandlingUnits = CreateConsignmentHandlingUnitsData(consignment.Products),
                        SpecialInstructions = consignment.SpecialInstructions,
                        Financial = new FinancialData()
                        {
                            AmountExcl = Convert.ToDecimal(consignment.Properties.AmountExcl),
                            AmountIncl = Convert.ToDecimal(consignment.Properties.AmountIncl)
                        },
                        Dimensions = new DimensionsData()
                        {
                            Volume = new VolumesData()
                            {
                                Volume = (decimal)consignment.Properties.Volume
                            },
                            VolumetricMass = (decimal)consignment.Properties.Volume,
                            Pieces = consignment.Properties.Units,
                            Pallets = (decimal)consignment.Properties.Pallets,
                            Weight = (decimal)consignment.Properties.Pallets
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
                            //HandlingUnitDebriefs = new List<HandlingUnitDebriefData>
                            //{
                            //    new HandlingUnitDebriefData
                            //    {
                            //        IntegrationKey = "HUD",
                            //        Label = "Handling unit Debrief"
                            //    }
                            //},
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

        private ConsignmentData CreateDropOffData(Action consignment)
        {
            var consignmentData = new ConsignmentData()
            {
                IntegrationKey = Utils.RemoveIllegalChars(consignment.ActionId),
                Reference = Utils.RemoveIllegalChars(consignment.ActionReference),
                ConsigneeEntityIntegrationKey = Utils.RemoveIllegalChars(consignment.ConsigneeID),
                ConsignorEntityIntegrationKey = "Teljoy",
                AccessGroups = new List<string>(),
                Dropoffs = new List<DropoffData>()
                {
                    new DropoffData()
                    {

                        EntityContactIntegrationKeys = _entityContactIntegrationKeys.First(x=> x.Key.Equals(consignment.ConsigneeID)).Value,
                        Reference = Utils.RemoveIllegalChars(consignment.ActionReference),
                        InternalReference = consignment.ActionInternalReferemce,
                        MaximumServiceTime = Utils.DetermineMst(consignment),
                        ExpectedCompletionDate = DateTime.Parse(consignment.Dates.DueAt).ToUniversalTime(),
                        EntityIntegrationKey = Utils.RemoveIllegalChars(consignment.ConsigneeID),
                        ShippingAddressIntegrationKey = Utils.RemoveIllegalChars(consignment.ConsigneeID),
                        IntegrationKey = Utils.RemoveIllegalChars(consignment.ActionId),
                        DueAt = DateTime.Parse(consignment.Dates.DueAt).ToUniversalTime(),
                        TimerType = "Up",
                        HandlingUnits = CreateConsignmentHandlingUnitsData(consignment.Products),
                        SpecialInstructions = consignment.SpecialInstructions,
                        Financial = new FinancialData()
                        {
                            AmountExcl = Convert.ToDecimal(consignment.Properties.AmountExcl),
                            AmountIncl = Convert.ToDecimal(consignment.Properties.AmountIncl)
                        },
                        Dimensions = new DimensionsData()
                        {
                            Volume = new VolumesData()
                            {
                                Volume = (decimal)consignment.Properties.Volume
                            },
                            VolumetricMass = (decimal)consignment.Properties.Volume,
                            Pieces = consignment.Properties.Units,
                            Pallets = (decimal)consignment.Properties.Pallets,
                            Weight = (decimal)consignment.Properties.Pallets
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
                            //HandlingUnitDebriefs = new List<HandlingUnitDebriefData>
                            //{
                            //    new HandlingUnitDebriefData
                            //    {
                            //        IntegrationKey = "HUD",
                            //        Label = "Handling unit Debrief"
                            //    }
                            //},
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
        private List<HandlingUnitData> CreateConsignmentHandlingUnitsData(List<Product> handlingUnits)
        {
            var handlingUnitData = new List<HandlingUnitData>();
            foreach (var handlingUnit in handlingUnits)
            {
                handlingUnitData.Add(new HandlingUnitData()
                {
                    IntegrationKey = !handlingUnit.Equals("")? Utils.RemoveIllegalChars(handlingUnit.Barcode).Replace(" ", "_") : Utils.RemoveIllegalChars(handlingUnit.InternalReference).Replace(" ", "_"),
                    Barcode = handlingUnit.Barcode,
                    Reference = handlingUnit.InternalReference,
                    Description = handlingUnit.Description,
                    Code = handlingUnit.ProductCode,
                    Model = handlingUnit.ProductModel,

                    Financial = new FinancialData()
                    {
                        AmountExcl = Convert.ToDecimal(handlingUnit.Properties.AmountExcl),
                        AmountIncl = Convert.ToDecimal(handlingUnit.Properties.AmountIncl)
                    },
                    Dimensions = new DimensionsData()
                    {
                        Pieces = Convert.ToInt32(handlingUnit.Properties.Units),
                        Weight = Convert.ToDecimal(handlingUnit.Properties.Weight),
                        Volume = new VolumesData()
                        {
                            Height = Convert.ToDecimal(handlingUnit.Properties.Dimensions.Height),
                            Length = Convert.ToDecimal(handlingUnit.Properties.Dimensions.Length),
                            Width = Convert.ToDecimal(handlingUnit.Properties.Dimensions.Width),
                            Volume = Convert.ToDecimal(handlingUnit.Properties.Volume)
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
                CreateConsignorEntity()
            };
            data.AddRange(CreateConsigneeEntity());
            return data;
        }

        private List<EntityData> CreateConsigneeEntity()
        {
            var consignees = new List<EntityData>();
            foreach (var entity in _stops.Consignees)
            {
                consignees.Add(new EntityData
                {
                    IntegrationKey = Utils.RemoveIllegalChars(entity.Id),
                    Name = entity.Name,
                    IsAdhoc = entity.IsAdHoc,
                    Reference = entity.Id
                });
            }
            return consignees;
        }

        private EntityData CreateConsignorEntity()
        {
            var consignor = new EntityData()
            {
                IntegrationKey = $"Teljoy",
                Name = $"Teljoy",
                IsAdhoc = true,
                Reference = $"Teljoy"
            };
            return consignor;
        }
        #endregion

        #region Entity Contact Data
        private List<EntityContactData> CreateEntityContactData()
        {
            var data = new List<EntityContactData>();
            foreach (var entity in _stops.Consignees)
            {
                var contacts = new List<string>();
                foreach (var contact in entity.Contacts)
                {
                    var entityContactData = CreateEntityContact(contact);
                    data.Add(entityContactData);
                    contacts.Add(entityContactData.IntegrationKey);
                }
                _entityContactIntegrationKeys.Add(entity.Id, contacts);
            }
            return data;
        }

        private EntityContactData CreateEntityContact(Contact contact)
        {
            var data = new EntityContactData()
            {
                IntegrationKey = IdentityGenerator.NewTinySequentialId(),
                Email = contact.Email,
                Work = contact.TelWork,
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
            return CreateConsigneeLocation();
        }

        private List<LocationData> CreateConsigneeLocation()
        {
            var locations = new List<LocationData>();
            foreach (var entity in _stops.Consignees)
            {
                var lat = Convert.ToDouble(entity.Address.Longitude);
                var lon = Convert.ToDouble(entity.Address.Latitude);
                locations.Add(
                    new LocationData()
                    {
                        IntegrationKey = Utils.RemoveIllegalChars(entity.Id),
                        IsAdhoc = true,
                        Reference = entity.Id,
                        Name = entity.Name,
                        Entrance = new double[] { lon, lat },
                        Marker = new double[] { lon, lat },
                        Markers = new List<double[]>()
                {
                    new double[] { lon, lat }
                },
                        Size = 100
                    }
                    );
            }
            return locations;
        }

        private LocationData CreateConsignorLocation()
        {
            var consignor = new LocationData()
            {
                IntegrationKey = "Teljoy",
                Reference = "Teljoy",
                Name = "Teljoy",
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
            return CreateConsigneeShippingAddressData();
        }

        private List<ShippingAddressData> CreateConsigneeShippingAddressData()
        {
            var shippingAddresses = new List<ShippingAddressData>();
            foreach (var entity in _stops.Consignees)
            {
                shippingAddresses.Add(new ShippingAddressData()
                {
                    IntegrationKey = Utils.RemoveIllegalChars(entity.Id),
                    LocationIntegrationKey = entity.Id,
                    IsAdhoc = true,
                    Name = entity.Name,
                    Reference = entity.Id,
                    BuildingName = entity.Address.BuildingName,
                    City =entity.Address.City,
                    MapCode =entity.Address.MapReference,
                    PostalCode =entity.Address.PostalCode,
                    Province =entity.Address.Province,
                    Street =entity.Address.StreetName,
                    StreetNo =entity.Address.StreetNo,
                    SubDivisionNumber =entity.Address.SubDivisionNumber,
                    Suburb =entity.Address.Suburb,
                    UnitNo =entity.Address.UnitNo
                });
            }
            return shippingAddresses;
        }
        #endregion
    }
}
