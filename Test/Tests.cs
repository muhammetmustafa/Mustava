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
            var str = "34mn,,,mlmlm";
            Console.WriteLine(str.Split(new []{','}, StringSplitOptions.RemoveEmptyEntries).Concatenate("."));
        }
    }
}