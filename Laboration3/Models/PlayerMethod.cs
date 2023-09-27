using System.Data;
using System.Data.SqlClient;
using System.Numerics;

namespace Laboration3.Models
{
    public class PlayerMethod
    {
        public PlayerMethod() { }

        public int InsertPlayer(PlayerModel pd, out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Players; Integrated Security = True";

            String sqlstring = "INSERT INTO Tbl_Players (Pl_Name, Pl_Position, Pl_IsStarting, Pl_TeamId) VALUES (@Name, @Position, @IsStarting, @TeamId)";

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            dbCommand.Parameters.Add("Pl_Name", SqlDbType.NVarChar, 255).Value = pd.Name;
            dbCommand.Parameters.Add("Pl_Position", SqlDbType.NVarChar, 255).Value = pd.Position;
            dbCommand.Parameters.Add("Pl_IsStarting", SqlDbType.Int).Value = pd.IsStarting;
            dbCommand.Parameters.Add("Pl_TeamId", SqlDbType.Int).Value = pd.TeamId;

            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { errormsg = ""; }
                else
                { errormsg = "Det skapas inte en person i databasen."; }
                return (i);
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public int DeletePlayer(int player_id, out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Players; Integrated Security = True";

            String sqlstring = "DELETE FROM Tbl_Players WHERE Pl_Id = @Id";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            dbCommand.Parameters.Add("Id", SqlDbType.Int).Value = player_id;

            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { errormsg = ""; }
                else
                { errormsg = "No player is deleted in db"; }
                return (i);
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public int UpdatePlayer(int player_id, PlayerModel updatedPlayer, out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Players; Integrated Security = True";

            String sqlstring = "UPDATE Tbl_Players SET Pl_Name = @Name, Pl_Position = @Position, Pl_IsStarting = @IsStarting, Pl_TeamId = @TeamId, WHERE Pl_Id = @Id";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            dbCommand.Parameters.Add("Pl_Name", SqlDbType.NVarChar, 30).Value = updatedPlayer.Name;
            dbCommand.Parameters.Add("Pl_Position", SqlDbType.NVarChar, 30).Value = updatedPlayer.Position;
            dbCommand.Parameters.Add("Pl_IsStarting", SqlDbType.Int).Value = updatedPlayer.IsStarting;
            dbCommand.Parameters.Add("Pl_TeamId", SqlDbType.Int).Value = updatedPlayer.TeamId;
            dbCommand.Parameters.Add("Pl_Id", SqlDbType.Int).Value = player_id;

            try
            {
                dbConnection.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    errormsg = ""; 
                }
                else
                {
                    errormsg = "No update in db, player not found."; 
                }

                return rowsAffected;
            }
            catch (Exception e)
            {
                errormsg = e.Message; 
                return 0; 
            }
            finally
            {
                dbConnection.Close();
            }
        }


        public PlayerModel GetPlayer(int player_id, out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Players; Integrated Security = True";

            String sqlstring = "Select * From Tbl_Players Where Pl_Id = @Id";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
            dbCommand.Parameters.Add("Pl_Id", SqlDbType.Int).Value = player_id;

            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            List<PlayerModel> PlayerList = new List<PlayerModel>();
            try
            {
                dbConnection.Open();

                myAdapter.Fill(myDS, "myPlayer");

                int count = 0;
                int i = 0;
                count = myDS.Tables["myPlayer"].Rows.Count;

                if (count > 0)
                {
                    PlayerModel pd = new PlayerModel();
                    pd.Name = myDS.Tables["myPlayer"].Rows[i]["Pl_Name"].ToString();
                    pd.Position = myDS.Tables["myPlayer"].Rows[i]["Pl_Position"].ToString();
                    pd.IsStarting = Convert.ToInt16(myDS.Tables["myPlayer"].Rows[i]["Pl_IsStarting"]);
                    pd.TeamId = Convert.ToInt16(myDS.Tables["myPlayer"].Rows[i]["Pl_TeamId"]);
                    pd.Id = Convert.ToInt16(myDS.Tables["myPlayer"].Rows[i]["Pl_Id"]);

                    errormsg = "";
                    return pd;
                }
                else
                {
                    errormsg = "No player was found in db.";
                    return null;
                }
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

        public List<PlayerModel> GetPlayerWithDataSet(out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Players; Integrated Security = True";

            String sqlstring = "Select * From Tbl_Players";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            List<PlayerModel> PlayerList = new List<PlayerModel>();

            try
            {
                dbConnection.Open();

                myAdapter.Fill(myDS, "myPlayer");

                int count = 0;
                int i = 0;
                count = myDS.Tables["myPlayer"].Rows.Count;
                if (count > 0)
                {
                    while (i < count)
                    {
                        PlayerModel pd = new PlayerModel();
                        pd.Name = myDS.Tables["myPlayer"].Rows[i]["Pl_Name"].ToString();
                        pd.Position = myDS.Tables["myPlayer"].Rows[i]["Pl_Position"].ToString();
                        pd.IsStarting = Convert.ToInt16(myDS.Tables["myPlayer"].Rows[i]["Pl_IsStarting"]);
                        pd.TeamId = Convert.ToInt16(myDS.Tables["myPlayer"].Rows[i]["Pl_TeamId"]);
                        pd.Id = Convert.ToInt16(myDS.Tables["myPlayer"].Rows[i]["Pl_Id"]);

                        i++;
                        PlayerList.Add(pd);
                    }
                    errormsg = "";
                    return PlayerList;
                }
                else
                {
                    errormsg = "No player is found";
                    return null;
                }
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
        public List<PlayerModel> GetPlayerWithReader(out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Players; Integrated Security = True";

            String sqlstring = "Select * From Tbl_Players";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            SqlDataReader reader = null;

            List<PlayerModel> PlayerList = new List<PlayerModel>();

            errormsg = "";

            try
            {
                dbConnection.Open();

                reader = dbCommand.ExecuteReader();

                while (reader.Read())
                {
                    PlayerModel Player = new PlayerModel();
                    Player.Name = reader["Pl_Name"].ToString();
                    Player.Position = reader["Pl_Position"].ToString();
                    Player.IsStarting = Convert.ToInt16(reader["Pl_IsStarting"]);
                    Player.TeamId = Convert.ToInt16(reader["Pl_TeamId"]);
                    Player.Id = Convert.ToInt16(reader["Pl_Id"]);

                    PlayerList.Add(Player);
                }
                reader.Close();
                return PlayerList;
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
        public List<PlayerTeamModel> GetPlayerTeam(out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Players; Integrated Security = True";

            String sqlstring = "SELECT Tbl_Players.Pl_Name AS PlayerName, Tbl_Teams.Te_Name AS TeamName FROM Tbl_Players INNER JOIN Tbl_Teams ON Tbl_Players.Pl_TeamId = Tbl_Teams.Te_Id;";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            SqlDataReader reader = null;

            List<PlayerTeamModel> TeamList = new List<PlayerTeamModel>();

            errormsg = "";

            try
            {
                dbConnection.Open();

                reader = dbCommand.ExecuteReader();

                while (reader.Read())
                {
                    PlayerTeamModel Team = new PlayerTeamModel();
                    Team.Name = reader["Name"].ToString();
                    Team.Team = reader["Team"].ToString();

                    TeamList.Add(Team);
                }
                reader.Close();
                return TeamList;
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
        public List<PlayerModel> SearchPlayer(string input, out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Players; Integrated Security = True";

            String sqlString = "SELECT * FROM Tbl_Players WHERE Pl_Name LIKE @input;";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("input", SqlDbType.NVarChar, 255).Value = input;

            SqlDataReader reader = null;

            List<PlayerModel> PlayerList = new List<PlayerModel>();

            errormsg = "";

            try
            {
                dbConnection.Open();

                reader = dbCommand.ExecuteReader();

                while (reader.Read())
                {
                    PlayerModel Player = new PlayerModel();
                    Player.Name = reader["Pl_Name"].ToString();
                    Player.Position = reader["Pl_Position"].ToString();
                    Player.IsStarting = Convert.ToInt16(reader["Pl_IsStarting"]);
                    Player.TeamId = Convert.ToInt16(reader["Pl_TeamId"]);
                    Player.Id = Convert.ToInt16(reader["Pl_Id"]);

                    PlayerList.Add(Player);
                }
                reader.Close();
                return PlayerList;

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
