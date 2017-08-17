using System.Windows.Forms;
using Mustava.Extensions;

namespace Mustava.WinForms.Helpers
{
    public static class FormHelpers
    {
        public static FolderBrowserDialog AskForFolderBrowserDialog(Form parent = null, string description = "Klasör seç...")
        {
            var dialog = new FolderBrowserDialog {Description = description};
            var result = dialog.ShowDialog(parent);
            if (result != DialogResult.OK || dialog.SelectedPath.ExIsNullOrEmpty())
            {
                return null;
            }

            return dialog;
        }
    }
}