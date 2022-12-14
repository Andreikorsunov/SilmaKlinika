using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SilmaKlinika.Pages.Patsiendid
{
    public class CreateModel : PageModel
    {
        public PatsiendidInfo patsiendidInfo = new PatsiendidInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
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
                    String sql = "INSERT INTO Patsiendid " +
                        "(eNimi, pNimi, telefon) VALUES " +
                        "(@enimi, @pnimi, @telefon);";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@enimi", patsiendidInfo.eNimi);
                        command.Parameters.AddWithValue("@pnimi", patsiendidInfo.pNimi);
                        command.Parameters.AddWithValue("@telefon", patsiendidInfo.telefon);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            patsiendidInfo.eNimi = ""; patsiendidInfo.pNimi = ""; patsiendidInfo.telefon = "";
            successMessage = "Uus patsiend loodud";

            Response.Redirect("/Patsiendid/Patsiendid");
        }
    }
}