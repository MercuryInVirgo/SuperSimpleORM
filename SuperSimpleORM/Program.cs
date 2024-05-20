using SuperSimpleORM.DBStuff;
using SuperSimpleORM.DoersOfThings;

namespace SuperSimpleORM;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        SqliteConnector connector = new SqliteConnector();
        BarEntity barEntity = connector.GetById<BarEntity>(2);
        FooEntity result = connector.GetById<FooEntity>(4);
        return;
    }
}