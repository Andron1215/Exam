using Cvety.AppDataFile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cvety.Pages
{
	public partial class PageMain : Page
	{
		DataBaseCommands dbc = new DataBaseCommands();
		DataTable dt = new DataTable();

		public PageMain()
		{
			InitializeComponent();

			Globals.window.Title = "Товары";

			Search();
			if (Globals.dtZakaz == null)
			{
				btnZakaz.IsEnabled = false;
				Globals.dtZakaz = dt.Clone();
				Globals.dtZakaz.Columns.Add("Kolichestvo");
			}
		}

		public void Search()
		{
			string search = tbxSearch.Text;
			string command = $@"SELECT P.ProductArticleNumber, P.ProductName, P.ProductDescription, PC.ProductCategoryName, PM.ProductManufacturerName, PP.ProductPostavshikName, P.ProductCost, P.ProductDiscountAmount, P.ProductQuantityInStock
								FROM Product P
								JOIN ProductCategory PC ON PC.ProductCategoryID = P.ProductCategoryID
								JOIN ProductManufacturer PM ON PM.ProductManufacturerID = P.ProductManufacturerID
								JOIN ProductPostavshik PP ON PP.ProductPostavshikID = P.ProductPostavshikID
								WHERE P.ProductArticleNumber LIKE '%{search}%' OR
								P.ProductName LIKE '%{search}%' OR
								P.ProductDescription LIKE '%{search}%' OR
								PC.ProductCategoryName LIKE '%{search}%' OR
								PM.ProductManufacturerName LIKE '%{search}%' OR
								PP.ProductPostavshikName LIKE '%{search}%'";
			dt = dbc.SqlReader(command, "ProductName");
			dg.ItemsSource = dt.DefaultView;
		}

		private void TbxSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			Search();
		}

		private void BtnZakaz_Click(object sender, RoutedEventArgs e)
		{
			Globals.frmMain.Navigate(new PageZakaz());
		}

		private void DobavitKZakazu_Click(object sender, RoutedEventArgs e)
		{
			for (int i = 0; i < Globals.dtZakaz.Rows.Count; i++)
			{
				if (Globals.dtZakaz.Rows[i]["ProductArticleNumber"] == dt.Rows[dg.SelectedIndex]["ProductArticleNumber"])
				{
					return;
				}
			}

			DataRow selectedRow = dt.Rows[dg.SelectedIndex];
			DataRow newRow = Globals.dtZakaz.NewRow();
			newRow.ItemArray = selectedRow.ItemArray;
			newRow["Kolichestvo"] = 1;
			Globals.dtZakaz.Rows.Add(newRow);

			if (!btnZakaz.IsEnabled)
			{
				btnZakaz.IsEnabled = true;
			}
		}
	}
}
