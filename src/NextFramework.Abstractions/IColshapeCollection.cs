using System.Numerics;
using System.Threading.Tasks;

namespace NextFramework
{
    public interface IColshapeCollection : IPool<IColshape>
    {
        /// <summary>
        /// Create a new circle colshape.
        /// </summary>
        /// <param name="position">Position of the colshape</param>
        /// <param name="radius">Radius of the colshape</param>
        /// <param name="dimension">Dimension of the colshape</param>
        /// <returns>New <see cref="IColshape" /> instance</returns>
        Task<IColshape> NewCircleAsync(Vector2 position, float radius, uint dimension = Constants.GlobalDimension);

        /// <summary>
        /// Create a new sphere colshape.
        /// </summary>
        /// <param name="position">Position of the colshape</param>
        /// <param name="radius">Radius of the colshape</param>
        /// <param name="dimension">Dimension of the colshape</param>
        /// <returns>New <see cref="IColshape" /> instance</returns>
        Task<IColshape> NewSphereAsync(Vector3 position, float radius, uint dimension = Constants.GlobalDimension);

        /// <summary>
        /// Create a new tube colshape.
        /// </summary>
        /// <param name="position">Position of the colshape</param>
        /// <param name="radius">Radius of the colshape</param>
        /// <param name="height">Height of the colshape</param>
        /// <param name="dimension">Dimension of the colshape</param>
        /// <returns>New <see cref="IColshape" /> instance</returns>
        Task<IColshape> NewTubeAsync(Vector3 position, float radius, float height, uint dimension = Constants.GlobalDimension);

        /// <summary>
        /// Create a new rectangle colshape.
        /// </summary>
        /// <param name="position">Position of the colshape</param>
        /// <param name="size">Size of the colshape</param>
        /// <param name="dimension">Dimension of the colshape</param>
        /// <returns>New <see cref="IColshape" /> instance</returns>
        Task<IColshape> NewRectangleAsync(Vector2 position, Vector2 size, uint dimension = Constants.GlobalDimension);

        /// <summary>
        /// Create a new cube colshape.
        /// </summary>
        /// <param name="position">Position of the colshape</param>
        /// <param name="size">Size of the colshape</param>
        /// <param name="dimension">Dimension of the colshape</param>
        /// <returns>New <see cref="IColshape" /> instance</returns>
        Task<IColshape> NewCubeAsync(Vector3 position, Vector3 size, uint dimension = Constants.GlobalDimension);
    }
}
