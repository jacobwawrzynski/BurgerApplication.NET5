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
                    select new { products.Name, products.Price, products.Description };

            ProductsDG.ItemsSource = q.ToList();

            //Product_Allergen prd_alg = new Product_Allergen();
            //Product product = new Product();
            //Allergen allergen = new Allergen();

            //using (var db = new AppDbContext())
            //{
            //   var pr_al = (from pa
            //                in db.Products_Allergens
            //                where product.Id == prd_alg.Id_Product
            //                select pa.Id_Allergen).FirstOrDefault();

            //   var al = from algn
            //            in db.Allergens
            //            where allergen.Id == pr_al
            //            select algn;

            //   AllergensDG.ItemsSource = al.ToList();
            //}
         }
      }

      private void ProductsDG_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
      {
         Product_Allergen prd_alg = new Product_Allergen();
         Product product = new Product();
         Allergen allergen = new Allergen();

         using (var db = new AppDbContext())
         {
            var pr_al = (from pa
                         in db.Products_Allergens
                         where product.Id == prd_alg.Id_Product
                         select pa.Id_Allergen).FirstOrDefault();

            var al = from algn
                     in db.Allergens
                     where allergen.Id == pr_al
                     select algn;

         }

         string str = "";
         if (ProductsDG.SelectedItems.Count > 0)
         {
            foreach (var item in ProductsDG.SelectedItems)
            {
               allergen = item as Allergen;
               str += "Allergeny: " + allergen.Name;
            }
         }
         else { }

         AllergensTB.Text = str;
      }
   }
}

//public MainWindow()
//{
//   InitializeComponent();
//   ObservableCollection<Member> memberData = new ObservableCollection<Member>()
//             {
//                 new Member(){DepartmetId = "Dp01",EmployeeId = "EP0011" ,Name = "Joe1",Address = "Street 1",Email = new Uri("mailto:Joe1@school.com")},
//                 new Member(){DepartmetId = "Dp02",EmployeeId = "EP0012" ,Name = "Joe2",Address = "Street 2",Email = new Uri("mailto:Joe2@school.com")},
//                 new Member(){DepartmetId = "Dp03",EmployeeId = "EP0013" ,Name = "Joe3",Address = "Street 3",Email = new Uri("mailto:Joe3@school.com")},
//                 new Member(){DepartmetId = "Dp04",EmployeeId = "EP0014" ,Name = "Joe4",Address = "Street 4",Email = new Uri("mailto:Joe4@school.com")}
//             };
//   dataGrid.DataContext = memberData;
//}

//private void dataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
//{
//   string str = "";
//   if (dataGrid.SelectedItems.Count > 0)
//   {
//      Member member = new Member();
//      foreach (var obj in dataGrid.SelectedItems)
//      {
//         member = obj as Member;
//         str += "DepartmetId : " + member.DepartmetId + "   EmployeeId:" + member.EmployeeId + "  Name:" + member.Name + "  Address:" + member.Address + "  Email:" + member.Email + "\n";
//      }
//   }
//   else
//   {

//   }
//   myTxt.Text = str;
//}
//     }