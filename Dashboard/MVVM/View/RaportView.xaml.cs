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
   /// Interaction logic for RaportView.xaml
   /// </summary>
   public partial class RaportView : UserControl
   {
      public RaportView()
      {
         InitializeComponent();
      }

      private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
      {
         using (var db = new AppDbContext())
         {
            var q = (from r
                    in db.Reports
                    where r.Date == datePicker.SelectedDate
                    select r).ToList();

            RaportDG.ItemsSource = q;
         }
      }
   }
}
