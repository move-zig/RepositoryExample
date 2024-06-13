namespace RepositoryExample;

public interface IRepository
{
    public void Open();

    public void Close();

    public void InsertFoo(string name);
}
