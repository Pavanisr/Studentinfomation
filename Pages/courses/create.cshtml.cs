using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Studentinfomation.Pages.students;
using System.Data.SqlClient;

namespace Studentinfomation.Pages.courses
{
    public class createModel : PageModel
    {

        public courseinfo courseinfo = new courseinfo();
        public string errorMessage = "";
        public string sucessMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            courseinfo.name = Request.Form["name"];
            courseinfo.lecture_name= Request.Form["lecture_name"];
           



            if (courseinfo.name.Length == 0 || courseinfo.lecture_name.Length == 0)
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
                    String sql = "INSERT INTO course " +
                        "(name, lecture_name)VALUES" +
                        "(@name,@lecture_name);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", courseinfo.name);
                        command.Parameters.AddWithValue("@lecture_name", courseinfo.lecture_name);
                       
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


            courseinfo.name = ""; courseinfo.lecture_name = "";
            sucessMessage = "New course added correctly";

            Response.Redirect("/courses/index");

        }
    }
}

