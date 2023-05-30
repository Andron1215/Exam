using Cvety.AppDataFile;
using System.Windows;

namespace Cvety
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			Globals.window = this;
			Globals.frmMain = frmMain;

			frmMain.Navigate(new Pages.PageAvtorizaciya());
		}
	}
}
