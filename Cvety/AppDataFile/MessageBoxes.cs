using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cvety.AppDataFile
{
	internal class MessageBoxes
	{
		public void MessageBoxWarning(string message)
		{
			MessageBox.Show(message, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
		}
	}
}
