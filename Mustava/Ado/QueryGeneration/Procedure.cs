using System.Data;

namespace Mustava.Ado.QueryGeneration
{
    public class Procedure : SqlQuery
    {
        public Procedure() : base(CommandType.StoredProcedure)
        {
            
        }
    }
}