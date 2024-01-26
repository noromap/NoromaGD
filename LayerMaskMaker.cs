using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoromaGD
{
    public static class LayerMaskMaker
    {
        public static Dictionary<string, int> Layers { get; private set; } = new Dictionary<string, int>();

        static LayerMaskMaker()
        {
            for (int i = 0; i < 21; i++)
            {
                var layer = ProjectSettings.GetSetting($"layer_names/2d_physics/layer_{i + 1}");
                if (layer.Obj != null && string.IsNullOrEmpty(layer.AsString()) == false)
                    Layers[layer.AsString()] = (int)Math.Pow(2, i);
            }
        }

        public static UInt32 MakeMask(params string[] layers)
        {
            var im = Layers.Where(l => layers.Contains(l.Key));
            if (im.Count() == 0)
            {
                GD.Print($"一致するレイヤーが存在しません。レイヤー：{string.Join(',', layers)}");
            }
            return (UInt32)im.Select(l => l.Value).Sum();
        }
    }
}