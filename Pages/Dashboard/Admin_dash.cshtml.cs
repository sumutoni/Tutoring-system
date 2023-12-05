using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TutoringSys_core.Pages.Dashboard
{
    public class Admin_dashModel : PageModel
    {
        public String user_names, user_id;
        public void OnGet()
        {
            user_names = HttpContext.Session.GetString("user_name");
            user_id = HttpContext.Session.GetString("user_id");
        }
    }
}
