using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SilmaKlinika.Pages.Teenus
{
    public class TeenusModel : PageModel
    {
        public List<TeenusInfo> listTeenus = new List<TeenusInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=SilmaK;Integrated Security=True;Pooling=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Teenus";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TeenusInfo info = new TeenusInfo();
                                info.TeenusID = "" + reader.GetInt32(0);
                                info.TeenusNimi = reader.GetString(1);
                                info.Hind = "" + reader.GetInt32(2);

                                listTeenus.Add(info);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    public class TeenusInfo
    {
        public String TeenusID;
        public String TeenusNimi;
        public String Hind;
    }
}