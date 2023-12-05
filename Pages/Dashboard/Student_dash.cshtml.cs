using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data.SqlClient;
using TutoringSys_core.Model;

namespace TutoringSys_core.Pages.Dashboard
{
    public class Student_dashModel : PageModel
    {
        public String user_names, user_id, search_text;
        public bool search = false;
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-AH0RDM8\\SQLEXPRESS;Initial Catalog=TutoringSysDb;Integrated Security=True;Encrypt=False");
        

        public TutoringSysDbContext db = new TutoringSysDbContext();
        public void OnGet()
        {
            
            user_names = HttpContext.Session.GetString("user_name");
            user_id = HttpContext.Session.GetString("user_id");
            if (user_id == null)
                Response.Redirect("/Registration/LoginTutor/");
        }

        public void OnPost() 
        {
            search = true;
            search_text = Request.Form["searchBox"].ToString();
        }
    }
}
