using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Studentinfomation.Pages.students
{
    public class createModel : PageModel
    {

        public studentinfo studentinfo = new studentinfo();
        public string errorMessage = "";
        public string sucessMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            studentinfo.name = Request.Form["name"];
            studentinfo.city = Request.Form["city"];
            studentinfo.course_id = Int32.Parse(Request.Form["course_id"]);


            if (studentinfo.name.Length == 0 || studentinfo.city.Length== 0)
            {
                errorMessage = "All the field are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-1R4RV5M;Initial Catalog=studentinformation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO students " +
                        "(name, city, course_id)VALUES" +
                        "(@name,@city,@course_id);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", studentinfo.name);
                        command.Parameters.AddWithValue("@city", studentinfo.city);
                        command.Parameters.AddWithValue("@course_id", studentinfo.course_id);
                        Console.WriteLine(studentinfo.name);
                        Console.WriteLine(studentinfo.city);
                        Console.WriteLine(studentinfo.course_id);
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                errorMessage = ex.Message;
                return;
            }


            studentinfo.name = ""; studentinfo.city = ""; 
            sucessMessage = "New student added correctly";

            Response.Redirect("/students/index");
            
        }
    }
}
