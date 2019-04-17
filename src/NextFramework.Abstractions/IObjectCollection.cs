using System.Numerics;
using System.Threading.Tasks;

namespace NextFramework
{
    public interface IObjectCollection : IPool<IObject>
    {
        /// <summary>
        /// Create a new object.
        /// </summary>
        /// <param name="model">Model of the object</param>
        /// <param name="position">Position of the object</param>
        /// <param name="rotation">Rotation of the object</param>
        /// <param name="dimension">Dimension of the object</param>
        /// <returns>New <see cref="IObject" /> instance</returns>
        Task<IObject> NewAsync(uint model, Vector3 position, Vector3 rotation, uint dimension = Constants.GlobalDimension);

        /// <summary>
        /// Create a new object.
        /// </summary>
        /// <param name="model">Model of the object</param>
        /// <param name="position">Position of the object</param>
        /// <param name="rotation">Rotation of the object</param>
        /// <param name="dimension">Dimension of the object</param>
        /// <returns>New <see cref="IObject" /> instance</returns>
        Task<IObject> NewAsync(int model, Vector3 position, Vector3 rotation, uint dimension = Constants.GlobalDimension);
    }
}
