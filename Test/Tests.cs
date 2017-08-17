using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Mustava.Attributes;
using Mustava.Extensions;
using NUnit.Framework;

namespace Test
{
    public enum MyEnum
    {
        [Symbol("amu")]
        Bir,
        
        [Symbol("amm")]
        Iki,
        
        Uc
    }
    
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var str = new
            {
                ali = "amam",
                veli = DateTime.Now
            };
            
            Console.WriteLine(str.GetValueOfProperty("veli").ExIsNullOrEmpty() ? "boş" : "dolu");
        }
    }
}