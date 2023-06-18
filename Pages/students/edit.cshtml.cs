using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Studentinfomation.Pages.students
{
    public class editModel : PageModel
    {

        public studentinfo studentinfo = new studentinfo();
        public string errorMessage = "";
        public string sucessMessage = "";

        



        public void OnGet()
        {
            String student_id = Request.Query["student_id"];

            try
            {
                String connectionString = "Data Source=DESKTOP-1R4RV5M;Initial Catalog=studentinformation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM students WHERE student_id=@student_id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@student_id", student_id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                studentinfo.student_id = "" + reader.GetInt32(0);
                                studentinfo.name = reader.GetString(1);
                                studentinfo.city = reader.GetString(2);
                                studentinfo.course_id = reader.GetInt32(3);

                            }
                        }
                    }

                }
                

                

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;

            }
        }
        public void OnPost()
        {
            studentinfo.student_id = Request.Form["student_id"];
            studentinfo.name = Request.Form["name"];
            studentinfo.city = Request.Form["city"];
            //studentinfo.course_id = Request.Form["course_id"];

            if (studentinfo.student_id.Length == 0 || studentinfo.name.Length == 0 || studentinfo.city.Length == 0)
            {
                errorMessage = "All field are required";
                return;

            }

            try
            {
                String connectionString = "Data Source=DESKTOP-1R4RV5M;Initial Catalog=studentinformation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE students " +
                                  "SET name=@name, city=@city"+
                                   " WHERE student_id=@student_id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", studentinfo.name);
                        command.Parameters.AddWithValue("@city", studentinfo.city);
                        command.Parameters.AddWithValue("@course_id", studentinfo.course_id);
                        command.Parameters.AddWithValue("@student_id", studentinfo.student_id);
                        command.ExecuteNonQuery();
                        

                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/students/index");
        }
    }
}
