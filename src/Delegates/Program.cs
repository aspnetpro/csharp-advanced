using Delegates;

//var leanwork = new Empresa();

//Gestor gestor = new Gestor(leanwork.Funcionario);

//Console.WriteLine(gestor(10));

//List<Person> people = new List<Person>
//{
//    new Person("Alice", 25),
//    new Person("Bob", 30),
//    new Person("Charlie", 35),
//    new Person("David", 40),
//    new Person("Emily", 22),
//    new Person("Frank", 27),
//    new Person("Grace", 32),
//    new Person("Hannah", 29),
//    new Person("Isaac", 36),
//    new Person("Jessica", 31)
//};

Func<Person, bool> PessoasMenorQue25Anos = (person) =>
{
    if (person == null)
        return false;
    if (person.Age < 25)
        return true;

    return false;
};

//people.Where(person => person.Age < 25);

//// LINQ
//foreach (var person in people)
//{
//    Console.WriteLine(person);
//}

ServicoBancario contaCorrente = new SeguroCasa();
contaCorrente.Contratar();

Console.ReadKey();