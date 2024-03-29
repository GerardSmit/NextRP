using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using NextFramework.Enums;

namespace NextFramework
{
    public interface IMarkerCollection : IPool<IMarker>
    {
        /// <summary>
        /// Create a new marker.
        /// </summary>
        /// <param name="type">Model of the marker</param>
        /// <param name="position">Position of the marker</param>
        /// <param name="rotation">Rotation of the marker</param>
        /// <param name="direction">Direction of the marker</param>
        /// <param name="scale">Scale of the marker</param>
        /// <param name="color">Color of the marker</param>
        /// <param name="visible">Visible state of the marker</param>
        /// <param name="dimension">Dimension of the marker</param>
        /// <returns>New <see cref="IMarker" /> instance</returns>
        Task<IMarker> NewAsync(MarkerType type, Vector3 position, Vector3 rotation, Vector3 direction, float scale, Color color, bool visible, uint dimension = Constants.GlobalDimension);

        /// <summary>
        /// Create a new marker.
        /// </summary>
        /// <param name="type">Model of the marker</param>
        /// <param name="position">Position of the marker</param>
        /// <param name="rotation">Rotation of the marker</param>
        /// <param name="direction">Direction of the marker</param>
        /// <param name="scale">Scale of the marker</param>
        /// <param name="color">Color of the marker</param>
        /// <param name="visible">Visible state of the marker</param>
        /// <param name="dimension">Dimension of the marker</param>
        /// <returns>New <see cref="IMarker" /> instance</returns>
        Task<IMarker> NewAsync(uint type, Vector3 position, Vector3 rotation, Vector3 direction, float scale, Color color, bool visible, uint dimension = Constants.GlobalDimension);

        /// <summary>
        /// Create a new marker.
        /// </summary>
        /// <param name="type">Model of the marker</param>
        /// <param name="position">Position of the marker</param>
        /// <param name="rotation">Rotation of the marker</param>
        /// <param name="direction">Direction of the marker</param>
        /// <param name="scale">Scale of the marker</param>
        /// <param name="color">Color of the marker</param>
        /// <param name="visible">Visible state of the marker</param>
        /// <param name="dimension">Dimension of the marker</param>
        /// <returns>New <see cref="IMarker" /> instance</returns>
        Task<IMarker> NewAsync(int type, Vector3 position, Vector3 rotation, Vector3 direction, float scale, Color color, bool visible, uint dimension = Constants.GlobalDimension);
    }
}
