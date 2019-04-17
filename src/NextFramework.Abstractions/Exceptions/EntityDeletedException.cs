using System;

namespace NextFramework.Exceptions
{
    public class EntityDeletedException : Exception
    {
        public EntityDeletedException(IEntity entity) : base($"This entity ({entity.Type.ToString()}: ID {entity.Id}) does not exist anymore!")
        {

        }

        public EntityDeletedException(IEntity entity, string parameterName) : base($"Parameter {parameterName}: This entity ({entity.Type.ToString()}: ID {entity.Id}) does not exist anymore!")
        {

        }
    }
}
