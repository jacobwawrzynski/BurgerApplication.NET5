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
using DataBaseContext.Entities;
using DataBaseContext.MyPdf;
namespace Dashboard.MVVM.View
{
    /// <summary>
    /// Interaction logic for GenerateDeliveryWindow.xaml
    /// </summary>
    public partial class GenerateDeliveryWindow : Window
    {

        public GenerateDeliveryWindow()
        {
            InitializeComponent();
        }
        public static DeliveryMenager DeliveryMenager = new DeliveryMenager(session.restaurant);
        private void TopPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void GenerateBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            DeliveryMenager.Add(txtName.Text, int.Parse(txtAmount.Text));
        }
    }
}
