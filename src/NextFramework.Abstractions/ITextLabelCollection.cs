using System;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;

namespace NextFramework
{
    public interface ITextLabelCollection : IPool<ITextLabel>
    {
        /// <summary>
        /// Create a new text label.
        /// </summary>
        /// <param name="position">Position of the text label</param>
        /// <param name="text">Text of the text label</param>
        /// <param name="font">Font of the text label</param>
        /// <param name="color">Color of the text label</param>
        /// <param name="drawDistance">Draw distance of the text label</param>
        /// <param name="los">Line of sight state of the text label</param>
        /// <param name="dimension">Dimension of the text label</param>
        /// <returns>New <see cref="ITextLabel" /> instance</returns>
        /// <exception cref="ArgumentNullException"><paramref name="text"/> is null</exception>
        Task<ITextLabel> NewAsync(Vector3 position, string text, uint font, Color color, float drawDistance = 20, bool los = false, uint dimension = Constants.GlobalDimension);

        /// <summary>
        /// Create a new text label.
        /// </summary>
        /// <param name="position">Position of the text label</param>
        /// <param name="text">Text of the text label</param>
        /// <param name="font">Font of the text label</param>
        /// <param name="color">Color of the text label</param>
        /// <param name="drawDistance">Draw distance of the text label</param>
        /// <param name="los">Line of sight state of the text label</param>
        /// <param name="dimension">Dimension of the text label</param>
        /// <returns>New <see cref="ITextLabel" /> instance</returns>
        /// <exception cref="ArgumentNullException"><paramref name="text"/> is null</exception>
        Task<ITextLabel> NewAsync(Vector3 position, string text, int font, Color color, float drawDistance = 20, bool los = false, uint dimension = Constants.GlobalDimension);
    }
}
