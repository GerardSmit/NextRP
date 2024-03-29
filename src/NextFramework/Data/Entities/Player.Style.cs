using System.Collections.Generic;
using System.Linq;
using NextFramework.Enums;
using NextFramework.Helpers;
using NextFramework.Native;

namespace NextFramework.Data.Entities
{
    internal partial class Player
    {
        public new PedHash Model
        {
            get
            {
                CheckExistence();

                return (PedHash) base.Model;
            }
            set
            {
                CheckExistence();

                base.Model = (uint) value;
            }
        }

        public uint EyeColor
        {
            get
            {
                CheckExistence();

                return Rage.Player.Player_GetEyeColor(NativePointer);
            }
            set
            {
                CheckExistence();

                Rage.Player.Player_SetEyeColor(NativePointer, value);
            }
        }

        public uint HairColor
        {
            get
            {
                CheckExistence();

                return Rage.Player.Player_GetHairColor(NativePointer);
            }
        }

        public uint HairHighlightColor
        {
            get
            {
                CheckExistence();

                return Rage.Player.Player_GetHairHighlightColor(NativePointer);
            }
        }

        public HeadBlendData HeadBlend
        {
            get
            {
                CheckExistence();

                return StructConverter.PointerToStruct<HeadBlendData>(Rage.Player.Player_GetHeadBlend(NativePointer));
            }
            set
            {
                CheckExistence();

                Rage.Player.Player_SetHeadBlend(NativePointer, value.Shape[0], value.Shape[1], value.Shape[2], value.Skin[0], value.Skin[1], value.Skin[2], value.ShapeMix,
                    value.SkinMix, value.ThirdMix);
            }
        }

        public ClothData GetCloth(ClothSlot slot)
        {
            CheckExistence();

            return StructConverter.PointerToStruct<ClothData>(Rage.Player.Player_GetClothes(NativePointer, (uint) slot));
        }

        public ClothData GetCloth(int slot)
        {
            return GetCloth((ClothSlot) slot);
        }

        public void SetCloth(ClothSlot slot, ClothData data)
        {
            CheckExistence();

            Rage.Player.Player_SetCloth(NativePointer, (uint) slot, data);
        }

        public void SetCloth(ClothSlot slot, byte drawable, byte texture, byte palette)
        {
            SetCloth(slot, new ClothData(drawable, texture, palette));
        }

        public void SetClothes(IDictionary<ClothSlot, ClothData> clothes)
        {
            Contract.NotNull(clothes, nameof(clothes));
            CheckExistence();

            var keys = clothes.Keys.Select(x => (uint) x).ToArray();
            var values = clothes.Values.ToArray();

            Rage.Player.Player_SetClothes(NativePointer, keys, values, (ulong) keys.Length);
        }

        public PropData GetProp(PropSlot slot)
        {
            CheckExistence();

            return StructConverter.PointerToStruct<PropData>(Rage.Player.Player_GetProp(NativePointer, (uint) slot));
        }

        public PropData GetProp(int slot)
        {
            return GetProp((PropSlot) slot);
        }

        public void SetProp(PropSlot slot, PropData data)
        {
            CheckExistence();

            Rage.Player.Player_SetProp(NativePointer, (uint) slot, data);
        }

        public void SetProp(PropSlot slot, byte drawable, byte texture)
        {
            SetProp(slot, new PropData(drawable, texture));
        }

        public void SetProps(IDictionary<PropSlot, PropData> props)
        {
            Contract.NotNull(props, nameof(props));
            CheckExistence();

            var keys = props.Keys.Select(x => (uint) x).ToArray();
            var values = props.Values.ToArray();

            Rage.Player.Player_SetProps(NativePointer, keys, values, (ulong) keys.Length);
        }

        public uint GetDecoration(uint collection)
        {
            CheckExistence();

            return Rage.Player.Player_GetDecoration(NativePointer, collection);
        }

        public int GetDecoration(int collection)
        {
            return (int) GetDecoration((uint) collection);
        }

        public void RemoveDecoration(uint collection, uint overlay)
        {
            CheckExistence();

            Rage.Player.Player_RemoveDecoration(NativePointer, collection, overlay);
        }

        public void RemoveDecoration(int collection, int overlay)
        {
            RemoveDecoration((uint) collection, (uint) overlay);
        }

        public void SetDecoration(uint collection, uint overlay)
        {
            CheckExistence();

            Rage.Player.Player_SetDecoration(NativePointer, collection, overlay);
        }

        public void SetDecoration(int collection, int overlay)
        {
            SetDecoration((uint) collection, (uint) overlay);
        }

        public void SetDecorations(IDictionary<uint, uint> decorations)
        {
            Contract.NotNull(decorations, nameof(decorations));
            CheckExistence();

            var keys = decorations.Keys.ToArray();
            var values = decorations.Values.ToArray();

            Rage.Player.Player_SetDecorations(NativePointer, keys, values, (ulong) keys.Length);
        }

        public void SetDecorations(IDictionary<int, int> decorations)
        {
            SetDecorations(decorations.ToDictionary(x => (uint) x.Key, x => (uint) x.Value));
        }

        public void ClearDecorations()
        {
            CheckExistence();

            Rage.Player.Player_ClearDecorations(NativePointer);
        }

        public void SetHairColor(uint color, uint highlightColor)
        {
            CheckExistence();

            Rage.Player.Player_SetHairColor(NativePointer, color, highlightColor);
        }

        public void SetHairColor(int color, int highlightColor)
        {
            SetHairColor((uint) color, (uint) highlightColor);
        }

        public float GetFaceFeature(uint id)
        {
            CheckExistence();

            return Rage.Player.Player_GetFaceFeature(NativePointer, id);
        }

        public float GetFaceFeature(int id)
        {
            return GetFaceFeature((uint) id);
        }

        public void SetFaceFeature(uint id, float scale)
        {
            CheckExistence();

            Rage.Player.Player_SetFaceFeature(NativePointer, id, scale);
        }

        public void SetFaceFeature(int id, float scale)
        {
            SetFaceFeature((uint) id, scale);
        }

        public void UpdateHeadBlend(float shapeMix, float skinMix, float thirdMix)
        {
            CheckExistence();

            Rage.Player.Player_UpdateHeadBlend(NativePointer, shapeMix, skinMix, thirdMix);
        }

        public HeadOverlayData GetHeadOverlay(uint overlayId)
        {
            CheckExistence();

            return StructConverter.PointerToStruct<HeadOverlayData>(Rage.Player.Player_GetHeadOverlay(NativePointer, overlayId));
        }

        public HeadOverlayData GetHeadOverlay(int overlayId)
        {
            return GetHeadOverlay((uint) overlayId);
        }

        public void SetHeadOverlay(uint overlayId, HeadOverlayData overlayData)
        {
            CheckExistence();

            Rage.Player.Player_SetHeadOverlay(NativePointer, overlayId, overlayData);
        }

        public void SetHeadOverlay(int overlayId, HeadOverlayData overlayData)
        {
            SetHeadOverlay((uint) overlayId, overlayData);
        }

        public void SetCustomization(bool isMale, HeadBlendData headBlend, uint eyeColor, uint hairColor, uint highlightColor, float[] faceFeatures,
            IDictionary<int, HeadOverlayData> headOverlays, IDictionary<uint, uint> decorations)
        {
            Contract.NotNull(headOverlays, nameof(headOverlays));
            Contract.NotNull(decorations, nameof(decorations));
            CheckExistence();

            var headOverlayKeys = headOverlays.Keys.ToArray();
            var headOverlayValues = headOverlays.Values.ToArray();

            var decorationKeys = decorations.Keys.ToArray();
            var decorationValues = decorations.Values.ToArray();

            Rage.Player.Player_SetCustomization(NativePointer, isMale, headBlend, eyeColor, hairColor, highlightColor, faceFeatures, (ulong) faceFeatures.Length, headOverlayKeys,
                headOverlayValues, (ulong) headOverlayKeys.Length, decorationKeys, decorationValues, (ulong) decorationKeys.Length);
        }

        public void SetCustomization(bool isMale, HeadBlendData headBlend, int eyeColor, int hairColor, int highlightColor, float[] faceFeatures,
            IDictionary<int, HeadOverlayData> headOverlays, IDictionary<int, int> decorations)
        {
            SetCustomization(isMale, headBlend, (uint) eyeColor, (uint) hairColor, (uint) highlightColor, faceFeatures, headOverlays,
                decorations.ToDictionary(x => (uint) x.Key, x => (uint) x.Value));
        }
    }
}
