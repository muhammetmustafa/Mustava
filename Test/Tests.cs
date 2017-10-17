using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Mustava.Ado;
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
    
    public class Tests
    {
        public void Test1()
        {
            DbHelper
                .SetQuery(
                    Query
                        .New("Select * from Memberships")
                        .SetParameters(new {})          
                 )
                .FetchItem<>()
                .FetchList<>()
                .Execute()
        }
    }
}