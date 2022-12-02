using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SilmaKlinika.Pages.Patsiendid
{
    public class PatsiendidModel : PageModel
    {
        public List<PatsiendidInfo> listPatsiendid = new List<PatsiendidInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=SilmaK;Integrated Security=True;Pooling=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Patsiendid";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PatsiendidInfo info = new PatsiendidInfo();
                                info.pID = "" + reader.GetInt32(0);
                                info.eNimi = reader.GetString(1);
                                info.pNimi = reader.GetString(2);
                                info.telefon = "" + reader.GetInt32(3);

                                listPatsiendid.Add(info);
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
    public class PatsiendidInfo
    {
        public String pID;
        public String eNimi;
        public String pNimi;
        public String telefon;
    }
}