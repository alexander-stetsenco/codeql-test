using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnetcore
{
    class Program
    {
        public static string connectionString = "Server=myserver;Database=mydb;User Id=myuser;Password=mypassword;"; // Security: Hardcoded credentials
        
        static void Main(string[] args)
        {
            var demo = new SecurityIssuesDemo();
            demo.RunDemo();

            var perfDemo = new PerformanceIssuesDemo();
            perfDemo.RunDemo();

            var reliabilityDemo = new ReliabilityIssuesDemo();
            reliabilityDemo.RunDemo();

            var codeSmell = new CodeSmellsDemo();
            codeSmell.CalculateDiscount(new Customer(), new Order(), true);
        }
    }

    public class SecurityIssuesDemo
    {
        public void RunDemo()
        {
            // SQL Injection vulnerability
            string userId = Console.ReadLine();
            using (var connection = new SqlConnection(Program.connectionString))
            {
                var command = new SqlCommand($"SELECT * FROM Users WHERE UserId = {userId}", connection);
                connection.Open();
                var reader = command.ExecuteReader();
            }

            // Weak cryptography
            using (var md5 = MD5.Create())
            {
                string password = "mypassword";
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            // Hardcoded encryption key
            byte[] key = Encoding.UTF8.GetBytes("MySuperSecretKey");
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                // Use for encryption
            }
        }
    }

    public class PerformanceIssuesDemo
    {
        // Multiple resource management issues
        private SqlConnection _connection;
        private StreamReader _reader;
        private MemoryStream _memStream;
        
        public void RunDemo()
        {
            InitializeResources();

            // Inefficient string concatenation in loop
            string result = "";
            for (int i = 0; i < 10000; i++)
            {
                result += i.ToString();
            }

            // Memory leak - not disposing IDisposable
            var fileStream = File.OpenRead("somefile.txt");
            // No using statement or dispose call

            // Inefficient LINQ usage
            var numbers = Enumerable.Range(1, 1000000);
            var count = numbers.Count(); // Should use numbers.Length
            var firstItem = numbers.First(); // Should use numbers[0]

            OpenFile();

            //  Not disposing multiple resources
            _connection = new SqlConnection("connection_string");
            _connection.Open();
            var command = _connection.CreateCommand();
            var reader = command.ExecuteReader();
            // Missing dispose for command, reader, and connection

            // Resource leak in exception path
            StreamReader streamReader = null;
            try
            {
                streamReader = new StreamReader("data.txt");
                var line = streamReader.ReadLine();
                throw new Exception("Simulated error");
            }
            catch(Exception)
            {
                // Resource leak - streamReader not disposed in exception path
            }

            // Nested disposables without using
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.WriteLine("test");
            // Neither ms nor writer disposed

            // Disposable stored in field without class implementing IDisposable
            _memStream = new MemoryStream();
            _reader = new StreamReader(_memStream);
            
            // Disposable created in loop without disposal
            for (int i = 0; i < 10; i++)
            {
                var tempStream = new MemoryStream();
                var data = Encoding.UTF8.GetBytes("test");
                tempStream.Write(data, 0, data.Length);
                // tempStream not disposed in loop
            }
        }

        // Resource leak - not implementing IDisposable
        private FileStream _fileStream;
        public void OpenFile()
        {
            _fileStream = File.OpenRead("data.txt");
        }
        
        private void InitializeResources()
        {
            _connection = new SqlConnection("connection_string");
            _reader = new StreamReader(File.OpenRead("test.txt"));
        }
    }

    public class ReliabilityIssuesDemo
    {
        static int _counter = 0;

        public void RunDemo()
        {
            // Null reference issues
            string nullString = null;
            int length = nullString.Length;

            // Integer overflow
            int maxValue = int.MaxValue;
            int overflow = maxValue + 1;

            // Exception swallowing
            try
            {
                File.ReadAllText("nonexistent.txt");
            }
            catch (Exception)
            {
                // Empty catch block
            }

            // Race condition
            Parallel.For(0, 1000, i =>
            {
                _counter++; // No lock
            });

            PotentialDeadlock();
        }

        // Potential deadlock
        private static readonly object lock1 = new object();
        private static readonly object lock2 = new object();

        public void PotentialDeadlock()
        {
            lock (lock1)
            {
                Thread.Sleep(1000);
                lock (lock2)
                {
                    Console.WriteLine("This could deadlock");
                }
            }
        }
    }

    public class CodeSmellsDemo
    {
        // Complex conditional
        public decimal CalculateDiscount(Customer customer, Order order, bool isHoliday)
        {
            if (customer.IsVIP && order.TotalAmount > 1000 && isHoliday)
            {
                return order.TotalAmount * 0.2m;
            }
            else if (customer.IsVIP && order.TotalAmount > 500)
            {
                return order.TotalAmount * 0.1m;
            }
            else if (isHoliday && order.TotalAmount > 750)
            {
                return order.TotalAmount * 0.15m;
            }
            else if (customer.IsVIP || (isHoliday && order.TotalAmount > 250))
            {
                return order.TotalAmount * 0.05m;
            }
            return 0;
        }
    }

    // Minimal classes to make the code compile
    public class Customer
    {
        public bool IsVIP { get; set; }
    }

    public class Order
    {
        public decimal TotalAmount { get; set; }
    }
}
