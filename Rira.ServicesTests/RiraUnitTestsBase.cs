using Microsoft.EntityFrameworkCore;
using Rira.Data;

namespace Rira.ServicesTests
{
    public class RiraUnitTestsBase 
    {
        private readonly Lazy<RiraCommandContext> _riraCommandContext;
        private readonly Lazy<RiraQueryContext> _riraQueryContext;

        /// <summary>
        /// 
        /// </summary>
        public RiraUnitTestsBase()
        {

            _riraCommandContext = new Lazy<RiraCommandContext>(() =>
            {
                var options = new DbContextOptionsBuilder<RiraCommandContext>().EnableSensitiveDataLogging().UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;
                return new RiraCommandContext(options);
            });
            _riraQueryContext = new Lazy<RiraQueryContext>(() => new RiraQueryContext(_riraCommandContext.Value));
        }

        public RiraCommandContext RiraCommand => _riraCommandContext.Value;
        public RiraQueryContext RiraQuery => _riraQueryContext.Value;
    }
}
