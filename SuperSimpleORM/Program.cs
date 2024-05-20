using SuperSimpleORM.DBStuff;
using SuperSimpleORM.DoersOfThings;

namespace SuperSimpleORM;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        SqliteConnector connector = new SqliteConnector();
        FooTable result = connector.GetById<FooTable>(4);
        return;
    }
}