using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Mustava.Ado;
using Mustava.Ado.QueryGeneration;
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
            SqlQuery
                .QueryString("SELECT * from Memberships")
                .DbHelper
                .FetchList<object>();

            SqlQuery
                .QueryString("SELECT * from memberships where ali = {0}")
                .FormatParameters("mamem")
                .DbHelper
                .FetchList<object>();

            Procedure
                .QueryString("GetFlights")
                .Parameters(new {aem = 34, amem = "uyliemk"})
                .DbHelper
                .Execute();
        }
    }
}