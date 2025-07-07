
namespace Rira.Models.User
{
    /// <summary>
    /// 
    /// </summary>
    public static class UserUtility
    {
        /// <summary>
        /// 
        /// </summary>
        public static IQueryable<UserEntity> ValidPredicate(this IQueryable<UserEntity> queryable)
        {
            return queryable.Where(m => !m.Deleted);
        }
    }
}
