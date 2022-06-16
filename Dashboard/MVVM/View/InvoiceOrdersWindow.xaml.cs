using DataBaseContext.Entities;
using System;
using System.Collections.Generic;
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

namespace Dashboard.MVVM.View
{
    /// <summary>
    /// Interaction logic for InvoiceOrderWindow.xaml
    /// </summary>
    public partial class InvoiceOrderWindow : Window
    {
        private static Order order;
        public InvoiceOrderWindow()
        {
            InitializeComponent();
        }

        private void TopPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Orders_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                var q = from orders in db.Orders
                        join s in db.Staff on orders.Id_Staff equals s.Id
                        where s.Id_Restaurant == session.staff.Id_Restaurant
                        orderby orders.Id descending
                        select orders;

                OrdersDG.ItemsSource = q.ToList();
            }
        }

        private void InvoiceDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            order = OrdersDG.SelectedItem as Order;
            this.Close();
        }
        public static Order GetOrder()
        {
            return order;
        }
    }
}
