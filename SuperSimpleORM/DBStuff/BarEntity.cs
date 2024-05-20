using SuperSimpleORM.Attributes;

namespace SuperSimpleORM.DBStuff;

[Table("main", "bar")]
public class BarEntity
{
    [Column(isPk:true)]
    public long Id { get; set; }
    
    [Column]
    public long NumVal { get; set; }
}