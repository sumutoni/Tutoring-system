using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace TutoringSys_core.Pages.Appointments
{
    public class Reservation_CancelModel : PageModel
    {
        public String user_names, user_id, code, cname, tname, sname, date, button, tutor_id, role;
        public int cred, id;
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-AH0RDM8\\SQLEXPRESS;Initial Catalog=TutoringSysDb;Integrated Security=True;Encrypt=False");
        public void OnGet()
        {
            user_names = HttpContext.Session.GetString("user_name");
            user_id = HttpContext.Session.GetString("user_id");
            con.Open();
            //Get user role
            getUserRole();

            if (role == "Student")
                getSession_for_Student();
            if (role == "Tutor")
                getSession_for_Tutor();
            
        }

        public void OnPost()
        {
            con.Open();

            //Retrieve tutor_id, course code, and date of the session
            SqlCommand cmd = new SqlCommand("select tutor_id, course_code, date_time from Reserved_sessions where id = @id", con);
            cmd.Parameters.AddWithValue("@id", int.Parse(Request.Query["id"]));
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tutor_id = reader.GetValue(0).ToString();
                    code = reader.GetValue(1).ToString();
                    date = reader.GetValue(2).ToString();
                }
            }
            //cancel session
            cmd = new SqlCommand("cancel_session", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", int.Parse(Request.Query["id"]));
            cmd.ExecuteNonQuery();

            //get user role to know if reserved session should be made available
            user_id = HttpContext.Session.GetString("user_id");
            getUserRole();
            if (role == "Student")
            { 
                //relocate the session to available_sessions table
                cmd = new SqlCommand("create_session", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@t_id", tutor_id);
                cmd.Parameters.AddWithValue("@c_code", code);
                cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(date));
                cmd.ExecuteNonQuery();
            }

            //close connection
            con.Close();
            Response.Redirect("/Appointments/Reserved");
        }

        private void getUserRole()
        {

            SqlCommand cmd = new SqlCommand("Retrieve_user_byId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", user_id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    role = reader.GetValue(2).ToString();
                }
            }
        }


        private void getSession_for_Student()
        {
            //Retrieve information about the session whose id is passed
            SqlCommand cmd = new SqlCommand("Retrieve_st_reservedSession", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", int.Parse(Request.Query["id"]));
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    id = int.Parse(reader.GetValue(0).ToString());
                    code = reader.GetValue(1).ToString();
                    cname = reader.GetValue(2).ToString();
                    cred = int.Parse(reader.GetValue(3).ToString());
                    tname = reader.GetValue(4).ToString();
                    date = reader.GetValue(5).ToString();
                }
            }
        }

        private void getSession_for_Tutor()
        {
            //Retrieve information about the session whose id is passed
            SqlCommand cmd = new SqlCommand("Retrieve_tu_reservedSession", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", int.Parse(Request.Query["id"]));
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    id = int.Parse(reader.GetValue(0).ToString());
                    code = reader.GetValue(1).ToString();
                    cname = reader.GetValue(2).ToString();
                    cred = int.Parse(reader.GetValue(3).ToString());
                    sname = reader.GetValue(4).ToString();
                    date = reader.GetValue(5).ToString();
                }
            }
        }

    }
}
