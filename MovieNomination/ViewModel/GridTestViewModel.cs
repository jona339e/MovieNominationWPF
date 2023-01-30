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
            testHelper = Test.ToList();
            // if i do testHelper = Test testHelper mirrors Test, so if i change Test testHelper also gets changed
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
            Test.Clear();
            Test = testHelper.ToList();

        }

        
        


        // inserts data if validation is successful
        [RelayCommand]
        public void OnCreate(GridTestModel model)
        {

            // Method that validates if model exist in testhelper
            // if it does exist set test = to null and then test = testhelper
            // if it doesn't exist update testhelper and insert into database


            if (ValidateData(model))
            {
                model.id = testHelper.Last().id++;
                testHelper.Add(model);
                Test.Clear();
                Test = testHelper.ToList();
                model.InsertData(model);
            }
            else
            {
                Test.Clear();
                Test = testHelper;
                MessageBox.Show("Validation failed.. try again!");
            }

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
                Test = testHelper.ToList();

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


        // checks if movieTitle already exists
        private bool ValidateTitle(GridTestModel model)
        {
            foreach (var item in testHelper)
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
            foreach (var item in testHelper)
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
