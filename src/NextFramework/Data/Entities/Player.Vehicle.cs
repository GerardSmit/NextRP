using NextFramework.Data.Collections;
using NextFramework.Helpers;
using NextFramework.Native;

namespace NextFramework.Data.Entities
{
    internal partial class Player
    {
        public IVehicle Vehicle
        {
            get
            {
                CheckExistence();

                var pointer = Rage.Player.Player_GetVehicle(NativePointer);

                return Collections.Collections.VehicleCollection[pointer];
            }
        }

        public bool IsInVehicle => Vehicle != null;

        public int Seat
        {
            get
            {
                CheckExistence();

                return Rage.Player.Player_GetSeat(NativePointer);
            }
        }

        public void PutIntoVehicle(IVehicle vehicle, int seat)
        {
            Contract.NotNull(vehicle, nameof(vehicle));
            CheckExistence();

            Rage.Player.Player_PutIntoVehicle(NativePointer, vehicle.NativePointer, seat);
        }

        public void RemoveFromVehicle()
        {
            CheckExistence();

            Rage.Player.Player_RemoveFromVehicle(NativePointer);
        }
    }
}
