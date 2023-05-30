using Cvety.AppDataFile;
using System;
using System.Collections.Generic;
using System.Data;
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
	public partial class PageAvtorizaciya : Page
	{
		DataBaseCommands dbc = new DataBaseCommands();
		MessageBoxes mb = new MessageBoxes();

		public PageAvtorizaciya()
		{
			InitializeComponent();

			Globals.window.Title = "Авторизация";
		}

		private void BtnVhod_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(tbxLogin.Text))
			{
				mb.MessageBoxWarning("Укажите логин");
			}
			else if (string.IsNullOrWhiteSpace(tbxParol.Text))
			{
				mb.MessageBoxWarning("Укажите пароль");
			}
			else
			{
				string command = $@"SELECT UserID, UserLogin, UserPassword
									FROM Users
									WHERE UserLogin = '{tbxLogin.Text}' AND UserPassword = '{tbxParol.Text}'";
				DataTable dt = new DataTable();
				dt = dbc.SqlReader(command, "");
				if (dt.Rows.Count > 0)
				{
					Globals.frmMain.Navigate(new Pages.PageMain());
				}
				else
				{
					mb.MessageBoxWarning("Неверный логин или пароль");
				}
			}
		}
	}
}
