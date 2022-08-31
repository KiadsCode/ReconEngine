using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Recon
{
    namespace Audio
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Listener is a global interface for defining the audio
        /// listener properties ; the audio listener is the point in
        /// the scene from where all the sounds are heard
        /// </summary>
        ////////////////////////////////////////////////////////////
        public class Listener
        {
            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Global volume of all sounds, in range [0 .. 100] (default is 100)
            /// </summary>
            ////////////////////////////////////////////////////////////
            public static float GlobalVolume
            {
                get {return sfListener_getGlobalVolume();}
                set {sfListener_setGlobalVolume(value);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// 3D position of the listener (default is (0, 0, 0))
            /// </summary>
            ////////////////////////////////////////////////////////////
            public static Vector3 Position
            {
                get {return sfListener_getPosition();}
                set {sfListener_setPosition(value);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// 3D direction of the listener (default is (0, 0, -1))
            /// </summary>
            ////////////////////////////////////////////////////////////
            public static Vector3 Direction
            {
                get {return sfListener_getDirection();}
                set {sfListener_setDirection(value);}
            }

            #region Imports
            [DllImport("recon-audio-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfListener_setGlobalVolume(float Volume);

            [DllImport("recon-audio-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern float sfListener_getGlobalVolume();

            [DllImport("recon-audio-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfListener_setPosition(Vector3 position);

            [DllImport("recon-audio-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Vector3 sfListener_getPosition();

            [DllImport("recon-audio-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfListener_setDirection(Vector3 direction);

            [DllImport("recon-audio-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Vector3 sfListener_getDirection();
            #endregion
        }
    }
}
