using System.ComponentModel;
using System.Data;
using System.Reflection;
using Microsoft.Data.Sqlite;
using SuperSimpleORM.Attributes;

namespace SuperSimpleORM.DoersOfThings;

public class SqliteConnector : IDbConnector
{
    public T GetById<T>(long id) where T : new()
    {
        List<Attribute> attributes = Attribute.GetCustomAttributes(typeof(T)).ToList();

        //If no attributes on this type, why are we trying to do anything with it?!
        if (attributes.Count == 0)
        {
            throw new Exception("Umm no attributes");
        }

        var tableInfo = attributes.OfType<Table>().FirstOrDefault();
        //If no Table Attribute, we can't do a dang thing!
        if (tableInfo == default(Table))
        {
            throw new Exception("No Table definition!");
        }

        string primaryKeyName = string.Empty;
        Dictionary<string, Tuple<Column, PropertyInfo>> columns = new Dictionary<string, Tuple<Column, PropertyInfo>>();
        var properties = typeof(T).GetProperties();

        //Each Property in the class that has a Column Attribute will get added to the dictionary
        foreach (var propertyInfo in properties)
        {
            List<Attribute> propertyAttributes = Attribute.GetCustomAttributes(propertyInfo).ToList();
            var columnInfo = propertyAttributes.OfType<Column>().FirstOrDefault();
            if (columnInfo == default(Column))
                continue;
            string columnName = columnInfo.Name != string.Empty ? columnInfo.Name : propertyInfo.Name;
            columns.Add(columnName, new Tuple<Column, PropertyInfo>(columnInfo, propertyInfo));
            //Need to set the primary key name
            //If this Column Attribute has IsPk set to true, then this is our primary key
            //If no Name value was passed to the Column constructor, just use the name of the Property
            if (columnInfo.IsPk)
                primaryKeyName = columnInfo.Name != string.Empty ? columnInfo.Name : propertyInfo.Name;
        }

        Dictionary<string, string> dbResults = new Dictionary<string, string>();
        
        using (var conn = new SqliteConnection("DataSource = someDb.sqlite"))
        {
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM "
                                  + tableInfo.Schema + "." + tableInfo.Name + " "
                                  + "WHERE " + primaryKeyName + " = $id";
            command.Parameters.AddWithValue("$id", id);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    foreach (var column in columns)
                    {
                        //TODO doing this check multiple times is no good
                        string value = string.Empty;
                        object? rawValue = reader.GetValue(column.Key);
                        if (rawValue is not DBNull) //Not great, will return "" instead of null for null fields
                            value = reader.GetString(column.Key);
                        dbResults.Add(column.Key, value);
                    }
                }
            }
        }
        
        //TDOD Ummmm error handling I guess
        //Like, what if the thing can't be converted?
        //What if it's null?
        //So many things
        T toReturn = new T();
        foreach (var result in dbResults)
        {
            var propertyInfo = columns[result.Key].Item2;
            var propertyType = propertyInfo.PropertyType;
            var converter = TypeDescriptor.GetConverter(propertyType);
            propertyInfo.SetValue(toReturn, converter.ConvertFromString(result.Value));
        }

        return toReturn;
    }
}