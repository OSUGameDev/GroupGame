using System;

namespace UnityEngine.Experimental.Rendering.LWRP
{
    [Serializable]
    public struct _2DLightOperationDescription
    {
        internal enum TextureChannel
        {
            None = 0,
            R = 1,
            G = 2,
            B = 3,
            A = 4,
            OneMinusR = 5,
            OneMinusG = 6,
            OneMinusB = 7,
            OneMinusA = 8
        }

        internal struct MaskChannelFilter
        {
            public Vector4 mask { get; private set; }
            public Vector4 inverted { get; private set; }

            public MaskChannelFilter(Vector4 m, Vector4 i)
            {
                mask = m;
                inverted = i;
            }
        }

        internal enum BlendMode
        {
            Additive = 0,
            Multiply = 1,
            Subtractive = 2,
            Custom = 99
        }

        [Serializable]
        internal struct BlendFactors
        {
            [SerializeField] internal float modulate;
            [SerializeField] internal float additve;
        }

        public bool enabled;
        public string name;
        [ColorUsageAttribute(false, true)]
        [SerializeField] internal Color globalColor;
        [SerializeField] internal TextureChannel maskTextureChannel;
        [SerializeField] internal BlendMode blendMode;
        [SerializeField] internal float renderTextureScale;
        [SerializeField] internal BlendFactors customBlendFactors;



        internal Vector2 blendFactors
        {
            get
            {
                var result = new Vector2();

                switch (blendMode)
                {
                    case BlendMode.Additive:
                        result.x = 0.0f;
                        result.y = 1.0f;
                        break;
                    case BlendMode.Multiply:
                        result.x = 1.0f;
                        result.y = 0.0f;
                        break;
                    case BlendMode.Subtractive:
                        result.x = 0.0f;
                        result.y = -1.0f;
                        break;
                    case BlendMode.Custom:
                        result.x = customBlendFactors.modulate;
                        result.y = customBlendFactors.additve;
                        break;
                    default:
                        result = Vector2.zero;
                        break;
                }

                return result;
            }
        }

        internal MaskChannelFilter maskTextureChannelFilter
        {
            get
            {
                switch (maskTextureChannel)
                {
                    case TextureChannel.R:
                        return new MaskChannelFilter(new Vector4(1, 0, 0, 0), new Vector4(0, 0, 0, 0));
                    case TextureChannel.OneMinusR:
                        return new MaskChannelFilter(new Vector4(1, 0, 0, 0), new Vector4(1, 0, 0, 0));
                    case TextureChannel.G:
                        return new MaskChannelFilter(new Vector4(0, 1, 0, 0), new Vector4(0, 0, 0, 0));
                    case TextureChannel.OneMinusG:
                        return new MaskChannelFilter(new Vector4(0, 1, 0, 0), new Vector4(0, 1, 0, 0));
                    case TextureChannel.B:
                        return new MaskChannelFilter(new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 0));
                    case TextureChannel.OneMinusB:
                        return new MaskChannelFilter(new Vector4(0, 0, 1, 0), new Vector4(0, 0, 1, 0));
                    case TextureChannel.A:
                        return new MaskChannelFilter(new Vector4(0, 0, 0, 1), new Vector4(0, 0, 0, 0));
                    case TextureChannel.OneMinusA:
                        return new MaskChannelFilter(new Vector4(0, 0, 0, 1), new Vector4(0, 0, 0, 1));
                    case TextureChannel.None:
                    default:
                        return new MaskChannelFilter(Vector4.zero, Vector4.zero);
                }
            }
        }
    }
}
