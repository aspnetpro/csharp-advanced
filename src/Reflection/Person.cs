namespace Reflection;

public class Person
{
    public Person(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}; Name: {Name}";
    }
}

public class Customer : Person
{
    public Customer(int id, string name) : base(id, name)
    {
    }
}
