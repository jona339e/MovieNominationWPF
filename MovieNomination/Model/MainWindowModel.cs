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


        // Business Logic

        public void SqlInsert()
        {
            const string connectionString = @"Data Source=192.168.99.60, 1433;
                                            Initial Catalog=MovieNominationDB;
                                            User ID=WPFLogin;Password=Passw0rd;
                                            encrypt=false;";

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

    }
}
