using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace RDLC_use_list
{
    public class Product
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Product> Products { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Products = new ObservableCollection<Product>();
            my_datagrid.ItemsSource = Products;

        }
        private void clearForm()
        {
            add_date.Text = "";
            p_name_tbx.Text = "";
            p_category_tbx.Text = "";
            p_price_tbx.Text = "";
        }
        private void clear_btn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            clearForm();
        }
        private void add_btn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var newProduct = new Product
            {
                Date = add_date.SelectedDate ?? DateTime.Now,
                Name = p_name_tbx.Text,
                Category = p_category_tbx.Text,
                Price = decimal.TryParse(p_price_tbx.Text, out decimal price) ? price : 0
            };
            Products.Add(newProduct);

            // Optionally, clear input fields after adding
            clearForm();
        }

        private void report_btn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (windowsFormsHost.Visibility == Visibility.Collapsed)
            {
                if (Products != null && Products.Count > 0)
                {
                    // Show the report
                    report_btn.Content = "Close Report";
                    report_btn.Background = new SolidColorBrush(Colors.DarkRed);
                    windowsFormsHost.Visibility = Visibility.Visible;

                    // Load the report viewer with the product data
                    reportViewer.Reset();
                    reportViewer.LocalReport.ReportPath = "C:\\Users\\HP\\source\\repos\\RDLC_use_list\\RDLC_use_list\\Report2.rdlc";
                    reportViewer.LocalReport.DataSources.Clear();

                    ReportDataSource rds = new ReportDataSource("DataSet2", Products);
                    reportViewer.LocalReport.DataSources.Add(rds);
                    reportViewer.RefreshReport();
                }
                else
                {
                    MessageBox.Show("No products to display.");
                }
            }
            else
            {
                // Hide the report
                report_btn.Content = "Get Report";
                report_btn.Background = new SolidColorBrush(Colors.LawnGreen);
                windowsFormsHost.Visibility = Visibility.Collapsed;
            }
        }

    }
}
