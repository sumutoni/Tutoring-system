using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using TutoringSys_core.Model;

namespace TutoringSys_core.Pages.Cruds
{
    public class Edit_pageModel : PageModel
    {
        public String user_names, user_id, layout, item_id;
        public String tutor_name, tutor_email, tutor_id, course_code, course_name, credit;
        
        public TutoringSysDbContext db = new TutoringSysDbContext();
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-AH0RDM8\\SQLEXPRESS;Initial Catalog=TutoringSysDb;Integrated Security=True;Encrypt=False");
        public void OnGet()
        {
            user_names = HttpContext.Session.GetString("user_name");
            user_id = HttpContext.Session.GetString("user_id");
            layout = Request.Query["layout"];
            item_id = Request.Query["id"];
            if (layout == "tutor")
                getTutor();
            if (layout == "course")
                getCourse();
        }

        public void OnPostDelete_course() 
        {
            con.Open();
            var course_ids = Request.Form["courses"];
            foreach (var course in course_ids)
            {
                SqlCommand cmd = new SqlCommand("delete from Tutoring where tutor_id=@id and course_code=@code", con);
                cmd.Parameters.AddWithValue("@id", Request.Query["id"].ToString());
                cmd.Parameters.AddWithValue("@code", course.ToString());
                cmd.ExecuteNonQuery();
            }
            con.Close();
            
        }

        public void OnPostUpdate_info()
        {
            con.Open();
            
            tutor_name = Request.Form["tr_name"];
            tutor_id = Request.Form["tr_id"];
            tutor_email = Request.Form["tr_email"];
            SqlCommand cmd = new SqlCommand("update Tutor set tr_Id=@id, fullnames=@name, email=@email where tr_Id=@old_id", con);
            cmd.Parameters.AddWithValue("@old_id", Request.Query["id"].ToString());
            cmd.Parameters.AddWithValue("@id", tutor_id);
            cmd.Parameters.AddWithValue("@name", tutor_name);
            cmd.Parameters.AddWithValue("@email", tutor_email);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private Boolean validatecourse(String tr_id, String code)
        {
            SqlCommand cmd = new SqlCommand("select * from Tutoring where tutor_id=@tr and course_code=@code", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("tr", tr_id);
            cmd.Parameters.AddWithValue("@code", code);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (!reader.Read())
                {
                    return true;
                }
            }
            return false;
        }

        private void getTutor()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Retrieve_tutor", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tr_id", item_id);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    tutor_id = item_id;
                    tutor_name = reader.GetValue(1).ToString();
                    tutor_email = reader.GetValue(2).ToString();
                }
                
            }
            con.Close();
        }

        private void getCourse()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Course", con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    course_code = item_id;
                    course_name = reader.GetValue(1).ToString();
                    credit = reader.GetValue(2).ToString();
                }

            }
            con.Close();
        }
    }
}
