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
   /// Interaction logic for AdminPanelRegCusView.xaml
   /// </summary>
   public partial class AdminPanelRegCusView : UserControl
   {
      public AdminPanelRegCusView()
      {
         InitializeComponent();
      }

      private void RegCusSearchBox_KeyUp(object sender, KeyEventArgs e)
      {
         using (var db = new AppDbContext())
         {
            int value;
            if (SearchBoxRegCus.Text != "" && int.TryParse(SearchBoxRegCus.Text, out value))
            {
               var all = from i
                          in db.Customers
                          select i.Id;

               if (all.Contains(int.Parse(SearchBoxRegCus.Text)))
               {
                  var pr = (from customers
                           in db.Customers
                            where int.Parse(SearchBoxRegCus.Text) == customers.Id
                            select customers).FirstOrDefault();

                  if (pr != null)
                  {
                     RegCusEmailField.Text = pr.Email;
                     RegCusVoucherCodeField.Text = pr.Code;
                  }
                  
               }
            }

         }
      }

      private void Add_RegCus_Btn_Click(object sender, RoutedEventArgs e)
      {
         using (var db = new AppDbContext())
         {
            Customer customer = new Customer();
            customer.Email = RegCusEmailField.Text;
            customer.Code = RegCusVoucherCodeField.Text;
            DataBaseQuery.AddCustomerToDataBase(customer);
         }
      }

      private void Remove_RegCus_Btn_Click(object sender, RoutedEventArgs e)
      {
         using (var db = new AppDbContext())
         {
            var pr = from customers
                     in db.Customers
                     where customers.Id == int.Parse(SearchBoxRegCus.Text)
                     select customers;

            foreach (var item in pr)
            {
               db.Customers.Remove(item);
            }
            db.SaveChanges();

         }
      }

      private void Save_RegCus_Btn_Click(object sender, RoutedEventArgs e)
      {
         using (var db = new AppDbContext())
         {
            var pr = (from customers
                           in db.Customers
                      where customers.Id == int.Parse(SearchBoxRegCus.Text)
                      select customers).FirstOrDefault();

            if (pr != null)
            {
               pr.Email = RegCusEmailField.Text;
               pr.Code = RegCusVoucherCodeField.Text;
               db.SaveChanges();
            }

         }
      }


   }
}
