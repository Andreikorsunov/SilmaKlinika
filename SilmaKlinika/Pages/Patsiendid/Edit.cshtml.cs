using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SilmaKlinika.Pages.Patsiendid
{
    public class EditModel : PageModel
    {
        public PatsiendidInfo patsiendidInfo = new PatsiendidInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            String pID = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=SilmaK;Integrated Security=True;Pooling=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Patsiendid WHERE pID=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", pID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                patsiendidInfo.pID = "" + reader.GetInt32(0);
                                patsiendidInfo.eNimi = reader.GetString(1);
                                patsiendidInfo.pNimi = reader.GetString(2);
                                patsiendidInfo.telefon = "" + reader.GetInt32(3);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            patsiendidInfo.pID = Request.Form["pID"];
            patsiendidInfo.eNimi = Request.Form["enimi"];
            patsiendidInfo.pNimi = Request.Form["pnimi"];
            patsiendidInfo.telefon = Request.Form["telefon"];

            if (patsiendidInfo.eNimi.Length == 0 || patsiendidInfo.pNimi.Length == 0 || patsiendidInfo.telefon.Length == 0)
            {
                errorMessage = "Kõik väljad on kohustuslikud";
                return;
            }
            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=SilmaK;Integrated Security=True;Pooling=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Patsiendid " +
                        "SET eNimi=@enimi, pNimi=@pnimi, telefon=@telefon" +
                        "WHERE pID=@id";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", patsiendidInfo.pID);
                        command.Parameters.AddWithValue("@enimi", patsiendidInfo.eNimi);
                        command.Parameters.AddWithValue("@pnimi", patsiendidInfo.pNimi);
                        command.Parameters.AddWithValue("@telefon", patsiendidInfo.telefon);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage=ex.Message;
                return;
            }

            Response.Redirect("/Patsiendid/Patsiendid");
        }
    }
}