﻿using System;
using System.Runtime.Serialization;
using System.IO;
using Trackmatic.Client;
using Trackmatic.Client.Routes.Integration.Data;
using SerialiserConsoleApp.Models;
using SerialiserConsoleApp.Transformer;
using SerialiserConsoleApp.Site;

namespace SerialiserConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            string FileName = "CompanyConsignments.xml";

            SerialiseAndTransform(FileName);
        }

        private static void SerialiseAndTransform(string fileName)
        {
            var Serialiser = new DataContractSerializer(typeof(ConsignmentStops));
            var consignmentModel = (ConsignmentStops)Serialiser.ReadObject(File.OpenRead(fileName));

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
            Console.ReadKey();
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
