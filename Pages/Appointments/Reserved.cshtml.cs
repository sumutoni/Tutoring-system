using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using TutoringSys_core.Model;

namespace TutoringSys_core.Pages.Appointments
{
    public class ReservedModel : PageModel
    {
        public String user_names, user_id, role;
        public TutoringSysDbContext db = new TutoringSysDbContext();
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-AH0RDM8\\SQLEXPRESS;Initial Catalog=TutoringSysDb;Integrated Security=True;Encrypt=False");
        public void OnGet()
        {
            user_names = HttpContext.Session.GetString("user_name");
            user_id = HttpContext.Session.GetString("user_id");
            con.Open();

            // getting user to know their role to display reserved sessions accordingly 
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

        public void OnPost() 
        {
            
        }
    }
}
