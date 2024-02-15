using Reflection;
using System.Reflection;

var person = new Person(1, "Michel Banagouro");

Type personType = person.GetType(); // ou typeof(Person)

Console.WriteLine($"Classe: {personType.IsClass}");
Console.WriteLine($"Fullname: {personType.FullName}");
Console.WriteLine($"Namespace: {personType.Namespace}");
Console.WriteLine($"BaseType: {personType.BaseType?.Namespace}");
foreach (var propInfo in personType.GetProperties())
{
    Console.WriteLine($"Propriedade: {propInfo.Name}; Valor: {propInfo.GetValue(person)}");
}
foreach (var methodInfo in personType.GetMethods())
{
    Console.WriteLine($"Método: {methodInfo.Name}");
}

Console.ReadLine();