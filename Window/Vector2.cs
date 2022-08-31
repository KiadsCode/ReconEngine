using Recon.Math;
using System;
using System.Runtime.InteropServices;

namespace Recon
{
    namespace Window
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Vector2f is an utility class for manipulating 2 dimensional
        /// vectors with float components
        /// </summary>
        ////////////////////////////////////////////////////////////
        [StructLayout(LayoutKind.Sequential)]
        public struct Vector2
        {
            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the vector from its coordinates
            /// </summary>
            /// <param name="x">X coordinate</param>
            /// <param name="y">Y coordinate</param>
            ////////////////////////////////////////////////////////////
            public Vector2(float x, float y)
            {
                X = x;
                Y = y;
            }

            public static Vector2 Zero
            {
                get { return new Vector2(0, 0); }
            }

            public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
            {
                t = Mathf.ClampA(t);
                return new Vector2(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator - overload ; returns the opposite of a vector
            /// </summary>
            /// <param name="v">Vector to negate</param>
            /// <returns>-v</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2 operator -(Vector2 v)
            {
                return new Vector2(-v.X, -v.Y);
            }

            public static Vector2 MoveTo(Vector2 current, Vector2 target, float maxDistanceDelta)
            {
                float num = target.X - current.X;
                float num2 = target.Y - current.Y;
                float num3 = num * num + num2 * num2;
                bool flag = num3 == 0f || (maxDistanceDelta >= 0f && num3 <= maxDistanceDelta * maxDistanceDelta);
                Vector2 result;
                if (flag)
                {
                    result = target;
                }
                else
                {
                    float num4 = (float)System.Math.Sqrt((double)num3);
                    result = new Vector2(current.X + num / num4 * maxDistanceDelta, current.Y + num2 / num4 * maxDistanceDelta);
                }
                return result;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator - overload ; subtracts two vectors
            /// </summary>
            /// <param name="v1">First vector</param>
            /// <param name="v2">Second vector</param>
            /// <returns>v1 - v2</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2 operator -(Vector2 v1, Vector2 v2)
            {
                return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator + overload ; add two vectors
            /// </summary>
            /// <param name="v1">First vector</param>
            /// <param name="v2">Second vector</param>
            /// <returns>v1 + v2</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2 operator +(Vector2 v1, Vector2 v2)
            {
                return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator * overload ; multiply a vector by a scalar value
            /// </summary>
            /// <param name="v">Vector</param>
            /// <param name="x">Scalar value</param>
            /// <returns>v * x</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2 operator *(Vector2 v, float x)
            {
                return new Vector2(v.X * x, v.Y * x);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator * overload ; multiply a scalar value by a vector
            /// </summary>
            /// <param name="x">Scalar value</param>
            /// <param name="v">Vector</param>
            /// <returns>x * v</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2 operator *(float x, Vector2 v)
            {
                return new Vector2(v.X * x, v.Y * x);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator / overload ; divide a vector by a scalar value
            /// </summary>
            /// <param name="v">Vector</param>
            /// <param name="x">Scalar value</param>
            /// <returns>v / x</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2 operator /(Vector2 v, float x)
            {
                return new Vector2(v.X / x, v.Y / x);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Provide a string describing the object
            /// </summary>
            /// <returns>String description of the object</returns>
            ////////////////////////////////////////////////////////////
            public override string ToString()
            {
                return "[Vector2f]" +
                       " X(" + X + ")" +
                       " Y(" + Y + ")";
            }

            /// <summary>X (horizontal) component of the vector</summary>
            public float X;
 
            /// <summary>Y (vertical) component of the vector</summary>
            public float Y;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Vector2i is an utility class for manipulating 2 dimensional
        /// vectors with integer components
        /// </summary>
        ////////////////////////////////////////////////////////////
        [StructLayout(LayoutKind.Sequential)]
        public struct Vector2I
        {
            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the vector from its coordinates
            /// </summary>
            /// <param name="x">X coordinate</param>
            /// <param name="y">Y coordinate</param>
            ////////////////////////////////////////////////////////////
            public Vector2I(int x, int y)
            {
                X = x;
                Y = y;
            }

            public static Vector2I Lerp(Vector2I a, Vector2I b, float t)
            {
                t = Mathf.ClampA(t);
                return new Vector2I(a.X + (b.X - a.X) * (int)t, a.Y + (b.Y - a.Y) * (int)t);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator - overload ; returns the opposite of a vector
            /// </summary>
            /// <param name="v">Vector to negate</param>
            /// <returns>-v</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2I operator -(Vector2I v)
            {
                return new Vector2I(-v.X, -v.Y);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator - overload ; subtracts two vectors
            /// </summary>
            /// <param name="v1">First vector</param>
            /// <param name="v2">Second vector</param>
            /// <returns>v1 - v2</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2I operator -(Vector2I v1, Vector2I v2)
            {
                return new Vector2I(v1.X - v2.X, v1.Y - v2.Y);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator + overload ; add two vectors
            /// </summary>
            /// <param name="v1">First vector</param>
            /// <param name="v2">Second vector</param>
            /// <returns>v1 + v2</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2I operator +(Vector2I v1, Vector2I v2)
            {
                return new Vector2I(v1.X + v2.X, v1.Y + v2.Y);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator * overload ; multiply a vector by a scalar value
            /// </summary>
            /// <param name="v">Vector</param>
            /// <param name="x">Scalar value</param>
            /// <returns>v * x</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2I operator *(Vector2I v, int x)
            {
                return new Vector2I(v.X * x, v.Y * x);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator * overload ; multiply a scalar value by a vector
            /// </summary>
            /// <param name="x">Scalar value</param>
            /// <param name="v">Vector</param>
            /// <returns>x * v</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2I operator *(int x, Vector2I v)
            {
                return new Vector2I(v.X * x, v.Y * x);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator / overload ; divide a vector by a scalar value
            /// </summary>
            /// <param name="v">Vector</param>
            /// <param name="x">Scalar value</param>
            /// <returns>v / x</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2I operator /(Vector2I v, int x)
            {
                return new Vector2I(v.X / x, v.Y / x);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Provide a string describing the object
            /// </summary>
            /// <returns>String description of the object</returns>
            ////////////////////////////////////////////////////////////
            public override string ToString()
            {
                return "[Vector2i]" +
                       " X(" + X + ")" +
                       " Y(" + Y + ")";
            }

            /// <summary>X (horizontal) component of the vector</summary>
            public int X;
 
            /// <summary>Y (vertical) component of the vector</summary>
            public int Y;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Vector2u is an utility class for manipulating 2 dimensional
        /// vectors with unsigned integer components
        /// </summary>
        ////////////////////////////////////////////////////////////
        [StructLayout(LayoutKind.Sequential)]
        public struct Vector2UI
        {
            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the vector from its coordinates
            /// </summary>
            /// <param name="x">X coordinate</param>
            /// <param name="y">Y coordinate</param>
            ////////////////////////////////////////////////////////////
            public Vector2UI(uint x, uint y)
            {
                X = x;
                Y = y;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator - overload ; subtracts two vectors
            /// </summary>
            /// <param name="v1">First vector</param>
            /// <param name="v2">Second vector</param>
            /// <returns>v1 - v2</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2UI operator -(Vector2UI v1, Vector2UI v2)
            {
                return new Vector2UI(v1.X - v2.X, v1.Y - v2.Y);
            }

            public static Vector2UI Lerp(Vector2UI a, Vector2UI b, float t)
            {
                t = Mathf.ClampA(t);
                return new Vector2UI(a.X + (b.X - a.X) * (uint)t, a.Y + (b.Y - a.Y) * (uint)t);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator + overload ; add two vectors
            /// </summary>
            /// <param name="v1">First vector</param>
            /// <param name="v2">Second vector</param>
            /// <returns>v1 + v2</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2UI operator +(Vector2UI v1, Vector2UI v2)
            {
                return new Vector2UI(v1.X + v2.X, v1.Y + v2.Y);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator * overload ; multiply a vector by a scalar value
            /// </summary>
            /// <param name="v">Vector</param>
            /// <param name="x">Scalar value</param>
            /// <returns>v * x</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2UI operator *(Vector2UI v, uint x)
            {
                return new Vector2UI(v.X * x, v.Y * x);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator * overload ; multiply a scalar value by a vector
            /// </summary>
            /// <param name="x">Scalar value</param>
            /// <param name="v">Vector</param>
            /// <returns>x * v</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2UI operator *(uint x, Vector2UI v)
            {
                return new Vector2UI(v.X * x, v.Y * x);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator / overload ; divide a vector by a scalar value
            /// </summary>
            /// <param name="v">Vector</param>
            /// <param name="x">Scalar value</param>
            /// <returns>v / x</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2UI operator /(Vector2UI v, uint x)
            {
                return new Vector2UI(v.X / x, v.Y / x);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Provide a string describing the object
            /// </summary>
            /// <returns>String description of the object</returns>
            ////////////////////////////////////////////////////////////
            public override string ToString()
            {
                return "[Vector2u]" +
                       " X(" + X + ")" +
                       " Y(" + Y + ")";
            }

            /// <summary>X (horizontal) component of the vector</summary>
            public uint X;

            /// <summary>Y (vertical) component of the vector</summary>
            public uint Y;
        }
    }
}
