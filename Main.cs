using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPIExportImage
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            using (var ts = new Transaction(doc, "export image"))
            {
                ts.Start();

                var desktop_path = Environment.GetFolderPath(
                Environment.SpecialFolder.Desktop);

                var view = doc.ActiveView;

                var filepath = Path.Combine(desktop_path,
                    view.Name);

                var img = new ImageExportOptions();

                doc.ExportImage(img);

                filepath = Path.ChangeExtension(filepath, "png");

                Process.Start(filepath);

                ts.Commit();
            }         
                      
            return Result.Succeeded;
        }
    }
}