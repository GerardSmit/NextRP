using System;
using NextFramework.Data;
using NextFramework.Enums;

namespace NextFramework
{
    public interface IWorld
    {
        /// <summary>
        /// Get or set the time of the world.
        /// </summary>
        TimeData Time { get; set; }

        /// <summary>
        /// Get or set the weather of the world.
        ///
        /// If value is set to WeatherType.XMAS, snow will lay on the ground.
        /// </summary>
        WeatherType Weather { get; set; }

        /// <summary>
        /// Get or set the traffic lights lock state of the world.
        /// </summary>
        bool AreTrafficLightsLocked { get; set; }

        /// <summary>
        /// Get or set the traffic light state of the world.
        /// </summary>
        int TrafficLightsState { get; set; }

        /// <summary>
        /// Start a weather transition on the world.
        /// </summary>
        /// <param name="weather">New weather to transit to</param>
        /// <param name="time">Duration of the transition</param>
        void SetWeatherTransition(WeatherType weather, float time = 0.5f);

        /// <summary>
        /// Request an ipl in the world.
        /// </summary>
        /// <param name="ipl">Name of the ipl to load</param>
        /// <exception cref="ArgumentNullException"><paramref name="ipl" /> is null or empty</exception>
        void RequestIpl(string ipl);

        /// <summary>
        /// Remove an ipl from the world.
        /// </summary>
        /// <param name="ipl">Name of the ipl to unload</param>
        /// <exception cref="ArgumentNullException"><paramref name="ipl" /> is null or empty</exception>
        void RemoveIpl(string ipl);
    }
}
