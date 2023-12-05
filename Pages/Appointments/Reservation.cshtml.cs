using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Data;
using System.Data.SqlClient;

namespace TutoringSys_core.Pages.Reservation
{
    public class ReservationModel : PageModel
    {
        public String user_names, code, cname, tname, date, button, tutor_id;
        public int cred, id, user_id;
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-AH0RDM8\\SQLEXPRESS;Initial Catalog=TutoringSysDb;Integrated Security=True;Encrypt=False");
        public void OnGet()
        {
            user_names = HttpContext.Session.GetString("user_name");
            user_id = int.Parse(HttpContext.Session.GetString("user_id"));
            con.Open();
            SqlCommand cmd = new SqlCommand("Retrieve_availableSession", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", int.Parse(Request.Query["id"]));
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    code = reader.GetValue(1).ToString();
                    cname = reader.GetString(2);
                    cred = reader.GetInt32(3);
                    tname = reader.GetString(4);
                    date = reader.GetValue(5).ToString();
                }
            }
        }

        public void OnPost() 
        {
            con.Open();
            user_id = int.Parse(HttpContext.Session.GetString("user_id"));
            //Retrieve tutor_id related to tutor of the session
            SqlCommand cmd = new SqlCommand("select tutor_id from Available_sessions where id = @id", con);
            cmd.Parameters.AddWithValue("@id", int.Parse(Request.Query["id"]));
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tutor_id = reader.GetValue(0).ToString();
                }
            }
            //Retrieve the session the student wants to reserve
            cmd = new SqlCommand("Retrieve_availableSession", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", int.Parse(Request.Query["id"]));
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    code = reader.GetValue(1).ToString();
                    cname = reader.GetString(2);
                    cred = reader.GetInt32(3);
                    tname = reader.GetString(4);
                    date = reader.GetValue(5).ToString();
                }
            }
            //Create reserved session
            cmd = new SqlCommand("Reserve_session", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@t_id", tutor_id);
            cmd.Parameters.AddWithValue("@c_code", code);
            cmd.Parameters.AddWithValue("@s_id", user_id);
            cmd.Parameters.AddWithValue("@date", DateTime.Parse(date));
            cmd.ExecuteNonQuery();

            //Delete the session from the available sessions
            cmd = new SqlCommand("delete from Available_sessions where id=@id", con);
            cmd.Parameters.AddWithValue("@id", int.Parse(Request.Query["id"]));
            cmd.ExecuteNonQuery();
            Response.Redirect("/Appointments/Reserved");

            //close connection
            con.Close();
            
        }
    }
}
