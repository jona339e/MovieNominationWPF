using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieNomination.Model
{
    class MainWindowModel
    {
        // Database Depiction
        public string movieTitle { get; set; }
        public string directorName { get; set; }
        public DateTime releaseDate { get; set; }
        public int rating { get; set; }


        // Business Logic / Sql Logic


        const string connectionString = @"Data Source=172.20.55.225, 1433;
                                            Initial Catalog=MovieNominationDB;
                                            User ID=WPFLogin;Password=Passw0rd;
                                            encrypt=false;";

        public void SqlInsert()
        {


            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new("INSERT INTO MOVIE VALUES(@movieTitle, @directorName, @releaseDate, @rating)", connection);
                    cmd.Parameters.AddWithValue("@movieTitle", movieTitle);
                    cmd.Parameters.AddWithValue("@directorName", directorName);
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

        public bool TableHasRowsTitle()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new("SELECT MOVIETITLE FROM MOVIE WHERE MOVIETITLE = '@movietitle'", connection);
                    cmd.Parameters.AddWithValue("@movietitle", movieTitle);

                    SqlDataReader dr = cmd.ExecuteReader();
                    return dr.HasRows;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;
        }

    }
}
