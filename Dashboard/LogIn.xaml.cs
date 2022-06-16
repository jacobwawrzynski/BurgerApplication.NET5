using DataBaseContext.Entities;
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
using DataBaseContext.Security;

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
            using (var db = new AppDbContext())
            {
                var user = (from s in db.Staff
                           where s.Login == txtLogin.Text.ToString()
                           select s).FirstOrDefault();

                if (user != null || Encryption.VerifyHash(txtPassword.Password, "SHA512", user.Password))
                {
                    var res = (from r in db.Restaurants
                               where r.Id == user.Id_Restaurant
                               select r).FirstOrDefault();
                    session.staff = user;
                    session.restaurant = res;
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                 
                    if (user.Role == "Manager" || user.Role == "Owner")
                        mainWindow.AdminPanelBtn.Visibility = Visibility.Visible;
                    
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or Password is incorrect");
                    db.Dispose();
                    return;
                }
            }
        }
    }
}
