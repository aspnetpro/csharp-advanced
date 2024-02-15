namespace Delegates;

public abstract class ServicoBancario
{
    public abstract string Nome();
    public abstract decimal Taxa();

    public void Contratar()
    {
        // realiza todo o processo de contratação do serviço
        Console.WriteLine($"Serviço {Nome()} contratado pela taxa {Taxa().ToString("C")}");
    }
}

public class ContaCorrente : ServicoBancario
{
    public override string Nome()
    {
        return "Conta Corrente";
    }

    public override decimal Taxa()
    {
        return 10M;
    }
}

public class SeguroCasa : ServicoBancario
{
    public override string Nome()
    {
        return "Seguro Residencial";
    }

    public override decimal Taxa()
    {
        return 100M;
    }
}