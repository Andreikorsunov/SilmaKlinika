using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SilmaKlinika.Pages.Tootaja
{
    public class EditModel : PageModel
    {
        public TootajadInfo tootajadInfo = new TootajadInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            String tID = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=SilmaK;Integrated Security=True;Pooling=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Tootajad WHERE tID=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", tID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                tootajadInfo.tID = "" + reader.GetInt32(0);
                                tootajadInfo.TooNimi = reader.GetString(1);
                                tootajadInfo.Spetsialiseerumine = reader.GetString(2);
                                tootajadInfo.Keeled = reader.GetString(3);
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
            tootajadInfo.tID = Request.Form["id"];
            tootajadInfo.TooNimi = Request.Form["nimi"];
            tootajadInfo.Spetsialiseerumine = Request.Form["spetsialiseerumine"];
            tootajadInfo.Keeled = Request.Form["keeled"];
        
            if (tootajadInfo.TooNimi.Length == 0 || tootajadInfo.Spetsialiseerumine.Length == 0
                || tootajadInfo.Keeled.Length == 0)
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
                    String sql = "UPDATE Tootajad" +
                        "SET TooNimi=@nimi, Spetsialiseerumine=@spetsialiseerumine, Keeled=@keeled " +
                        "WHERE tID=@id";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", tootajadInfo.tID);
                        command.Parameters.AddWithValue("@nimi", tootajadInfo.TooNimi);
                        command.Parameters.AddWithValue("@spetsialiseerumine", tootajadInfo.Spetsialiseerumine);
                        command.Parameters.AddWithValue("@keeled", tootajadInfo.Keeled);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage=ex.Message;
                return;
            }

            Response.Redirect("/Tootaja/Tootajad");
        }
    }
}