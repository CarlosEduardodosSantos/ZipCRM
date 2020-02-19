namespace VipCRM.Data.BusinessAdo
{
    public enum DataBaseType
    {
        SQLSERVER,
        MySQL
    }

    internal enum TagType
    {
        Select,
        From,
        Where,
        GroupBy,
        Having,
        OrderBy
    }

    internal enum AdjustmentMethod
    {
        None,
        Beginning,
        End,
        Auto
    }

    internal enum TagStatus
    {
        Single,
        Decimal,
        Missing,
        Wrong
    }

    internal enum TagOperation
    {
        Append,
        Replace,
        Drop
    }

    internal enum ElementSeparator
    {
        Shit,
        AND,
        Comma
    }
}