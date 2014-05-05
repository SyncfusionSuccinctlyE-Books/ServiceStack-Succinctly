using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;

namespace ServiceStack.Succinctly.Chapter1
{
    internal class Program
    {
        private static void Main()
        {
            JsonSerializationExample();

            TypeSerializerExample();

            Console.Read();
        }

        public static void JsonSerializationExample()
        {
            var person = new { LastName = "Doe", Name = "John", Age = 36 };

            var personJson = JsonSerializer.SerializeToString(person);

            Console.WriteLine(personJson);
        }

        public static void TypeSerializerExample()
        {
            var person = new { LastName = "Doe", Name = "John", Age = 36 };

            var personJsv = TypeSerializer.SerializeToString(person);

            Console.WriteLine(personJsv);
        }
    }
}