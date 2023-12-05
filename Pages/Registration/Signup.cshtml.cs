using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace TutoringSys_core.Pages.Registration
{
    public class SignupModel : PageModel
    {
        public String err_message = "";
        public String success_message = "";
        public void OnGet()
        {
            

        }
        public void OnPost()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-AH0RDM8\\SQLEXPRESS;Initial Catalog=TutoringSysDb;Integrated Security=True;Encrypt=False");
            con.Open();
            SqlCommand cmd = new SqlCommand("Retrieve_Student", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@st_id", int.Parse(Request.Form["student_id"]));
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (!reader.Read())
                {
                    reader.Close();
                    cmd = new SqlCommand("Register_student", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@st_id", int.Parse(Request.Form["student_id"]));
                    cmd.Parameters.AddWithValue("@fullnames", Request.Form["fullnames"].ToString());
                    cmd.Parameters.AddWithValue("@password", Request.Form["Password"].ToString());
                    err_message = "";
                    cmd.ExecuteNonQuery();
                    success_message = "Registered Sucessfully! You can now login in.";
                    
                }
                else
                {
                    success_message = "";
                    err_message = "Student already registered!";
                }
            }
            con.Close();
        }
    }
}
