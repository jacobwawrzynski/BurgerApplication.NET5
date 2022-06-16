using DataBaseContext;
using DataBaseContext.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
   /// Interaction logic for SalesView.xaml
   /// </summary>
   public partial class SalesView : UserControl
   {

      public SalesView()
      {
         InitializeComponent();
      }

      private void Sales_Loaded(object sender, RoutedEventArgs e)
      {
         using (var db = new AppDbContext())
         {
            var q = from products
                    in db.Products
                    select products;

            ProductsDG.ItemsSource = q.ToList();
         }
      }

      private void ProductsDG_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
      {
         Product_Allergen prd_alg = new Product_Allergen();
         Product product = new Product();
         Allergen allergen = new Allergen();


         using (var db = new AppDbContext())
         {
            var prod = ProductsDG.SelectedItem as Product;
            int idProd = (from p
                         in db.Products
                         where p.Id == prod.Id
                         select p.Id).FirstOrDefault();


            var alerg = (from pa
                         in db.Products_Allergens
                         join a in db.Allergens
                         on pa.Id_Allergen 
                         equals a.Id
                         where pa.Id_Product == idProd
                         select a).ToList();

            string str = "";
            if (ProductsDG.SelectedItems.Count == 1)
            {
               str += $"Allergeny: " ;
               foreach (var item in alerg)
               {
                  str += $"{item.Name} ";
               }

            }

            AllergensTB.Text = str;
         }

         
      }

      public static List<Product> pr = new List<Product>();
      private void PlaceOrderBtn_Click(object sender, RoutedEventArgs e)
      {
         Product product = ProductsDG.SelectedItem as Product;
         pr.Add(product);

      }
   }
}