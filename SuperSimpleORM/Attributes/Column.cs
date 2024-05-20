namespace SuperSimpleORM.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class Column(string name, bool isPk = false) : Attribute
{
    public string Name { get; } = name;
    public bool IsPk { get; } = isPk;

    public Column() : this(string.Empty, false) { }

    public Column(bool isPk) : this(String.Empty, isPk) { }
}