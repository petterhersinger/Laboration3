using System.Data.SqlClient;

namespace Laboration3.Models
{
    public class TeamMethod
    {
        public TeamMethod() { }

        public List<TeamModel> GetTeamList(out string errormsg)
        {

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Players; Integrated Security = True";
            String sqlstring = "Select * From Tbl_Teams";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
            SqlDataReader reader = null;

            List<TeamModel> TeamModelList = new List<TeamModel>();

            errormsg = "";

            try
            {
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    TeamModel tm = new TeamModel();
                    tm.Name = reader["Name"].ToString();
                    tm.Id = Convert.ToInt16(reader["Id"]);

                    TeamModelList.Add(tm);
                }
                reader.Close();
                return TeamModelList;
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
