public class UserService : IUserService
{
    public string GetUserInfo(int id, bool includeEmail = false)
    {
        string info = $"Пользователь #{id}";
        if (includeEmail)
            info += ", Email: user@example.com";
        return info;
    }

    public string GetUserInfo(int id)
    {
        return GetUserInfo(id, false);
    }
}
