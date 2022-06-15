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
using DataBaseContext.Entities;


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
      private void SearchBox_KeyUp(object sender, KeyEventArgs e)
      {
         using (var db = new AppDbContext())
         {
            int value;
            if (SearchBox.Text != "" && int.TryParse(SearchBox.Text, out value))
            {
               var all = (from i
                          in db.Products
                          select i).Count();

               if (int.Parse(SearchBox.Text) <= all)
               {
                  var pr = (from products
                  in db.Products
                            where int.Parse(SearchBox.Text) == products.Id
                            select products).First();

                  ProductNameField.Text = pr.Name;
                  ProductDescriptionField.Text = pr.Description;
                  ProductPriceField.Text = pr.Price.ToString();
               }
            }
            
         }
        
      }
   }
}
