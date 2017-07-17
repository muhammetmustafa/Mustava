using System;
using System.IO;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using Mustava.Extensions;
using Mustava.Helper;

namespace Mustava.WinForms
{
    public static class ControlsExtensions
    {
        public static bool isSelectedItemLast(this ToolStripComboBox control)
        {
            return control.SelectedIndex >= control.Items.Count - 1;
        }

        public static bool isSelectedItemFirst(this ToolStripComboBox control)
        {
            return control.SelectedIndex <= 0;
        }

        public static void selectBefore(this ToolStripComboBox control)
        {
            control.SelectedIndex -= 1;
        }

        public static void selectNext(this ToolStripComboBox control)
        {
            control.SelectedIndex += 1;
        }

        public static bool isSelectedItemLast(this ComboBox control)
        {
            return control.SelectedIndex >= control.Items.Count - 1;
        }

        public static bool isSelectedItemFirst(this ComboBox control)
        {
            return control.SelectedIndex <= 0;
        }

        public static void selectBefore(this ComboBox control)
        {
            control.SelectedIndex -= 1;
        }

        public static void selectNext(this ComboBox control)
        {
            control.SelectedIndex += 1;
        }

        public static string GridViewLayoutToXml(this GridView gridView)
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

        public static void XmlToGridViewLayout(this GridView gridView, string layoutXml)
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

        public static void SetGridDataSourceWithIntactView(this GridView gridView, object dataSource)
        {
            var focusedRowHandle = gridView.FocusedRowHandle;
            var topRow = gridView.TopRowIndex;

            gridView.GridControl.DataSource = dataSource.AsBindingSource();

            gridView.FocusedRowHandle = focusedRowHandle;
            gridView.TopRowIndex = topRow;
        }

        public static BindingSource AsBindingSource(this object o)
        {
            return new BindingSource(o, "");
        }

        public static Func<T, bool> GenerateFilters<T>(Control.ControlCollection controls, string valueProperty, string namesProperty)
        {
            var andBuilder = PredicateBuilder.True<T>();

            foreach (var control in controls)
            {
                if (control == null || !control.HasProperty(valueProperty))
                    continue;

                var value = control.GetStringProperty(valueProperty);
                if (value.IsNullOrEmpty())
                    continue;

                var properties = control.GetValueOfProperty(namesProperty);
                if (properties == null)
                    continue;

                var orBuilder = PredicateBuilder.False<T>();
                foreach (var propertyName in properties.ToStringOrEmpty().Split(','))
                {
                    var name = propertyName;
                    orBuilder = orBuilder.Or(item => GeneralUtils.FilterCheck(item, name, value));
                }

                andBuilder = andBuilder.And(orBuilder);
            }

            return andBuilder.Compile();
        }


        public static FileDialog AskForExcelFile()
        {
            FileDialog fileDialog = new SaveFileDialog();
            fileDialog.DefaultExt = ".xlsx";
            fileDialog.Filter = "Excel Files|.xlsx";
            if (fileDialog.ShowDialog() != DialogResult.OK)
                return null;

            return fileDialog;
        }

        public static FileDialog AskForPdfFile()
        {
            FileDialog fileDialog = new SaveFileDialog();
            fileDialog.DefaultExt = ".pdf";
            fileDialog.Filter = "Pdf Files|.pdf";
            if (fileDialog.ShowDialog() != DialogResult.OK)
                return null;

            return fileDialog;
        }

    }
}