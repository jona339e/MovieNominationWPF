using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieNomination.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieNomination.ViewModel
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        public string movieTitle;

        [ObservableProperty]
        public string movieDirector;

        [ObservableProperty]
        public DateTime releaseDate = DateTime.Now;

        [ObservableProperty]
        public int selectedRating = 1;

        [ObservableProperty]
        public List<int> ratingList = new() { 1, 2, 3, 4, 5, 6};



        // Application Logic

        [RelayCommand]
        public void OnConfirm()
        {
            if (ValidationCheck())
            {
                MainWindowModel model = new MainWindowModel
                {
                    movieTitle = MovieTitle,
                    directorName = MovieDirector,
                    releaseDate = ReleaseDate,
                    rating = SelectedRating

                };
                model.SqlInsert();
            }
            else
            {
                MessageBox.Show("Venligst udfyld alle data.");
            }
        }

        private bool ValidationCheck()
        {
            if (movieTitle == string.Empty || movieDirector == string.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        [RelayCommand]
        public void OnCancel()
        {
            MovieTitle = string.Empty;
            MovieDirector = string.Empty;
            ReleaseDate = DateTime.Now;
            SelectedRating = RatingList[0];
        }


    }
}
