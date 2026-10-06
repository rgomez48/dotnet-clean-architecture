using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public DateTime CreatedAtUtc { get; protected set; } = DateTime.UtcNow;

        public DateTime? LastModifiedAtUtc { get; protected set; }
    }
}
