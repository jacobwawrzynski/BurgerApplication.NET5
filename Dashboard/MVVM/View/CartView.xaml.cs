using DataBaseContext;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard.MVVM.View
{
   /// <summary>
   /// Interaction logic for CartView.xaml
   /// </summary>
   public partial class CartView : UserControl
   {
      public CartView()
      {
         InitializeComponent();
      }

      private void Summary_Loaded(object sender, RoutedEventArgs e)
      {
         SummaryDG.ItemsSource = SalesView.pr;
      }

      private void MakeOrderBtn_Click(object sender, RoutedEventArgs e)
      {
         List<Product> productsInCart = SalesView.pr;
         Order order = new Order();
         
         using (var db = new AppDbContext())
         {
            if (int.TryParse(ClientCodeTB.Text, out int result))
            {
               result = int.Parse(ClientCodeTB.Text);
               order.Id_Customer = result;
            }
            if (int.TryParse(DiscountCodeTB.Text, out int result2))
            {
               result2 = int.Parse(DiscountCodeTB.Text);
               order.Id_Discount_Code = result2;
            }
            order.Id_Staff = session.staff.Id;
            bool a = 
            DataBaseQuery.AddOrderToDataBase(order);

            foreach (var item in productsInCart)
            {
               Product_Order product_Order = new Product_Order();
               product_Order.Id_Order = order.Id;
               product_Order.Id_Product = item.Id;
               DataBaseQuery.AddProduct_OrderToDataBase(product_Order);
            }
            SalesView.pr.Clear();
            this.Summary_Loaded(sender, e);
         }
      }

      private void RemoveOrderBtn_Click(object sender, RoutedEventArgs e)
      {
         SalesView.pr.Clear();
         this.Summary_Loaded(sender, e);
      }
   }
}
