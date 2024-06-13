namespace RepositoryExample;

using System.Data;

public class MyRepository: IRepository
{
    private readonly IDbConnection connection;

    public MyRepository(IDbConnection connection)
    {
        this.connection = connection;
    }

    public void Open()
    {
        this.connection.Open();
    }

    public void Close()
    {
        this.connection.Close();
    }

    public void InsertFoo(string name)
    {
        using var command = this.connection.CreateCommand();
        command.CommandText = "INSERT INTO foos (name) VALUES (?)";

        var nameParam = command.CreateParameter();
        nameParam.ParameterName = "name";
        nameParam.DbType = DbType.String;
        nameParam.Value = name;
        command.Parameters.Add(nameParam);

        command.ExecuteNonQuery();
    }
}
