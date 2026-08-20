using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APPR.coreproject.Pages.Employee
{
    [Authorize(Roles = "Employee")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
