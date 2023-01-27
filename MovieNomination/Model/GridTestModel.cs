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




        const string connectionString = @"Data Source=172.24.24.111, 1433;
                                            Initial Catalog=MovieNominationDB;
                                            User ID=WPFLogin;Password=Passw0rd;
                                            encrypt=false;";


        public List<GridTestModel> getData()
        {
            List<GridTestModel> returnList = new();

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


                        returnList.Add(data);
                    }


                    return returnList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }


        public void InsertData(GridTestModel model)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new("INSERT INTO MOVIE VALUES(@movieTitle, @directorName, @releaseDate, @rating)", connection);
                    cmd.Parameters.AddWithValue("@movieTitle", model.movieTitle);
                    cmd.Parameters.AddWithValue("@directorName", model.directorName);
                    cmd.Parameters.AddWithValue("@releaseDate", model.releaseDate);
                    cmd.Parameters.AddWithValue("@rating", model.rating);

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

        public void UpdateData(GridTestModel model)
        {
            
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();


                    using (SqlCommand cmd = new("UPDATE MOVIE SET MOVIETITLE = @movieTitle, DIRECTORNAME = @directorName, RELEASEDATE = @releaseDate, RATING = @rating WHERE ID = @id", connection))
                    {

                        cmd.Parameters.AddWithValue("@id", model.id);
                        cmd.Parameters.AddWithValue("@movieTitle", model.movieTitle);
                        cmd.Parameters.AddWithValue("@directorName", model.directorName);
                        cmd.Parameters.AddWithValue("@releaseDate", model.releaseDate);
                        cmd.Parameters.AddWithValue("@rating", model.rating);


                        using (SqlDataAdapter da = new())
                        {

                            da.UpdateCommand = cmd;
                            da.UpdateCommand.ExecuteNonQuery();

                        }
                    }
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
