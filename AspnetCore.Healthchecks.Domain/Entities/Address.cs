using System;

namespace AspnetCore.Healthchecks.Domain.Entities
{
    /// <summary>
    /// Entidade que representa o Endereço
    /// </summary>
    public class Address
    {
        public Guid Id { get; set; }
        public string Cep { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        protected Address() { }

    }
}
