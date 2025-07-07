using Rira.Core.Contracts;

namespace Rira.Core.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class EntityBase : IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public EntityBase()
        {
            Created = DateTime.UtcNow;
            Updated = DateTime.UtcNow;
        }


        /// <summary>
        /// 
        /// </summary>
        public EntityBase(DateTime created)
        {
            Created = created;
            Updated = created;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Deleted { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Created { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Updated { get; protected set; }
    }
}
