using System;

/*
 *  Benefits of Records:
 *  - Simple to setup
 *  - Thread to set up
 *  - Easy/ safe to share
 *  
 *  When to use Records:
 *  - Capturing external data that doesn't change 
 *  - API calls
 *  - Processing data
 *  - Read-only data
 *  
 *  When not to use Records:
 *  - When is necessary to change the data (Entity Framework)
 *  
 */


namespace RecordDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Record1 r1a = new(FirstName: "Leandro", LastName: "Reis");
            Record1 r1b = new(FirstName: "Leandro", LastName: "Reis");
            Record1 r1c = new(FirstName: "Selin", LastName: "Civanbay");

            Class1 c1a = new(FirstName: "Ororo", LastName: "Munroe");
            Class1 c1b = new(FirstName: "Ororo", LastName: "Munroe");
            Class1 c1c = new(FirstName: "Susan", LastName: "Storm");

            Console.WriteLine("Record Type");
            Console.WriteLine($"To String: { r1a}");
            Console.WriteLine($"Are the two object equal: { Equals(r1a, r1b) }");
            Console.WriteLine($"Are the two object reference equal: { ReferenceEquals(r1a, r1b) }");
            Console.WriteLine($"Are the two object ==: { r1a == r1b }");
            Console.WriteLine($"Are the two object !=: { r1a != r1c }");
            Console.WriteLine($"Hash code of object A: { r1a.GetHashCode() }");
            Console.WriteLine($"Hash code of object B: { r1b.GetHashCode() }");
            Console.WriteLine($"Hash code of object C: { r1c.GetHashCode() }");

            var (fn, ln) = r1a;
            Console.WriteLine($"The value of fn is '{fn}' and the value of ln is '{ln}'");

            Record1 r1d = r1a with
            {
                FirstName = "Ronaaaaaldo"
            };
            Console.WriteLine($"Ronaldo's record: {r1d}");

            Console.WriteLine();
            Console.WriteLine("************************************* ");
            Console.WriteLine("Class Type");
            Console.WriteLine($"To String: { c1a}");
            Console.WriteLine($"Are the two object equal: { Equals(c1a, c1b) }");
            Console.WriteLine($"Are the two object reference equal: { ReferenceEquals(c1a, c1b) }");
            Console.WriteLine($"Are the two object ==: { c1a == c1b }");
            Console.WriteLine($"Are the two object !=: { c1a != c1c }");
            Console.WriteLine($"Hash code of object A: { c1a.GetHashCode() }");
            Console.WriteLine($"Hash code of object B: { c1b.GetHashCode() }");
            Console.WriteLine($"Hash code of object C: { c1c.GetHashCode() }");

            Console.WriteLine();
            Console.WriteLine("************************************* ");
            Record2 r2a = new(FirstName: "Leandro", LastName: "Reis", Address: "123, Street");
            Console.WriteLine($"r2a value {r2a}");
            Console.WriteLine($"r2a fn: {r2a.FirstName} ln: { r2a.LastName}");
            Console.WriteLine(r2a.SayTheAddress());

        }

        // A record is just a fancy read-only class
        public record Record1(string FirstName, string LastName);

        // class can inherit from a Record, but not from another class in this context 
        public record User1(int Id, string FirstName, string LastName) : Record1(FirstName, LastName);

        public record Record2(string FirstName, string LastName, string Address)
        {
            private string _firstName = FirstName;
            public string FirstName
            {
                get { return _firstName.Substring(0, 1); }
                init { }
            }

            internal string Address { get; init; } = Address;
            public string FullName { get => $"{ FirstName} { LastName} "; }
            public string SayTheAddress()
            {
                return $"Your Address is {Address}";
            }
        }

        public class Class1
        {
            public string FirstName { get; init; }
            public string LastName { get; init; }

            public Class1(string FirstName, string LastName)
            {
                this.FirstName = FirstName;
                this.LastName = LastName;
            }

            public void Deconstruct(out string FirstName, out string LastName)
            {
                FirstName = this.FirstName;
                LastName = this.LastName;
            }
        }

        // *********************************************************
        //  DO NOT DO ANY OF THE BELOW - CAN CAUSE UNEXPECTED PROBLEMS
        // *********************************************************
        public record Record3  // No constructor so no desconstructor
        {
            public string FirstName { get; set; }      // The set makes this record mutable (BAD) 
            public string LastName { get; set; }       // The set makes this record mutable (BAD) 
        }
    }
}