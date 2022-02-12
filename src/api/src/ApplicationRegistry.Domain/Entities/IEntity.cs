using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Database
{
    public interface IEntity
    {
        DateTimeOffset CreateDate { get; }
    }

    public interface IEntity<T>: IEntity
    {
        T Id { get; }        
    }
}
