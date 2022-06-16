using DataBaseContext;
using DataBaseContext.Entities;
using DataBaseContext.MyPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                        where deliveries.Id_Restaurant == session.restaurant.Id
                        select deliveries;

                DeliveriesDG.ItemsSource = q.ToList();
            }
        }

        private void AddDeliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            GenerateDeliveryWindow generateDelivery = new GenerateDeliveryWindow();

            generateDelivery.Show();


        }

        private  void DownloadDeliveryPdfBtn_Click(object sender, RoutedEventArgs e)
        {
            if (GenerateDeliveryWindow.DeliveryMenager.IsEmpty())
            {
                MessageBox.Show("Empty");
                return;
            }
            var pdf = GenerateDeliveryWindow.DeliveryMenager.GeneratePdf();
            Delivery delivery = new Delivery() { File = PdfMenager.PdfToByteArray(pdf), Id_Restaurant = session.restaurant.Id };
            DataBaseQuery.AddDeliveryToDataBase(delivery);
            GenerateDeliveryWindow.DeliveryMenager.Clear();
            this.Deliveries_Loaded(sender, e);
        }

        private void DownloadDelivery_Click(object sender, RoutedEventArgs e)
        {
            Delivery delivery = DeliveriesDG.SelectedItem as Delivery;
            if (delivery == null) return;

            PdfMenager.SavePdf(delivery.File, "..", $"Delivery_{delivery.Date.Year}_{delivery.Date.Month}_{delivery.Date.Day}_{delivery.Id}");
        }

        private void RefreshDeliveryPdfBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
