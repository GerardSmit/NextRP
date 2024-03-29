using System;
using System.Numerics;
using System.Threading.Tasks;
using NextFramework.Enums;

namespace NextFramework
{
    public interface IVehicleCollection : IPool<IVehicle>
    {
        /// <summary>
        /// Create a new vehicle.
        /// </summary>
        /// <param name="model">Model of the vehicle</param>
        /// <param name="position">Position of the vehicle</param>
        /// <param name="heading">Heading of the vehicle</param>
        /// <param name="numberPlate">Number plate of the vehicle</param>
        /// <param name="alpha">Alpha of the vehicle</param>
        /// <param name="locked">Locked state of the vehicle</param>
        /// <param name="engine">Engine state of the vehicle</param>
        /// <param name="dimension">Dimension of the vehicle</param>
        /// <returns>New <see cref="IVehicle" /> instance</returns>
        /// <exception cref="ArgumentNullException"><paramref name="numberPlate"/> is null</exception>
        Task<IVehicle> NewAsync(VehicleHash model, Vector3 position, float heading = 0, string numberPlate = "", uint alpha = 255, bool locked = false, bool engine = false,
            uint dimension = Constants.GlobalDimension);

        /// <summary>
        /// Create a new vehicle.
        /// </summary>
        /// <param name="model">Model of the vehicle</param>
        /// <param name="position">Position of the vehicle</param>
        /// <param name="heading">Heading of the vehicle</param>
        /// <param name="numberPlate">Number plate of the vehicle</param>
        /// <param name="alpha">Alpha of the vehicle</param>
        /// <param name="locked">Locked state of the vehicle</param>
        /// <param name="engine">Engine state of the vehicle</param>
        /// <param name="dimension">Dimension of the vehicle</param>
        /// <returns>New <see cref="IVehicle" /> instance</returns>
        /// <exception cref="ArgumentNullException"><paramref name="numberPlate"/> is null</exception>
        Task<IVehicle> NewAsync(uint model, Vector3 position, float heading = 0, string numberPlate = "", uint alpha = 255, bool locked = false, bool engine = false,
            uint dimension = Constants.GlobalDimension);

        /// <summary>
        /// Create a new vehicle.
        /// </summary>
        /// <param name="model">Model of the vehicle</param>
        /// <param name="position">Position of the vehicle</param>
        /// <param name="heading">Heading of the vehicle</param>
        /// <param name="numberPlate">Number plate of the vehicle</param>
        /// <param name="alpha">Alpha of the vehicle</param>
        /// <param name="locked">Locked state of the vehicle</param>
        /// <param name="engine">Engine state of the vehicle</param>
        /// <param name="dimension">Dimension of the vehicle</param>
        /// <returns>New <see cref="IVehicle" /> instance</returns>
        /// <exception cref="ArgumentNullException"><paramref name="numberPlate"/> is null</exception>
        Task<IVehicle> NewAsync(int model, Vector3 position, float heading = 0, string numberPlate = "", int alpha = 255, bool locked = false, bool engine = false,
            uint dimension = Constants.GlobalDimension);
    }
}
