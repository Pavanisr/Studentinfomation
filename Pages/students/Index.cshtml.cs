using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.Security;

namespace Studentinfomation.Pages.students
{
    public class IndexModel : PageModel
    {
        public List<studentinfo> liststudents = new List<studentinfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-1R4RV5M;Initial Catalog=studentinformation;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM students";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                studentinfo studentinfo = new studentinfo();
                                Console.WriteLine(reader.VisibleFieldCount);
                                studentinfo.student_id = "" + reader.GetInt32(0);
                                studentinfo.name = reader.GetString(1);
                                Console.WriteLine(studentinfo.name);
                                studentinfo.city = reader.GetString(2);
                                studentinfo.course_id = reader.GetInt32(3);
                                
                                
                                liststudents.Add(studentinfo);


                            }
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine("sql error");
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }


    public class studentinfo
    {
        public string student_id;
        public string name;
        public string city;
        public int course_id;
        public string created_at;
    } 
}
