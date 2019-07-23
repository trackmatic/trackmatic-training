using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;
using SerialiserConsoleApp.Models;
using System;

namespace SerialiserConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string FileName = "CompanyConsignments.xml";
            var Serialiser = new DataContractSerializer(typeof(Consignments));
            // (Consignment) casts the serialiser to an object of type Consigments.
            var ConsignmentModel = (Consignments)Serialiser.ReadObject(File.OpenRead(FileName));

            Console.ReadKey();
        }
    }
}
