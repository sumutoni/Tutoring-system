using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using TutoringSys_core.Model;

namespace TutoringSys_core.Pages.Cruds
{
    public class View_pageModel : PageModel
    {
        public String user_names, user_id, layout;
        public TutoringSysDbContext db = new TutoringSysDbContext();
        public List<Tutor> tutors = new List<Tutor>();
        public List<Course> courses = new List<Course>();
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-AH0RDM8\\SQLEXPRESS;Initial Catalog=TutoringSysDb;Integrated Security=True;Encrypt=False");
        public void OnGet(String lay)
        {
            user_names = HttpContext.Session.GetString("user_name");
            user_id = HttpContext.Session.GetString("user_id");
            if (string.IsNullOrEmpty(lay))
                layout = Request.Query["layout"];
            else
                layout = lay;
            getAllElements(layout);

        }

        public void getAllElements(String layout)
        {
            con.Open();
            if (layout == "tutor")
            {
                
                SqlCommand cmd = new SqlCommand("select * from Tutor", con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tutor tutor = new Tutor();
                        tutor.TrId = reader.GetValue(0).ToString();
                        tutor.Fullnames = reader.GetValue(1).ToString();
                        tutor.Email = reader.GetValue(2).ToString();
                        tutors.Add(tutor);
                    }
                }
            }
            if (layout == "course")
            {
                
                SqlCommand cmd = new SqlCommand("select * from Course", con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Course course = new Course();
                        course.Code = reader.GetValue(0).ToString();
                        course.Name = reader.GetValue(1).ToString();
                        course.Credit = int.Parse(reader.GetValue(2).ToString());
                        courses.Add(course);
                    }
                }

            }
          
        }
    }
}
