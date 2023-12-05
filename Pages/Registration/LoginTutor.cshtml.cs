using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace TutoringSys_core.Pages.Registration
{
    public class LoginTutorModel : PageModel
    {
        public String message = "", role;
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-AH0RDM8\\SQLEXPRESS;Initial Catalog=TutoringSysDb;Integrated Security=True;Encrypt=False");
        public void OnGet()
        {
        }

        public void OnPost() 
        { 
            con.Open();
            SqlCommand cmd = new SqlCommand("Retrieve_user", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", Request.Form["user_id"].ToString());
            cmd.Parameters.AddWithValue("@password", Request.Form["password"].ToString());
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    role = reader.GetValue(2).ToString();
                    reader.Close();
                    getUser(role);
                }
                if (reader == null)
                {
                    message = "No student with provided ID  was found!";
                }
            }
            con.Close();
        }

        private void getUser(String role)
        {
            if (role == "Student")
            {
                SqlCommand cmd = new SqlCommand("Retrieve_Student", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@st_id", int.Parse(Request.Form["user_id"]));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        HttpContext.Session.SetString("user_name", reader.GetValue(1).ToString());
                        HttpContext.Session.SetString("user_id", reader.GetValue(0).ToString());
                        Response.Redirect("/Dashboard/Student_dash");

                    }
                }
            }

            if (role == "Tutor")
            {
                SqlCommand cmd = new SqlCommand("Retrieve_Tutor", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tr_id", Request.Form["user_id"].ToString());
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        HttpContext.Session.SetString("user_name", reader.GetValue(1).ToString());
                        HttpContext.Session.SetString("user_id", reader.GetValue(0).ToString());
                        Response.Redirect("/Dashboard/Tutor_dash");

                    }
                }

            }
            if (role == "Admin")
            {
                SqlCommand cmd = new SqlCommand("Retrieve_Admin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ad_id", Request.Form["user_id"].ToString());
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        HttpContext.Session.SetString("user_name", reader.GetValue(1).ToString());
                        HttpContext.Session.SetString("user_id", reader.GetValue(0).ToString());
                        Response.Redirect("/Dashboard/Admin_dash");

                    }
                }
            }
        }


    }
}
