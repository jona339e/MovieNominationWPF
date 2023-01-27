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
        public GridTestViewModel viewModel { get; set; }

        public GridTest()
        {
            GridTestViewModel viewModel = new();
            this.viewModel = viewModel;

            DataContext = viewModel;
            InitializeComponent();

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

            TestGrid.CommitEdit(); // this is needed or the gtm will contain un-edited data
            GridTestModel gtm = this.TestGrid.SelectedItem as GridTestModel;
            if (gtm != null)
            {
                
                viewModel.OnUpdate(gtm);

            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            TestGrid.CommitEdit(); // this is needed or the gtm will contain un-edited data
            GridTestModel gtm = this.TestGrid.SelectedItem as GridTestModel;
            if (gtm != null)
            {

                viewModel.OnCreate(gtm);

            }
        }
    }
}
