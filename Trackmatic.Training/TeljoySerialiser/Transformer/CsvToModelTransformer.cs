using System.Collections.Generic;
using System.IO;
using System.Linq;
using TeljoySerialiser.Model;

namespace TeljoySerialiser.Transformer
{
    public class CsvToModelTransformer
    {
        public Stops Transform(string path)
        {
            return Translate(ReadFile(path));
        }
        private List<Item> ReadFile(string path)
        {
            var lines = File.ReadAllLines(path).ToList();
            var items = new List<Item>();
            foreach (var line in lines)
            {
                items.Add(
                    new Item
                    {
                        Id = line.Split('\t')[0],
                        CusNo = line.Split('\t')[1],
                        CustomerName = line.Split('\t')[2],
                        CellNumber = line.Split('\t')[3],
                        Address = line.Split('\t')[4],
                        Lattitude = line.Split('\t')[5],
                        Longitude = line.Split('\t')[6],
                        DocNr = line.Split('\t')[7],
                        ProdCode = line.Split('\t')[8],
                        Description = line.Split('\t')[9],
                        SerialNr = line.Split('\t')[10],
                        MovementType = line.Split('\t')[11],
                        ActionDate = line.Split('\t')[12],
                        DeliveryInstructions = line.Split('\t')[13]
                    }
                    );
            }
            return items;
        }
        private Stops Translate(List<Item> Items)
        {
            var file = File.ReadAllLines("Models.csv");
            var modelDIctionary = new Dictionary<string, string>();
            file.ToList().ForEach(x => modelDIctionary.Add(x.Split(',')[0], x.Split(',')[1]));
            Items.RemoveAt(0);
            var consignmentStops = new Stops()
            {
                SiteName = "Teljoy Midrand",
                SiteReference = "516",
                Actions = new List<Action>(),
                Consignees = new List<Consignee>(),
                Consignors = new List<Consignor>()
            };
            foreach (var itemGroup in Items.GroupBy(x => x.DocNr).ToList())
            {
                consignmentStops.Consignees.AddRange(itemGroup.Select(x => new Consignee
                {
                    Id = x.CusNo,
                    Name = x.CustomerName,
                    Address = new Address
                    {
                        StreetName = itemGroup.First().Address.Split(',')[0],
                        Suburb = itemGroup.First().Address.Split(',')[1],
                        City = itemGroup.First().Address.Split(',')[2],
                        Latitude = double.Parse(itemGroup.First().Longitude),
                        Longitude = double.Parse(itemGroup.First().Lattitude)
                    },
                    Contacts = new List<Contact>
                   {
                       new Contact
                       {
                           FirstName = itemGroup.First().GetFirstName(),
                           LastName = itemGroup.First().GetLastName(),
                           Reference = itemGroup.First().CusNo,
                           Mobile = itemGroup.First().CellNumber,
                           ComEmail = false,
                           ComSms = false
                       }
                    },
                    FixedMst = 10,
                    VariableMSt = 10,
                    IsAdHoc = true
                }));
                consignmentStops.Consignors.Add(
                    new Consignor
                    {
                        Id = "197100978407",
                        Name = "Filmfun Holdings (Pty) Ltd",
                        IsAdHoc = false
                    }
                    );
                consignmentStops.Actions.Add(
                    new Action
                    {
                        ConsigneeID = itemGroup.First().CusNo,
                        ConsignorID = "197100978407",
                        Type = itemGroup.First().MovementType.Equals("Collection") ? "Pickup" : "Dropoff",
                        Nature = itemGroup.First().MovementType,
                        ActionId = itemGroup.First().DocNr,
                        ActionReference = itemGroup.First().DocNr,
                        IsCod = false,
                        SpecialInstructions = string.Join("; ", itemGroup.Select(x => x.DeliveryInstructions)),
                        Dates = new Dates
                        {
                            DueAt = itemGroup.First().ActionDate
                        },
                        Properties = new Properties
                        {
                            FixedMst = 10,
                            VariableMSt = 10,
                            Dimensions = new Dimensions()
                        },
                        Products = itemGroup.Select(x => new Product
                        {
                            IsSpecified = "false",
                            Barcode = x.SerialNr,
                            ProductCode = x.ProdCode,
                            ProductModel = modelDIctionary.First(y=> y.Key.Equals(x.SerialNr, System.StringComparison.InvariantCultureIgnoreCase)).Value,
                            Description = x.Description,
                            InternalReference = x.Id,
                            Properties = new Properties
                            {
                                Units = 1,
                                Dimensions = new Dimensions()
                           }
                        }).ToList(),

                    }
                    );
            };
            consignmentStops.Consignees = consignmentStops.Consignees.GroupBy(x=> x.Id).Select(x=> x.First()).ToList();
            return consignmentStops;
        }

    }

}
