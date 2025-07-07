using System.ComponentModel.DataAnnotations;
using Rira.Core.Contracts;
using Rira.Core.Entity;

namespace Rira.Models.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserEntity : EntityBase
    {
        private UserEntity() { }

        /// <inheritdoc />
        public UserEntity(string firstName,
            string lastName,
            string nationalCode,
            DateOnly birthday)
        {
            FirstName = firstName;
            LastName = lastName;
            NationalCode = nationalCode;
            Birthday = birthday;
        }

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int UserId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(1000)]
        public string FirstName { get; private set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(1000)]
        public string LastName { get; private set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Length(10 , 10)]
        public string NationalCode { get; private set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public DateOnly Birthday { get; private set; } = DateOnly.MinValue;

        /// <summary>
        /// 
        /// </summary>
        public void Update(string firstName,
            string lastName,
            string nationalCode,
            DateOnly birthday,
            IClockService clock)
        {
            FirstName = firstName;
            LastName = lastName;
            NationalCode = nationalCode;
            Birthday = birthday;
            Updated = clock.Now("User.Update");
        }


        /// <summary>
        /// 
        /// </summary>
        public void Delete(IClockService clock)
        {
            Deleted = true;
            Updated = clock.Now("User.Delete");
        }
    }
}
