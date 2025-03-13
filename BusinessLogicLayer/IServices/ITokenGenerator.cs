namespace BusinessLogicLayer.Interface
{
    public interface ITokenGenerator
    {
        /// <summary>
        /// Generates   a token from the specified  user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        string GenerateToken(string userId, string email);
    }
}
