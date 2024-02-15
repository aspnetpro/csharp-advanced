using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Configuration;
using MyApp.Models;

namespace MyApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfigr configr;

        [BindProperty]
        public Store Store { get; set; }

        public IndexModel(ILogger<IndexModel> logger,
            IConfigr configr)
        {
            _logger = logger;
            this.configr = configr;
        }

        public void OnGet()
        {
            Store = configr.Get<Store>();
        }

        public void OnPost()
        {
            configr.Save(Store);
        }
    }
}
