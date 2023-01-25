using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieNomination.Model
{
    public class GridTestModel
    {
        public int id { get; set; }
        public string movieTitle { get; set; }
        public string directorName { get; set; }
        public DateTime releaseDate { get; set; } = DateTime.Now;
        public int rating { get; set; } = 1;





        // sql logic




        const string connectionString = @"Data Source=172.20.55.225, 1433;
                                            Initial Catalog=MovieNominationDB;
                                            User ID=WPFLogin;Password=Passw0rd;
                                            encrypt=false;";


        public List<GridTestModel> getData()
        {
            List<GridTestModel> myList = new();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new("SELECT * FROM MOVIE", connection);

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        GridTestModel data = new();


                        data.id = dr.GetInt32(0);
                        data.movieTitle = dr.GetString(1);
                        data.directorName = dr.GetString(2);
                        data.releaseDate = dr.GetDateTime(3);
                        data.rating = dr.GetInt32(4);


                        myList.Add(data);
                    }


                    return myList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }


        public void InsertData(GridTestModel insertModel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new("INSERT INTO MOVIE VALUES(@movieTitle, @directorName, @releaseDate, @rating)", connection);
                    cmd.Parameters.AddWithValue("@movieTitle", insertModel.movieTitle);
                    cmd.Parameters.AddWithValue("@directorName", insertModel.directorName);
                    cmd.Parameters.AddWithValue("@releaseDate", insertModel.releaseDate);
                    cmd.Parameters.AddWithValue("@rating", insertModel.rating);

                    SqlDataAdapter da = new();
                    da.InsertCommand = cmd;
                    da.InsertCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // used in an instance of this class
        public void UpdateData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new("INSERT INTO MOVIE VALUES()", connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@movieTitle", movieTitle);
                    cmd.Parameters.AddWithValue("@directorName",directorName);
                    cmd.Parameters.AddWithValue("@releaseDate", releaseDate);
                    cmd.Parameters.AddWithValue("@rating", rating);

                    SqlDataAdapter da = new();
                    da.InsertCommand = cmd;
                    da.InsertCommand.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteData(int id)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new("DELETE FROM MOVIE WHERE id = @id", connection);
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataAdapter da = new();
                    da.DeleteCommand = cmd;
                    da.DeleteCommand.ExecuteNonQuery();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

    }
 
}
