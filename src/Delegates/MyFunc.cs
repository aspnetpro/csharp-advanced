namespace Delegates;

public static class MyFunc
{
    public static bool PessoasMenorQue25Anos(Person person)
    {
        if (person == null)
            return false;
        if (person.Age < 25)
            return true;
        
        return false;
    }
}
