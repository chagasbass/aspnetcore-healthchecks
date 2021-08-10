using System;

namespace AspnetCore.Healthchecks.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public string CEP { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        protected Address() { }

    }
}
