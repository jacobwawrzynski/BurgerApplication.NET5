using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using Dashboard.MVVM.ViewModel;

namespace Dashboard
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>

   public partial class MainWindow : Window
   {
      public static bool adminPanelIsOpened = false;
      public MainWindow()
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
      /// Wylogowuje i wyświetla panel logowania
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void LogOutBtn_Click(object sender, RoutedEventArgs e)
      {
         SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSqlLocalDb; Initial Catalog=BurgerAppDataBase; Integrated Security=True;");

         MainWindow mainWindow = new MainWindow();
         AdminPanel adminPanel = new AdminPanel();
         LogIn loginWin = new LogIn();


         try
         {
            if (connection.State == ConnectionState.Closed)
               connection.Open();

            if (adminPanelIsOpened)
            {
               adminPanel.Close();
               adminPanelIsOpened = false;
            }
            this.Close();
            loginWin.Show();
         }
         catch (Exception exc)
         {
            MessageBox.Show(exc.Message);
         }
         finally
         {
            connection.Close();
         }
      }

      /// <summary>
      /// Otwiera okno Admina, jeśli jest zamknięte
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void AdminPanelBtn_Click(object sender, RoutedEventArgs e)
      {
         AdminPanel adminPanel = new AdminPanel();

         if (!adminPanelIsOpened)
         {
            adminPanel.Show();
            adminPanelIsOpened = true;
         }
      }
   }
}
