using MyApp.Configuration;
using MyApp.Models;

namespace MyApp.Service;

public class PagSeguroPayment
{
    private readonly IConfigr configr;

    public PagSeguroPayment(IConfigr configr)
    {
        this.configr = configr;
    }

    public bool Execute(Order order)
    {
        PagSeguro pagSeguroConfig = configr.Get<PagSeguro>();

        return true;
    }
}
