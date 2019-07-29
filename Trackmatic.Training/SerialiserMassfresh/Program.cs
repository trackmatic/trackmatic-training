using System.Runtime.Serialization;
using System.IO;
using Massfresh.Models;
using Massfresh.Site;
using Massfresh.Transformer;
using Trackmatic.Client;
using Trackmatic.Client.Routes.Integration.Data;

namespace Massfresh
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string FileName = "MassFreshConsignmentTemplate.xml";

            Serialise(FileName);
        }

        private static void Serialise(string fileName)
        {
            var serialiser = new DataContractSerializer(typeof(ConsignmentStops));
            var consignmentModel = (ConsignmentStops)serialiser.ReadObject(File.OpenRead(fileName));

            Transform(consignmentModel);
        }

        private static void Transform(ConsignmentStops consignmentModel)
        {
            var stops = consignmentModel.Stops;
            var site = SiteData.GetSites();
            var api = CreateApi(site);
            foreach (var stop in stops)
            {
                var transformer = new UploadModelTransformer(site, stop);
                var uploadModel = transformer.Transform();
                var integration = api.Organisations.Current.Routes.Integration(site.SiteId);

                SubmitRecord(integration, uploadModel);
            }
        }

        private static IApi CreateApi(SiteData site)
        {
            var apiFactory = new ApiFactory("https://rest.trackmatic.co.za/api/v2");
            var api = apiFactory.Create();
            api.Login(site.ApiKey).Wait();
            return api;
        }

        private static void SubmitRecord(Trackmatic.Client.Routes.Integration.IIntegration integration, UploadRouteConsignmentsData uploadModel)
        {
            integration.Upload(uploadModel).Wait();
        }
    }
}
