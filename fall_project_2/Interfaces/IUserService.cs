namespace fall_project_2.Interfaces;

public interface IUserService
{
    User GetUserByEmail(string email);

    bool CheckPassword(User user, string password);

    User CreateUser(string name, string email, string password);

}