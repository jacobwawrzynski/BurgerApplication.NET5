using DataBaseContext;
using DataBaseContext.Entities;
using DataBaseContext.MyPdf;
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
    /// Interaction logic for InvoiceView.xaml
    /// </summary>
    public partial class InvoiceView : UserControl
    {
        public InvoiceView()
        {
            InitializeComponent();
        }

        private void Invoice_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                var q = from i in db.Invoices
                        where i.Id_Restaurant == session.restaurant.Id
                        select i;

                InvoiceDG.ItemsSource = q.ToList();
            }
        }
        private void AddInvoicePdfBtn_Click(object sender, RoutedEventArgs e)
        {
            InvoiceOrderWindow invoiceOrder = new InvoiceOrderWindow();
            invoiceOrder.Show();
        }

        private void DownloadInvoicePdfBtn_Click(object sender, RoutedEventArgs e)
        {
            Invoice invoice = InvoiceDG.SelectedItem as Invoice;
            if (invoice == null) return;

            PdfMenager.SavePdf(invoice.File, "..", $"Faktura_{invoice.Date.Year}_{invoice.Date.Month}_{invoice.Date.Day}_{invoice.Id}");
        }

        private void GenerateInvoicePfdBtn_Click(object sender, RoutedEventArgs e)
        {
            InvoiceMenager invoiceMenager = new InvoiceMenager(InvoiceOrderWindow.GetOrder());

            DataBaseQuery.AddInvoiceToDataBase(new Invoice() { File = PdfMenager.PdfToByteArray(invoiceMenager.GeneratePdf()), Id_Restaurant = session.restaurant.Id });
            this.Invoice_Loaded(sender, e);
        }
    }
}
