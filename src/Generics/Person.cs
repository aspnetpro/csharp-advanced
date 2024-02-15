namespace Generics;

public class Person
{
    public Person()
    {
        
    }

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
