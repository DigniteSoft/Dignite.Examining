using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Dignite.Examining.Pages
{
    public class IndexModel : ExaminingPageModel
    {
        public void OnGet()
        {
            
        }

        public async Task OnPostLoginAsync()
        {
            await HttpContext.ChallengeAsync("oidc");
        }
    }
}