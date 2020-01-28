using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySecurityLib2;
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string encData = null;
            //string res = Security.Encrypt("Vaibhav", out encData);
            Console.WriteLine(res);

            string plain = null;
            Security.Decrypt(res,out plain);
            Console.WriteLine(plain);
            Console.ReadLine();
        }
    }
}
