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
	public partial class PageZakaz : Page
	{
		DataBaseCommands dbc = new DataBaseCommands();
		DataTable dtTochli;
		string command;
		int selectedIndex;
		double stoimost = 0;

		public PageZakaz()
		{
			InitializeComponent();

			dg.ItemsSource = Globals.dtZakaz.DefaultView;
			Stoimost();
			command = $@"SELECT OrderPickupPointID, 'г. ' + OrderPickupPointCity + ', ул. ' + OrderPickupPointStreet + ', д. ' + OrderPickupPointHouse as Adres
						 FROM OrderPickupPoint";
			dbc.SqlComboBoxLoad(command, dtTochli, "Adres", cbxTochkaVydachi);
		}

		private void Stoimost()
		{
			for (int i = 0; i < Globals.dtZakaz.Rows.Count; i++)
			{
				stoimost += double.Parse(Globals.dtZakaz.Rows[i]["ProductCost"].ToString()) * double.Parse(Globals.dtZakaz.Rows[i]["Kolichestvo"].ToString());
			}
			tblStoimost.Text = stoimost.ToString();
		}

		private void BtnIzmenitKolichestvo_Click(object sender, RoutedEventArgs e)
		{
			selectedIndex = dg.SelectedIndex;
			tbxKolichestvo.Text = Globals.dtZakaz.Rows[selectedIndex]["Kolichestvo"].ToString();
		}

		private void BtnSohranit_Click(object sender, RoutedEventArgs e)
		{
			Globals.dtZakaz.Rows[selectedIndex]["Kolichestvo"] = tbxKolichestvo.Text;
			Stoimost();
		}

		private void BtnNazad_Click(object sender, RoutedEventArgs e)
		{
			Globals.frmMain.Navigate(new PageMain());
		}

		private void BtnZakazat_Click(object sender, RoutedEventArgs e)
		{
			command = $@"INSERT INTO [Order] (OrderStatusID, OrderDate, UserID, OrderDeliveryDate, OrderPickupPointID, OrderPickupCode)
						 VALUES (1, '{DateTime.Today}', NULL, '{DateTime.Today.AddDays(5)}', '{cbxTochkaVydachi.SelectedValue}', '911')";
			dbc.SqlModification(command);

			for (int i = 0; i < Globals.dtZakaz.Rows.Count; i++)
			{
				command = $@"INSERT INTO OrderProduct (OrderID, ProductArticleNumber, ProductQuantity)
							 VALUES ((SELECT MAX(OrderID) FROM [Order]), '{Globals.dtZakaz.Rows[i]["ProductArticleNumber"]}', '{Globals.dtZakaz.Rows[i]["Kolichestvo"]}')";
				dbc.SqlModification(command);
			}
		}
	}
}
