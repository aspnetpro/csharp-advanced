using Generics;
using System.Collections;
using System.Collections.Generic;

var person1 = new Person(id: 1, name: "John");
var person2 = new Person(id: 2, name: "Mary");

// Array
Person[] people = new Person[2];
people[0] = person1;
people[1] = person2;

// ArrayList
ArrayList people2 = new ArrayList();
people2.Add(person1);
people2.Add(1);
people2.Add(true);

// Generics
List<Person> people3 = new List<Person>();
people3.Add(person1);

GenericRepository<Person> repoPerson = new GenericRepository<Person>();
repoPerson.Insert(person1);
repoPerson.Insert(person2);

Console.ReadLine();