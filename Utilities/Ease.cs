using Godot;
using System;

namespace NoromaGD
{
    public static class Easing
    {
        public enum Type
        {
            LINEAR,
            QUAD_IN,
            QUAD_OUT,
            QUAD_INOUT,
            CUBE_IN,
            CUBE_OUT,
            CUBE_INOUT,
            QUART_IN,
            QUART_OUT,
            QUART_INOUT,
            QUINT_IN,
            QUINT_OUT,
            QUINT_INOUT,
            SMOOTH_STEP_IN,
            SMOOTH_STEP_OUT,
            SMOOTH_STEP_INOUT,
            SMOOTHER_STEP_IN,
            SMOOTHER_STEP_OUT,
            SMOOTHER_STEP_INOUT,
            SIN_IN,
            SIN_OUT,
            SIN_INOUT,
            BOUNCE_IN,
            BOUNCE_OUT,
            BOUNCE_INOUT,
            CIRC_IN,
            CIRC_OUT,
            CIRC_INOUT,
            EXPO_IN,
            EXPO_OUT,
            EXPO_INOUT,
            BACK_IN,
            BACK_OUT,
            BACK_INOUT,
            ELASTIC_IN,
            ELASTIC_OUT,
            ELASTIC_INOUT
        }

        public static float Linear(float t)
        {
            return t;
        }

        public static float QuadIn(float t)
        {
            return t * t;
        }

        public static float QuadOut(float t)
        {
            return -t * (t - 2);
        }

        public static float QuadInOut(float t)
        {
            if (t <= 0.5)
                return t * t * 2;
            else
                return 1 - (t - 1) * (t - 1) * 2;
        }

        // 同様に他のイージング関数を変換

        public static float Exec(Type type, float t)
        {
            Func<float, float> function = GetFunction(type);
            return function(t);
        }

        public static float Step(Type type, float start, float end, float v)
        {
            if (start == end)
                return start;

            float a = start;
            float b = end;

            if (v <= 0.0)
                return start;

            if (v >= 1.0)
                return end;

            float d = b - a;
            float time = v;

            Func<float, float> function = GetFunction(type);
            return a + (d * function(time));
        }

        public static float Step(Type type, float start, float end, float timeCurrent, float timeEnd)
        {
            if (timeEnd == 0)
            {
                throw new DivideByZeroException("timeEnd が 0です");
            }
            return Step(type, start, end, timeCurrent / timeEnd);
        }

        private static Func<float, float> GetFunction(Type type)
        {
            switch (type)
            {
                case Type.LINEAR: return Linear;
                case Type.QUAD_IN: return QuadIn;
                case Type.QUAD_OUT: return QuadOut;
                case Type.QUAD_INOUT: return QuadInOut;
                // 同様に他のイージング関数を追加
                default:
                    GD.Print("未定義のイージング関数: " + type.ToString());
                    return Linear;
            }
        }
    }
}