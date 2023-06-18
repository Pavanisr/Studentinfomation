using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Studentinfomation.Pages.students;
using System.Data.SqlClient;

namespace Studentinfomation.Pages.courses
{
    public class IndexModel : PageModel
    {
        public List<courseinfo> listcourse = new List<courseinfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-1R4RV5M;Initial Catalog=studentinformation;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM course";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                courseinfo courseinfo = new courseinfo();
                                Console.WriteLine(reader.VisibleFieldCount);
                                courseinfo.course_id = "" + reader.GetInt32(0);
                                courseinfo.name = reader.GetString(1);
                                Console.WriteLine(courseinfo.name);
                                courseinfo.lecture_name = reader.GetString(2);
                                


                                listcourse.Add(courseinfo);


                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("sql error");
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class courseinfo
    {
        public string course_id;
        public string name;
        public string lecture_name;
       
    }
}
