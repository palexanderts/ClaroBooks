using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainEvents
{
    public class DomainEventMesage<T> : DomainEventMesage
    {
        public T Data { get; set; }
    }

    public class DomainEventMesage : IDomainEvent
    {
        public const string SuccessfulMessage = "Operación realizada correctamente";

        public enum EventType { Ok, Warning, Error }
        public EventType Type { get; set; }
        public string Message { get; set; }
        public override string ToString() => this.Message;
    }
}
