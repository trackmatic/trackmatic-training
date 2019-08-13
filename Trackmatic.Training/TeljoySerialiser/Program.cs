using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using TeljoySerialiser.Model;
using TeljoySerialiser.Site;
using TeljoySerialiser.Transformer;
using Trackmatic.Client;
using Trackmatic.Client.Framework.Json;
using Trackmatic.Client.Routes.Integration.Data;

namespace TeljoySerialiser
{
    public class Program
    {
        static void Main(string[] args)
        {
            var tramsformer = new CsvToModelTransformer().Transform("Trackmatic XML Test Cases.txt");
            SerializeToXml<Stops>("TeljoyTestCases", "c:/temp", tramsformer);
            var json = tramsformer.ToJson();

            //string FileName = "TeljoyTestCasesV2.xml";
            //SerialiseAndTransform(FileName);
        }
        public static void SerializeToXml<T>(string filename, string path, T collection)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var stream = new FileStream($"{path}/{filename}.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(stream, collection);
            }
        }

        private static void SerialiseAndTransform(string fileName)
        {
            var Serialiser = new DataContractSerializer(typeof(Stops));
            var consignmentModel = (Stops)Serialiser.ReadObject(File.OpenRead(fileName));
            consignmentModel.Actions.SelectMany(x => x.Products).Select(x => string.Join(",", x.Barcode, x.ProductModel)).ToList().ForEach(x => System.Console.WriteLine(x));

            Transform(consignmentModel);
        }

        private static void Transform(Stops stops)
        {
            var site = SiteData.GetSites();
            var api = CreateApi(site);
            var transformer = new UploadModelTransformer(site, stops);
            var uploadModel = transformer.Transform();
            var integration = api.Organisations.Current.Routes.Integration(site.SiteId);

            SubmitRecord(integration, uploadModel);
        }

        private static void SubmitRecord(Trackmatic.Client.Routes.Integration.IIntegration integration, UploadRouteConsignmentsData uploadModel)
        {
            integration.Upload(uploadModel).Wait();
        }

        private static IApi CreateApi(SiteData site)
        {
            var apiFactory = new ApiFactory("https://rest.trackmatic.co.za/api/v2");
            var api = apiFactory.Create();
            api.Login(site.ApiKey).Wait();
            return api;
        }
    }
}
