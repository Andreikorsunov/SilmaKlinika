using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SilmaKlinika.Pages.Tootaja;
using System.Data.SqlClient;

namespace SilmaKlinika.Pages.Teenus
{
    public class EditModel : PageModel
    {
        public TeenusInfo teenusInfo = new TeenusInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            String TeenusID = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=SilmaK;Integrated Security=True;Pooling=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Teenus WHERE TeenusID=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", TeenusID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                teenusInfo.TeenusID = "" + reader.GetInt32(0);
                                teenusInfo.TeenusNimi = reader.GetString(1);
                                teenusInfo.Hind = "" + reader.GetFloat(2);
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
            teenusInfo.TeenusNimi = Request.Form["TeenusNimi"];
            teenusInfo.Hind = Request.Form["Hind"];
            
            if (teenusInfo.TeenusID.Length == 0 || teenusInfo.TeenusNimi.Length == 0 || teenusInfo.Hind.Length == 0)
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
                    String sql = "UPDATE Teenus " +
                        "SET TeenusNimi=@nimi, Hind=@hind" +
                        "WHERE TeenusID=@id";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", teenusInfo.TeenusID);
                        command.Parameters.AddWithValue("@nimi", teenusInfo.TeenusNimi);
                        command.Parameters.AddWithValue("@hind", teenusInfo.Hind);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage=ex.Message;
                return;
            }

            Response.Redirect("/Teenus/Teenus");
        }
    }
}