using SerialiserConsoleApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SerialiserConsoleApp.Transformer
{
    public class CsvToModelTransformer
    {
        public ConsignmentStops Transform(string path)
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
        private ConsignmentStops Translate(List<Item> Items)
        {
            Items.RemoveAt(0);
            var consignmentStops = new ConsignmentStops() { Stops = new List<Stop>()};
            foreach (var itemGroup in Items.GroupBy(x => x.DocNr).ToList())
            {
                consignmentStops.Stops.Add(
                    new Stop
                    {
                        Consignments = new List<Consignment>
                        {
                            new Consignment
                            {
                                Reference = itemGroup.First().DocNr,
                                Nature = itemGroup.First().MovementType,
                                CommodityType = "Box",
                                SpecialInstructions = string.Join(";", itemGroup.Select(x=> x.DeliveryInstructions)),
                                DueAtDateTime = itemGroup.First().GetDate(),
                                HandlingUnits = itemGroup.Select(x=> new HandlingUnit
                                {
                                    Barcode = x.SerialNr,
                                    Reference = x.SerialNr,
                                    Description = x.Description,
                                    Code = x.ProdCode,
                                    Pieces = 1,
                                    AmountEx = 0,
                                    AmountIncl = 0,
                                    Discount = 0,
                                     Height = 0,
                                     Length = 0,
                                     Volume = 0,
                                     Weight = 0,
                                     Width = 0,
                                     Model= ""
                                }).ToList(),
                                AmountEx = 0,
                                AmountInc = 0,
                                Height = 0,
                                Length = 0,
                                Pallets = 0,
                                Pieces = 0,
                                Volume = 0,
                                Weight = 0,
                                Width = 0
                            }
                        },
                        Consignee = new Consignee
                        {
                            MST = 20,
                            Name = itemGroup.First().CustomerName,
                            Reference = itemGroup.First().CusNo,
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
                            ConsigneeCode = itemGroup.First().CusNo,
                            Position = new Position
                            {
                                Latitude = double.Parse(itemGroup.First().Lattitude),
                                Longitude = double.Parse(itemGroup.First().Longitude)
                            },
                            Address = new Address
                            {
                                Street = itemGroup.First().Address.Split(',')[0],
                                Suburb = itemGroup.First().Address.Split(',')[1],
                                City = itemGroup.First().Address.Split(',')[2],
                                //PostalCode = itemGroup.First().Address.Split(',')[3]
                            }
                        }
                    }
                    );
            };
            return consignmentStops;
        }

    }

}
