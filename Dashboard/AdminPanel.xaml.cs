using Dashboard.MVVM.ViewModel;
using System;
using System.Collections.Generic;
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

namespace Dashboard
{
   /// <summary>
   /// Interaction logic for AdminPanel.xaml
   /// </summary>
   public partial class AdminPanel : Window
   {
      public AdminPanel()
      {
         InitializeComponent();
      }

      private void TopPanel_MouseDown(object sender, MouseButtonEventArgs e)
      {
         if (e.LeftButton == MouseButtonState.Pressed) DragMove();
      }

      private void CloseBtn_Click(object sender, RoutedEventArgs e)
      {
         MainWindow.adminPanelIsOpened = false;
         this.Close();
      }

      private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
      {
         WindowState = WindowState.Minimized;
      }

      protected override void OnClosed(EventArgs e)
      {
         base.OnClosed(e);
         MainWindow.adminPanelIsOpened = false;
      }





   }
}
