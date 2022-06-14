using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using RandomDataToDataBase;

namespace Dashboard.MVVM.View
{
   /// <summary>
   /// Interaction logic for AdminPanelMenuView.xaml
   /// </summary>
   public partial class AdminPanelMenuView : UserControl
   {
      public AdminPanelMenuView()
      {
         InitializeComponent();
      }

      SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSqlLocalDb; Initial Catalog=BurgerAppDataBase; Integrated Security=True;");
      

      private void SearchBox_KeyUp(object sender, KeyEventArgs e)
      {



         //SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSqlLocalDb; Initial Catalog=BurgerAppDataBase; Integrated Security=True;");
         //try
         //{
         //   if (connection.State == ConnectionState.Closed)
         //      connection.Open();

         //   DataContext dataContext = new DataContext(connection);

         //   string query = "SELECT * FROM Products";
         //   SqlCommand sqlCommand = new SqlCommand(query, connection);
         //   DataTable products = new DataTable();
         //   products.Load(sqlCommand.ExecuteReader());


         //   if (SearchBox.Text != "")
         //   {
         //      var line = Convert.ToInt32(SearchBox.Text);
         //      ProductNameField.Text = products.Rows[line]["Name"].ToString();
         //      ProductPriceField.Text = products.Rows[line]["Price"].ToString();
         //      ProductDescriptionField.Text = products.Rows[line]["Description"].ToString();
         //   }
            
         //}
         //catch (Exception ex)
         //{

         //   MessageBox.Show(ex.Message);
         //}
         //finally
         //{
         //   connection.Close();
         //}
        
      }
   }
}
