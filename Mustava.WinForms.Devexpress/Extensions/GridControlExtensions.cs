using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Mustava.Extensions;

namespace Mustava.WinForms.Devexpress.Extensions
{
    public static class GridControlExtensions
    {
        public static void SetGridDataSourceWithIntactView(this GridControl grid, object dataSource)
        {
            if (grid == null)
            {
                return;
            }

            var focusedRowHandle = 0;
            var topRowIndex = 0;

            var gridView = grid.DefaultView as GridView;
            if (gridView != null)
            {
                focusedRowHandle = gridView.FocusedRowHandle;
                topRowIndex = gridView.TopRowIndex;
            }

            grid.DataSource = dataSource.AsBindingSource();

            if (gridView != null)
            {
                gridView.FocusedRowHandle = focusedRowHandle;
                gridView.TopRowIndex = topRowIndex;
            }
        }
        
        public static void SetDataSource(this GridControl gridControl, object data)
        {
            gridControl.DataSource = new BindingSource(data, "");
        }
        
        public static void AskForFileAndExportToXls(this GridView gridView)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = @"Excel Files (*.xls)|*.xls";
            saveFileDialog.DefaultExt = "xls";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //string fileName = "grid.xls";

                gridView.ExportToXls(saveFileDialog.FileName);
                Process.Start(saveFileDialog.FileName);
            }
        }

        public static void AskForFileAndExportToXls(this GridControl grid)
        {
            if (grid == null)
            {
                return;
            }

            var view = grid.MainView as GridView;
            if (view == null)
            {
                return;
            }

            view.AskForFileAndExportToXls();
        }
    }
}