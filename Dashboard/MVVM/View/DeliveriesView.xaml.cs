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
   /// Interaction logic for DeliveriesView.xaml
   /// </summary>
   public partial class DeliveriesView : UserControl
   {
      public DeliveriesView()
      {
         InitializeComponent();
      }

      private void Deliveries_Loaded(object sender, RoutedEventArgs e)
      {
         using (var db = new AppDbContext())
         {
            var q = from deliveries
                    in db.Delivery
                    select new {deliveries.Id, deliveries.File, deliveries.Date};

            DeliveriesDG.ItemsSource = q.ToList();
         }
      }
   }
}
