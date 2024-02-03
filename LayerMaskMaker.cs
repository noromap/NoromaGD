using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoromaGD
{
    public static class LayerMaskMaker
    {
        public static Dictionary<string, UInt32> Layers { get; private set; } = new Dictionary<string, UInt32>();
        public static Dictionary<string, UInt32> LayerValues { get; private set; } = new Dictionary<string, UInt32>();

        static LayerMaskMaker()
        {
            for (int i = 0; i < 21; i++)
            {
                var layer = ProjectSettings.GetSetting($"layer_names/2d_physics/layer_{i + 1}");
                if (layer.Obj != null && string.IsNullOrEmpty(layer.AsString()) == false)
                {
                    Layers[layer.AsString()] = (uint)Math.Pow(2, i);
                    LayerValues[layer.AsString()] = (uint)i + 1;
                }
            }
        }

        public static int GetLayer(string name)
        {
            if (LayerValues.ContainsKey(name) == false)
            {
                GD.PrintErr($"Layer : {name} は存在しません。");
                return -1;
            }
            return (int)LayerValues[name];
        }

        public static UInt32 MakeMask(params string[] layers)
        {
            var im = Layers.Where(l => layers.Contains(l.Key));
            if (im.Count() == 0)
            {
                GD.Print($"一致するレイヤーが存在しません。レイヤー：{string.Join(',', layers)}");
            }
            return (UInt32)im.Select(l => l.Value).Sum(v => v);
        }

        public static UInt32 AddLayerToMask(UInt32 mask, params string[] layers)
        {
            foreach (var layer in layers)
            {
                mask = mask | Layers[layer];
            }
            return mask;
        }

        public static UInt32 RemoveLayerFromMask(UInt32 mask, params string[] layers)
        {
            foreach (var layer in layers)
            {
                mask = mask & ~Layers[layer];
            }
            return mask;
        }
    }
}