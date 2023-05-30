using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cvety.AppDataFile
{
	internal class DataBaseCommands
	{
		SqlConnection sqlConnection = new SqlConnection(@"data source=DESKTOP-HCNULQU;initial catalog=Cvety;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");

		public DataTable SqlReader(string command, string sort)
		{
			DataTable dt = new DataTable();
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
			dt.Load(sqlCommand.ExecuteReader());
			dt.DefaultView.Sort = sort;
			sqlConnection.Close();

			return dt.DefaultView.ToTable();
		}

		public void SqlModification(string command)
		{
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
			sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
		}

		public void SqlComboBoxLoad(string command, DataTable dt, string sort, ComboBox comboBox)
		{
			dt = SqlReader(command, sort);
			comboBox.ItemsSource = dt.DefaultView;
			comboBox.SelectedValuePath = dt.Columns[0].ToString();
			comboBox.DisplayMemberPath = dt.Columns[1].ToString();
		}
	}
}
