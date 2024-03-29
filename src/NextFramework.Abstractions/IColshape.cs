using System.Numerics;
using NextFramework.Enums;
using NextFramework.Exceptions;

namespace NextFramework
{
    public interface IColshape : IEntity
    {
        /// <summary>
        /// Get the type of the colshape.
        /// </summary>
        /// <exception cref="EntityDeletedException">This entity was deleted before</exception>
        ColshapeType ShapeType { get; }

        /// <summary>
        /// Check if a position is within the colshape.
        /// </summary>
        /// <param name="position">Position to check</param>
        /// <exception cref="EntityDeletedException">This entity was deleted before</exception>
        /// <returns>True if the <paramref name="position" /> is inside the colshape, otherwise false</returns>
        bool IsPointWhithin(Vector3 position);
    }
}
