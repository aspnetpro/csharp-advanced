using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Configuration;
using MyApp.Models;

namespace MyApp.Pages
{
    public class PagSeguroModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfigr configr;

        [BindProperty]
        public PagSeguro PagSeguro { get; set; }

        public PagSeguroModel(ILogger<IndexModel> logger,
            IConfigr configr)
        {
            _logger = logger;
            this.configr = configr;
        }

        public void OnGet()
        {
            PagSeguro = configr.Get<PagSeguro>();
        }

        public void OnPost()
        {
            configr.Save(PagSeguro);
        }
    }
}
