using System;
using System.Collections.Generic;

namespace Logistika.Service.Common.Entities
{
    public class Person : BaseObject
    {
        public string PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Suffix { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public IList<Address> Addresses { get; set; }
        public Contact Contact { get; set; }
        public string Photo { get; set; }

    }

}
