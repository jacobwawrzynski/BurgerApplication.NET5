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
    /// Interaction logic for RaportView.xaml
    /// </summary>
    public partial class ReportView : UserControl
    {
        DateTime _date = DateTime.Now;
        public ReportView()
        {
            InitializeComponent();
        }
        private void Report_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                var q = from r in db.Reports
                        where r.Id_Restaurant == session.restaurant.Id
                        select r;

                RaportDG.ItemsSource = q.ToList();
            }
        }
        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePicker.SelectedDate != null)
                _date = (DateTime)datePicker.SelectedDate;

        }

        private void GenerateReportPdfBtn_Click(object sender, RoutedEventArgs e)
        {
            ReportMenager reportMenager = new ReportMenager(_date, session.restaurant);
            DataBaseQuery.AddReportsToDataBase(new Report() { File = PdfMenager.PdfToByteArray(reportMenager.GeneratePdf()), Id_Restaurant = session.restaurant.Id });
            Report_Loaded(sender, e);
        }

        private void DownloadReportPdfBtn_Click(object sender, RoutedEventArgs e)
        {
            Report report = RaportDG.SelectedItem as Report;
            if (report == null) return;

            PdfMenager.SavePdf(report.File, "..", $"Raport_{report.Date.Year}_{report.Date.Month}_{report.Date.Day}_{report.Id}");
        }
    }
}
