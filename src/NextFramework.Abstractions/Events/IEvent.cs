namespace NextFramework.Events
{
    public interface IEvent
    {
        /// <summary>
        ///     True if the event was cancelled.
        /// </summary>
        bool IsPropagationStopped { get; set; }
    }
}
