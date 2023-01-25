using MovieNomination.Model;
using MovieNomination.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MovieNomination.View
{
    /// <summary>
    /// Interaction logic for GridTest.xaml
    /// </summary>
    public partial class GridTest : Window
    {
        GridTestViewModel viewModel = new();
        public GridTest()
        {
            InitializeComponent();
            DataContext = viewModel;

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            GridTestModel gtm = this.TestGrid.SelectedItem as GridTestModel;
            if (gtm != null)
            {

                viewModel.OnDelete(gtm);

            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            GridTestModel gtm = this.TestGrid.SelectedItem as GridTestModel;
            if (gtm != null)
            {

                viewModel.OnUpdate(gtm);

            }
        }
    }
}
