namespace SuperSimpleORM.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class Table(string schema, string name) : Attribute
{
    public string Schema { get; set; } = schema;
    public string Name { get; set; } = name;
}