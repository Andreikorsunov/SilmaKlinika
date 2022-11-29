using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SilmaKlinika.Pages.Teenus
{
    public class CreateModel : PageModel
    {
        public TeenusInfo teenusInfo = new TeenusInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            teenusInfo.TeenusNimi = Request.Form["nimi"];
            teenusInfo.Hind = Request.Form["hind"];

            if (teenusInfo.TeenusNimi.Length == 0 || teenusInfo.Hind.Length == 0)
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
                    String sql = "INSERT INTO Teenus " +
                        "(TeenusNimi, Hind) VALUES " +
                        "(@nimi, @hind);";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nimi", teenusInfo.TeenusNimi);
                        command.Parameters.AddWithValue("@hind", teenusInfo.Hind);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            teenusInfo.TeenusNimi = ""; teenusInfo.Hind = "";
            successMessage = "Uus teenus loodud";

            Response.Redirect("/Teenus/Teenus");
        }
    }
}