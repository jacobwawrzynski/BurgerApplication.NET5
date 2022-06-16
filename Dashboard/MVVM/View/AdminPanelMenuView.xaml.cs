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
using DataBaseContext.Entities;
using DataBaseContext;
using DataBaseContext.random;

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

      /// <summary>
      /// Dynamiczne wyszukiwanie produktów w bazie
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void SearchBoxProduct_KeyUp(object sender, KeyEventArgs e)
      {
         using (var db = new AppDbContext())
         {
            int value;
            if (SearchBoxProduct.Text != "" && int.TryParse(SearchBoxProduct.Text, out value))
            {
               var all = from i
                          in db.Products
                          select i.Id;

               if (all.Contains(int.Parse(SearchBoxProduct.Text)))
               {
                  var pr = (from products
                           in db.Products
                            where int.Parse(SearchBoxProduct.Text) == products.Id
                            select products).FirstOrDefault();

                  if (pr != null)
                  {
                     ProductNameField.Text = pr.Name;
                     ProductDescriptionField.Text = pr.Description;
                     ProductPriceField.Text = pr.Price.ToString();
                  }

               }
            }
            
         }
        
      }

      private void Add_Product_Btn_Click(object sender, RoutedEventArgs e)
      {
         using(var db = new AppDbContext())
         {
            Product product = new Product();
            product.Name = ProductNameField.Text;
            product.Price = Convert.ToDecimal(ProductPriceField.Text);
            product.Description = ProductDescriptionField.Text;
            DataBaseQuery.AddProductToDataBase(product);
         }
      }

      private void Remove_Product_Btn_Click(object sender, RoutedEventArgs e)
      {
         using (var db = new AppDbContext())
         {
            var pr = from product
                     in db.Products
                     where product.Id == int.Parse(SearchBoxProduct.Text)
                     select product;

            foreach (var item in pr)
            {
               db.Products.Remove(item);
            }
            db.SaveChanges();
            
         }
      }

      private void Save_Product_Btn_Click(object sender, RoutedEventArgs e)
      {
         using (var db = new AppDbContext())
         {
            var pr = (from products
                           in db.Products
                      where products.Id == int.Parse(SearchBoxProduct.Text)
                      select products).FirstOrDefault();

            if (pr != null)
            {
               pr.Name = ProductNameField.Text;
               pr.Price = Convert.ToDecimal(ProductPriceField.Text);
               pr.Description = ProductDescriptionField.Text;
               db.SaveChanges();
            }

         }
      }
   }
}
