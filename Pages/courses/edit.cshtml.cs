using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Studentinfomation.Pages.students;
using System.Data.SqlClient;

namespace Studentinfomation.Pages.courses
{
    public class editModel : PageModel
    {

        public courseinfo courseinfo = new courseinfo();
        public string errorMessage = "";
        public string sucessMessage = "";





        public void OnGet()
        {
            String course_id = Request.Query["course_id"];

            try
            {
                String connectionString = "Data Source=DESKTOP-1R4RV5M;Initial Catalog=studentinformation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM course WHERE course_id=@course_id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@course_id", course_id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                courseinfo.course_id = "" + reader.GetInt32(0);
                                courseinfo.name = reader.GetString(1);
                                courseinfo.lecture_name = reader.GetString(2);
                                

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
            courseinfo.course_id = Request.Form["course_id"];
            courseinfo.name = Request.Form["name"];
            courseinfo.lecture_name = Request.Form["lecture_name"];
            

            if (courseinfo.course_id.Length == 0 || courseinfo.name.Length == 0 || courseinfo.lecture_name.Length == 0)
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
                    String sql = "UPDATE course " +
                                  "SET name=@name, lecture_name=@lecture_name" +
                                   " WHERE course_id=@course_id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", courseinfo.name);
                        command.Parameters.AddWithValue("@lecture_name", courseinfo.lecture_name);
                        command.Parameters.AddWithValue("@course_id", courseinfo.course_id);
                       
                        command.ExecuteNonQuery();


                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/courses/index");
        }
    }
}
