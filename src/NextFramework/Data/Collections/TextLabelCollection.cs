using System;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using NextFramework.Data.Entities;
using NextFramework.Helpers;
using NextFramework.Native;

namespace NextFramework.Data.Collections
{
    internal class TextLabelCollection : CollectionBase<ITextLabel>, ITextLabelCollection
    {
        public TextLabelCollection(IntPtr nativePointer) : base(nativePointer)
        {
        }

        public async Task<ITextLabel> NewAsync(Vector3 position, string text, uint font, Color color, float drawDistance, bool los, uint dimension)
        {
            Contract.NotNull(text, nameof(text));
            
            using (var converter = new StringConverter())
            {
                var textPointer = converter.StringToPointer(text);

                var pointer = await TickScheduler.Instance
                    .Schedule(() => Rage.TextLabelPool.TextLabelPool_New(_nativePointer, position, textPointer, font, color, drawDistance, los, dimension));

                return CreateAndSaveEntity(pointer);
            }
        }

        public Task<ITextLabel> NewAsync(Vector3 position, string text, int font, Color color, float drawDistance, bool los, uint dimension)
        {
            return NewAsync(position, text, (uint) font, color, drawDistance, los, dimension);
        }

        protected override ITextLabel BuildEntity(IntPtr entityPointer)
        {
            return new TextLabel(entityPointer);
        }
    }
}
