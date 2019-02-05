using System.Collections.Generic;
using Trackmatic.Excel;
using Action = Trackmatic.Rest.Routing.Model.Action;

namespace RTTLoadRouteActionEntity
{
    class WriteToFile
    {
        public WriteToFile(string fileName, string path, List<string> headings, List<string[]> collection)
        {
            FileName = fileName;
            Path = path;
            Headings = headings;
            Collection = collection;
        }
        public string FileName { get; set; }
        public string Path { get; set; }
        public List<string> Headings { get; set; }
        public List<string[]> Collection { get; set; }

        public void Write()
        {
            var newFile = new WriteToExcel();
            newFile.WriteToSheet(Headings, Collection);
            newFile.Save(FileName, Path);
        }
    }
}
