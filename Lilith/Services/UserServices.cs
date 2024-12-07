namespace EnPassant.Lilith.Services;

public interface IUserService
{
    public void TestApiAsync(CancellationToken cancellationToken);
}

public class UserService : IUserService
{

    public UserService()
    {

    }

    public void TestApiAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Working");
    }
}
