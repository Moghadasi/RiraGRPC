namespace Rira.Contracts.User.Query
{
    /// <summary>
    /// 
    /// </summary>
    public record GetUserOutput(int UserId,
        string FirstName,
        string LastName,
        string NationalCode,
        DateOnly Birthday);
}