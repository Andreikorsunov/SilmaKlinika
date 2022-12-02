using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SilmaKlinika.Pages.Tootaja
{
    public class TootajadModel : PageModel
    {
        public List<TootajadInfo> listTootajad = new List<TootajadInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=SilmaK;Integrated Security=True;Pooling=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Tootajad";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TootajadInfo info = new TootajadInfo();
                                info.tID = "" + reader.GetInt32(0);
                                info.TooNimi = reader.GetString(1);
                                info.Spetsialiseerumine = reader.GetString(2);
                                info.Keeled = reader.GetString(3);

                                listTootajad.Add(info);
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
    public class TootajadInfo
    {
        public String tID;
        public String TooNimi;
        public String Spetsialiseerumine;
        public String Keeled;
    }
}