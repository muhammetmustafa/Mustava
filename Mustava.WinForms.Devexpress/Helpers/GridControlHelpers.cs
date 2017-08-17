using System.IO;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;

namespace Mustava.WinForms.Devexpress.Helpers
{
    public static class GridControlHelpers
    {
        public static string GridViewLayoutToXml(GridView gridView)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var streamReader = new StreamReader(memoryStream))
                {
                    gridView.OptionsLayout.StoreAllOptions = true;
                    gridView.SaveLayoutToStream(memoryStream, OptionsLayoutBase.FullLayout);
                    memoryStream.Position = 0;
                    
                    return streamReader.ReadToEnd();
                }
            }
        }

        public static void XmlToGridViewLayout(GridView gridView, string layoutXml)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    streamWriter.Write(layoutXml);
                    streamWriter.Flush();
                    memoryStream.Position = 0;

                    gridView.GridControl.ForceInitialize();
                    gridView.RestoreLayoutFromStream(memoryStream, OptionsLayoutBase.FullLayout);
                }
            }

        }
    }
}