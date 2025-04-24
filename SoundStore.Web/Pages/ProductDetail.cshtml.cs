using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SoundStore.Web.Pages
{
    public class ProductDetailModel : PageModel
    {
        //public void OnGet()
        //{
        //}

        public async Task<IActionResult> OnGetAsync(long productId)
        {
            return Page();
        }
    }
}
