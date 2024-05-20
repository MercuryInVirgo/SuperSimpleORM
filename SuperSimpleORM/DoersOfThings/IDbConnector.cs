namespace SuperSimpleORM.DoersOfThings;

public interface IDbConnector
{
    public T GetById<T>(long id) where T : new();
}