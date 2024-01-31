using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NoromaGD
{
    public static class CSExtensions
    {
        #region IEnumerable

        public static void Do<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        #endregion IEnumerable

        #region Int

        public static float SignFloat(this int value)
        {
            return Mathf.Sign(value);
        }

        public static int Sign(this int value)
        {
            return Mathf.Sign(value);
        }

        #endregion Int

        #region Float

        public static float Abs(this float x)
        {
            return Mathf.Abs(x);
        }

        public static float Sign(this float x)
        {
            return Mathf.Sign(x);
        }

        public static int SignInt(this float x)
        {
            return Mathf.Sign(x);
        }

        public static float DegToRad(this float x)
        {
            return Mathf.DegToRad(x);
        }

        public static float RadToDeg(this float x)
        {
            return Mathf.RadToDeg(x);
        }

        public static float Pow(this float x, float y)
        {
            return Mathf.Pow(x, y);
        }

        #endregion Float

        #region double

        public static double Abs(this double x)
        {
            return Mathf.Abs(x);
        }

        public static double Sign(this double x)
        {
            return Mathf.Sign(x);
        }

        public static double DegToRad(this double x)
        {
            return Mathf.DegToRad(x);
        }

        public static double RadToDeg(this double x)
        {
            return Mathf.RadToDeg(x);
        }

        public static double Pow(this double x, double y)
        {
            return Mathf.Pow(x, y);
        }

        #endregion double

        #region string

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        #endregion string
    }
}