using System;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client("https://localhost:5001");
            var task = client.StudentInsert(new StudentDataService.Contracts.Request.StudentCRUID() { Code = "1", Firstname = "2", Middlename = "3", Surname = "4" }, System.Threading.CancellationToken.None);

            Console.ReadKey();
        }
    }
}
