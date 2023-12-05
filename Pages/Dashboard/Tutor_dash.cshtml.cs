using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TutoringSys_core.Model;

namespace TutoringSys_core.Pages.Dashboard
{
    public class Tutor_dashModel : PageModel
    {
        public String user_id, user_names;
        public TutoringSysDbContext db = new TutoringSysDbContext();
        public void OnGet()
        {
            user_names = HttpContext.Session.GetString("user_name");
            user_id = HttpContext.Session.GetString("user_id");
            if (user_id == null)
                Response.Redirect("/Registration/LoginTutor/");
        }

        public void OnPostlogout() 
        {
            HttpContext.Session.Clear();
            Response.Redirect("Registration/LoginTutor/");
        }
    }
}
