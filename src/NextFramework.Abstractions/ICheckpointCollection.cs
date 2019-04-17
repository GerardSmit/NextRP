using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;

namespace NextFramework
{
    public interface ICheckpointCollection : IPool<ICheckpoint>
    {
        /// <summary>
        /// Create a new checkpoint.
        /// </summary>
        /// <param name="type">Type of the checkpoint</param>
        /// <param name="position">Position of the checkpoint</param>
        /// <param name="nextPosition">Next position of the checkpoint</param>
        /// <param name="radius">Radius of the checkpoint</param>
        /// <param name="color">Color of the checkpoint</param>
        /// <param name="visible">Visible state of the checkpoint</param>
        /// <param name="dimension">Dimension of the checkpoint</param>
        /// <returns>New <see cref="ICheckpoint" /> instance</returns>
        Task<ICheckpoint> NewAsync(uint type, Vector3 position, Vector3 nextPosition, float radius, Color color, bool visible = true, uint dimension = Constants.GlobalDimension);

        /// <summary>
        /// Create a new checkpoint.
        /// </summary>
        /// <param name="type">Type of the checkpoint</param>
        /// <param name="position">Position of the checkpoint</param>
        /// <param name="nextPosition">Next position of the checkpoint</param>
        /// <param name="radius">Radius of the checkpoint</param>
        /// <param name="color">Color of the checkpoint</param>
        /// <param name="visible">Visible state of the checkpoint</param>
        /// <param name="dimension">Dimension of the checkpoint</param>
        /// <returns>New <see cref="ICheckpoint" /> instance</returns>
        Task<ICheckpoint> NewAsync(int type, Vector3 position, Vector3 nextPosition, float radius, Color color, bool visible = true, uint dimension = Constants.GlobalDimension);
    }
}
