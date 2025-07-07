using Microsoft.EntityFrameworkCore;
using Rira.Core.DependencyInjection;
using Rira.Models.User;

namespace Rira.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class RiraQueryContext : IScopedSelfService
    {
        private readonly RiraCommandContext _riraCommand;

        /// <summary>
        /// 
        /// </summary>
        public RiraQueryContext(RiraCommandContext riraCommand)
        {
            _riraCommand = riraCommand;
        }


        /// <summary>
        /// 
        /// </summary>
        public IQueryable<UserEntity> Users => _riraCommand.Users.AsNoTracking();
    }
}
