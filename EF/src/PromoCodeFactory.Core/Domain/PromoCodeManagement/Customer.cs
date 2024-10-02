using System.Collections.Generic;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public sealed class Customer
        : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Email { get; set; }

        public ICollection<Preference> Preferences { get; set; }

        public ICollection<PromoCode> Promocodes { get; set; }
    }
}