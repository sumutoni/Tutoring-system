using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace TutoringSys_core.Pages.Profile
{
    public class ProfileModel : PageModel
    {
        public String user_names, user_id, role, email;
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-AH0RDM8\\SQLEXPRESS;Initial Catalog=TutoringSysDb;Integrated Security=True;Encrypt=False");
        public void OnGet()
        {
            user_names = HttpContext.Session.GetString("user_name");
            user_id = HttpContext.Session.GetString("user_id");
            con.Open();
            SqlCommand cmd = new SqlCommand("Retrieve_user_byId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", user_id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    role = reader.GetValue(2).ToString();
                    reader.Close();
                    getUser(role);
                }
            }
            con.Close();
        }

        private void getUser(String role)
        {
            if (role == "Tutor")
            {
                SqlCommand cmd = new SqlCommand("Retrieve_Tutor", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tr_id", user_id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        email = reader.GetValue(2).ToString();

                    }
                }

            }
        }
    }
}
