using SuperSimpleORM.Attributes;

namespace SuperSimpleORM.DBStuff;

[Table("main", "Foo")]
public class FooTable
{
    [Column(true)]
    public long Id { get; set; }
    
    [Column]
    public string? SomeValue { get; set; }
    
    [Column("OtherValue")]
    public string? SomeOtherValueWithAWeirdName { get; set; }
}