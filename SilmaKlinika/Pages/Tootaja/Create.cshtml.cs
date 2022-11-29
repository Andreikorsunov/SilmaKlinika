using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SilmaKlinika.Pages.Tootaja
{
    public class CreateModel : PageModel
    {
        public TootajadInfo tootajadInfo = new TootajadInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
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
                    String sql = "INSERT INTO Tootajad " +
                        "(TooNimi, Spetsialiseerumine, Keeled) VALUES " +
                        "(@nimi, @spetsialiseerumine, @keeled);";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nimi", tootajadInfo.TooNimi);
                        command.Parameters.AddWithValue("@spetsialiseerumine", tootajadInfo.Spetsialiseerumine);
                        command.Parameters.AddWithValue("@keeled", tootajadInfo.Keeled);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            tootajadInfo.TooNimi = ""; tootajadInfo.Spetsialiseerumine = ""; tootajadInfo.Keeled = "";
            successMessage = "Uus töötaja loodud";

            Response.Redirect("/Tootaja/Tootajad");
        }
    }
}