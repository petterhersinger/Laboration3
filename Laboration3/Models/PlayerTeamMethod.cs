using System.Data.SqlClient;
using System.Data;

namespace Laboration3.Models
{
    public class PlayerTeamMethod
    {
        public PlayerTeamMethod() { }

        public List<PlayerTeamModel> GetPlayerTeamModel (out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Players; Integrated Security = True";
            String sqlstring = "SELECT Person.Fornamn, Person.Efternamn, Hobby.Aktivitet FROM Person INNER JOIN HarHobby ON Person.Id = HarHobby.Person INNER JOIN Hobby ON HarHobby.Hobby = Hobby.Id;";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
            SqlDataReader reader = null;

            List<PlayerTeamModel> PlayerTeamModelList = new List<PlayerTeamModel>();

            errormsg = "";

            try
            {
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    PlayerTeamModel pt = new PlayerTeamModel();
                    pt.Name = reader["Name"].ToString();
                    pt.Team = reader["Team"].ToString();

                    PlayerTeamModelList.Add(pt);
                }
                reader.Close();
                return PlayerTeamModelList;
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }
        public List<PlayerTeamModel> GetPlayerTeamModel(out string errormsg, int filterId)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Players; Integrated Security = True";
            String sqlstring = "SELECT Person.Fornamn, Person.Efternamn, Hobby.Aktivitet FROM Person INNER JOIN HarHobby ON Person.Id = HarHobby.Person INNER JOIN Hobby ON HarHobby.Hobby = Hobby.Id WHERE Hobby.Id = @filterId;";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            dbCommand.Parameters.Add("filterId", SqlDbType.Int).Value = filterId;

            SqlDataReader reader = null;

            List<PlayerTeamModel> PlayerTeamModelList = new List<PlayerTeamModel>();

            errormsg = "";

            try
            {
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    PlayerTeamModel pt = new PlayerTeamModel();
                    pt.Name = reader["Name"].ToString();
                    pt.Team = reader["Team"].ToString();

                    PlayerTeamModelList.Add(pt);
                }
                reader.Close();
                return PlayerTeamModelList;
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
