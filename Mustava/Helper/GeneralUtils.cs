using System;

namespace Mustava.Helper
{
    public static class GeneralUtils
    {
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
                    orBuilder = orBuilder.Or(item => FilterCheck(item, name, value));
                }

                andBuilder = andBuilder.And(orBuilder);
            }

            return andBuilder.Compile();
        }

        private static bool FilterCheck<T>(T item, string propertyName, string text)
        {
            if (item == null)
                return false;

            var property = item.GetValueOfProperty(propertyName, true);

            return property != null && property.ToString().StartsWith(text, StringComparison.OrdinalIgnoreCase);
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