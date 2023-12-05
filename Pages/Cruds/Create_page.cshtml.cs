using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using TutoringSys_core.Model;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace TutoringSys_core.Pages.Cruds
{
    public class Create_pageModel : PageModel
    {
        public String user_names, user_id, layout, id;
        public IEnumerable<Course> courses { get; set; }
        public IEnumerable<Tutor> tutors { get; set; }
        public TutoringSysDbContext db = new TutoringSysDbContext();
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-AH0RDM8\\SQLEXPRESS;Initial Catalog=TutoringSysDb;Integrated Security=True;Encrypt=False");
        public void OnGet()
        {
            user_names = HttpContext.Session.GetString("user_name");
            user_id = HttpContext.Session.GetString("user_id");
            layout = Request.Query["layout"];
            id = Request.Query["id"];
            courses = getCourses();
            tutors = getTutors();
        }
        public void OnPostSave_tutor()
        {
            con.Open();
            // Save tutor to database
            SqlCommand cmd = new SqlCommand("Register_Tutor", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tr_id", Request.Form["tr_id"].ToString());
            cmd.Parameters.AddWithValue("@fullnames", Request.Form["tr_name"].ToString());
            cmd.Parameters.AddWithValue("@email", Request.Form["tr_email"].ToString());
            cmd.Parameters.AddWithValue("@password", "1234");
            cmd.ExecuteNonQuery();

            //Save selected courses taught by tutor to database
            var courses_list = Request.Form["courses"];
                
            foreach(var course in courses_list)
            {

                cmd = new SqlCommand("insert into Tutoring(tutor_id, course_code) values(@tr_id, @ccode)", con);
                cmd.Parameters.AddWithValue("@tr_id", Request.Form["tr_id"].ToString());
                cmd.Parameters.AddWithValue("@ccode", course.ToString());
                cmd.ExecuteNonQuery();
            }
            con.Close();
            RedirectToPage("/Cruds/View_page", new {layout="tutor"});
            
        }

        public void OnPostSave_course()
        {
            con.Open();
            // Save course to database
            SqlCommand cmd = new SqlCommand("insert into Course(code, name, credit) values(@code, @name, @cred)", con);
            cmd.Parameters.AddWithValue("@code", Request.Form["cr_id"].ToString());
            cmd.Parameters.AddWithValue("@name", Request.Form["cr_name"].ToString());
            cmd.Parameters.AddWithValue("@cred", int.Parse(Request.Form["cr_cred"].ToString()));
            cmd.ExecuteNonQuery();

            
            con.Close();
            RedirectToPage("/Cruds/View_page", new { layout = "course" });

        }

        public void OnPostSave_session()
        {
            con.Open();
            // Save course to database

            SqlCommand cmd = new SqlCommand("create_session", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@t_id", Request.Form["tutors"].ToString());
            cmd.Parameters.AddWithValue("@c_code", Request.Form["courses1"].ToString());
            cmd.Parameters.AddWithValue("@date", DateTime.Parse(Request.Form["date_time"].ToString()));
            cmd.ExecuteNonQuery();


            con.Close();
            RedirectToPage("/Cruds/View_page", new { layout = "course" });

        }



        private IEnumerable<Course> getCourses()
        {
            con.Open();

            //SqlCommand cmd = new SqlCommand("select * from Course", con);
            //using (SqlDataReader reader = cmd.ExecuteReader())
            //{
            //    while (reader.Read())
            //    {
            //        Course course = new Course();
            //        course.Code = reader.GetValue(0).ToString();
            //        course.Name = reader.GetValue(1).ToString();
            //        course.Credit = int.Parse(reader.GetValue(2).ToString());
            //        courses.Add(course);
            //    }
            //}
            var coursesFromDb = db.Courses.
                                Select(c => new Course
                                {
                                    Code = c.Code,
                                    Name = c.Name
                                })
                                .ToList();
            con.Close();
            return coursesFromDb;
        }
        private IEnumerable<Tutor> getTutors()
        {
            con.Open();

            
            var tutorsFromDb = db.Tutors.
                                Select(c => new Tutor
                                {
                                    TrId = c.TrId,
                                    Fullnames = c.Fullnames,
                                })
                                .ToList();

            con.Close();
            return tutorsFromDb ;
        }

    }
}
