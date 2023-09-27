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
            String sqlstring = "SELECT Tbl_Players.Pl_Name, Tbl_Teams.Te_Name FROM Tbl_Players INNER JOIN Tbl_Teams ON Tbl_Players.Pl_TeamId = Tbl_Teams.Te_Id;";
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
                    pt.Name = reader["Pl_Name"].ToString();
                    pt.Team = reader["Te_Name"].ToString();

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
            String sqlstring = "SELECT Tbl_Players.Pl_Name, Tbl_Teams.Te_Name FROM Tbl_Players INNER JOIN Tbl_Teams ON Tbl_Players.Pl_TeamId = Tbl_Teams.Te_Id WHERE Tbl_Teams.Te_Id = @filterId;";
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
                    pt.Name = reader["Pl_Name"].ToString();
                    pt.Team = reader["Te_Name"].ToString();

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
