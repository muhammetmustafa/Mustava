using Mustava.Attributes;

namespace Mustava.DbManager.Enums
{
    public enum SqlObjectTypeEnum
    {
        [Symbol("P")]
        Procedure,

        [Symbol("FN")]
        Function,

        [Symbol("U")]
        Table
    }
}