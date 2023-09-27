using System.Data.SqlClient;
using System.Data;

namespace Laboration3.Models
{
    public class ImageMethod
    {
        public int AddImage(int playerId, byte[] imageData, string contentType, out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Players;Integrated Security=True";

            string sqlString = "INSERT INTO Images (PlayerId, ImagePath) VALUES (@PlayerId, @ImagePath);";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add(new SqlParameter("@PlayerId", SqlDbType.Int) { Value = playerId });
            dbCommand.Parameters.Add(new SqlParameter("@ImagePath", SqlDbType.NVarChar, 255) { Value = contentType });

            try
            {
                dbConnection.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();
                errormsg = rowsAffected == 1 ? "" : "No new image was added to the database.";
                return rowsAffected;
            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public byte[] GetImage(int imageId, out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Players;Integrated Security=True";

            string sqlString = "SELECT ImagePath FROM Images WHERE ImageId = @imageId;";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);
            dbCommand.Parameters.Add(new SqlParameter("@imageId", SqlDbType.Int) { Value = imageId });

            try
            {
                dbConnection.Open();
                using (SqlDataReader reader = dbCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (!(reader.IsDBNull(0)))
                        {
                            byte[] imageData = (byte[])reader["ImageData"];
                            errormsg = "";
                            return imageData;
                        }
                    }
                }

                errormsg = "Image not found in the database.";
                return null;
            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
