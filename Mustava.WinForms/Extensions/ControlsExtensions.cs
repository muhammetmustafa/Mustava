using System;
using System.IO;
using System.Windows.Forms;
using Mustava.Extensions;
using Mustava.Helpers;

namespace Mustava.WinForms.Extensions
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

        public static Func<T, bool> GenerateFilters<T>(Control.ControlCollection controls, string valueProperty, string namesProperty)
        {
            var andBuilder = PredicateBuilder.True<T>();

            foreach (var control in controls)
            {
                if (control == null || !control.HasProperty(valueProperty))
                    continue;

                var value = control.GetStringProperty(valueProperty);
                if (value.ExIsNullOrEmpty())
                    continue;

                var properties = control.GetValueOfProperty(namesProperty);
                if (properties == null)
                    continue;

                var orBuilder = PredicateBuilder.False<T>();
                foreach (var propertyName in properties.ExToStringOrEmpty().Split(','))
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