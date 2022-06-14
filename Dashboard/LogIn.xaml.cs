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
using System.Windows.Shapes;

namespace Dashboard
{
   /// <summary>
   /// Interaction logic for LogIn.xaml
   /// </summary>
   public partial class LogIn : Window
   {
      public LogIn()
      {
         InitializeComponent();
      }

      private void TopPanel_MouseDown(object sender, MouseButtonEventArgs e)
      {
         if (e.LeftButton == MouseButtonState.Pressed) DragMove();
      }

      private void CloseBtn_Click(object sender, RoutedEventArgs e)
      {
         Application.Current.Shutdown();
      }

      private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
      {
         WindowState = WindowState.Minimized;
      }

      /// <summary>
      /// Łączy z bazą danych i loguje odpowiedniego użytkownika
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void btnSubmitLogin_Click(object sender, RoutedEventArgs e)
      {
         SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSqlLocalDb; Initial Catalog=BurgerAppDataBase; Integrated Security=True;");

         try
         {
            if (connection.State == ConnectionState.Closed)
               connection.Open();

            string query = "SELECT * FROM Staff WHERE Login=@Login AND Password=@Password";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue("@Login", txtLogin.Text);
            sqlCommand.Parameters.AddWithValue("@Password", txtPassword.Password);

            //int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlCommand.ExecuteReader());

            // EDIT FOR PROPER LOGGING
            if (dataTable.Rows.Count == 1)
            {
               MainWindow mainWindow = new MainWindow();
               mainWindow.Show();
               string role = dataTable.Rows[0]["Role"].ToString();
               if (role == "Manager")
               {
                  mainWindow.AdminPanelBtn.Visibility = Visibility.Visible;
               }
               this.Close();

            }
            else
            {
               MessageBox.Show("Username or Password is incorrect");
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
         finally
         {
            connection.Close();
         }
      }
   }
}
