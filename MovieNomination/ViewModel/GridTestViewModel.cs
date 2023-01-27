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
    public partial class GridTestViewModel : ObservableObject
    {

        GridTestModel model = new();

        [ObservableProperty]
        public List<GridTestModel> test;

        public List<GridTestModel> testHelper;

        public GridTestViewModel()
        {
            Test = model.getData();
            testHelper = Test;
        }

        public void OnDelete(GridTestModel model)
        { 
            int i = 0;

            // maybe it would make more sense to run through testhelper instead of test

            foreach (var item in Test)
            {
                if (item.id == model.id)
                {
                    model.DeleteData(item.id);
                    testHelper.RemoveAt(i);
                    break;
                }
                i++;
            }


            // resets Test to update dataGrid
            Test = null;
            Test = testHelper;

        }

        
        


        // inserts data if validation is successful
        [RelayCommand]
        public void OnCreate(GridTestModel model)
        {

            //MessageBox.Show($"{model.id} | {model.movieTitle} | {model.directorName} | {model.releaseDate} | {model.rating} | ");

            if (ValidateData(model)) model.InsertData(model);
            else MessageBox.Show("Validation failed.. try again!");


        }

        public void OnUpdate(GridTestModel model)
        {
            if (validateOtherTitles(model) && validateRating(model))
            {
                model.UpdateData(model);


                // Find test index where test.id == model.id
                // replace data in testhelper
                // update test
                int i = 0;
                foreach (var item in testHelper)
                {
                    if (item.id == model.id)
                    {
                        testHelper[i] = model;
                        break;
                    }
                    i++;
                }

                // resets Test to update dataGrid

                Test.Clear();
                Test = testHelper;

            }
            else MessageBox.Show("Validation Failed");

        }

        // runs validation checks for insert
        private bool ValidateData(GridTestModel model)
        {
            if (!validateRating(model))
            {
                MessageBox.Show("Rating is invalid... Please choose a value between 1-6");
                return false;
            }
            else if (!ValidateTitle(model))
            {
                MessageBox.Show("Movie Title already exists... Choose another name!");
                return false;
            }
            else
            {
                return true;
            }

        }

        // TODO: Test is alreayd added and it looks to so if title exist

        // checks if movieTitle already exists
        private bool ValidateTitle(GridTestModel model)
        {
            foreach (var item in Test)
            {
                if (item.movieTitle == model.movieTitle)
                {
                    return false;
                }
            }
            return true;
        }


        // checks if rating is valid
        private bool validateRating(GridTestModel model)
        {
            if (model.rating < 1 || model.rating > 6)
            {
                return false;
            }
            else return true;
        }

        private bool validateOtherTitles(GridTestModel model)
        {
            foreach (var item in Test)
            {
                if (item.movieTitle == model.movieTitle && item.id != model.id)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
