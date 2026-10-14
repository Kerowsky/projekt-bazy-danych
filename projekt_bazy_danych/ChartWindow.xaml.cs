using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace projekt_bazy_danych
{
    /// <summary>
    /// Interaction logic for ChartWindow.xaml
    /// </summary>
    public partial class ChartWindow : Window
    {
        public ChartWindow()
        {

            InitializeComponent();
            WpfPlotChart.Plot.Add.Signal(new double[] { 1, 4, 9, 16, 25 });
            WpfPlotChart.Plot.Title("Przykładowy wykres");
            WpfPlotChart.Refresh();
        }
    }
}
