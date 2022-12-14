using System;
using System.Security;
using System.Runtime.InteropServices;
using Recon.Window;
using Recon.Lime;

namespace Recon
{
    namespace Graphics
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// This class defines a graphical 2D text, that can be drawn on screen
        /// </summary>
        ////////////////////////////////////////////////////////////
        public class Text : Transformable, IObjectBase
        {
            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Enumerate the string drawing styles
            /// </summary>
            ////////////////////////////////////////////////////////////
            [Flags]
            public enum Styles
            {
                /// <summary>Regular characters, no style</summary>
                Regular = 0,

                /// <summary> Characters are bold</summary>
                Bold = 1 << 0,

                /// <summary>Characters are in italic</summary>
                Italic = 1 << 1,

                /// <summary>Characters are underlined</summary>
                Underlined = 1 << 2
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Default constructor
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Text() :
                this("", null)
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the text from a string and a font
            /// </summary>
            /// <param name="str">String to display</param>
            /// <param name="font">Font to use</param>
            ////////////////////////////////////////////////////////////
            public Text(string str, Font font) :
                this(str, font, 30)
            {
            }

            public bool Intersection(GameObject obj) {
                return GetGlobalBounds().Intersects(obj.CSprite.GetGlobalBounds());
            }
            public bool Intersection(Text obj) {
                return GetGlobalBounds().Intersects(obj.GetGlobalBounds());
            }
            public bool Intersection(Shape obj) {
                return GetGlobalBounds().Intersects(obj.GetGlobalBounds());
            }
            public bool Intersection(Texture2D obj) {
                return GetGlobalBounds().Intersects(obj.GetGlobalBounds());
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the text from a string, font and size
            /// </summary>
            /// <param name="str">String to display</param>
            /// <param name="font">Font to use</param>
            /// <param name="characterSize">Base characters size</param>
            ////////////////////////////////////////////////////////////
            public Text(string str, Font font, uint characterSize) :
                base(sfText_create())
            {
                DisplayedString = str;
                Font = font;
                CharacterSize = characterSize;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the text from another text
            /// </summary>
            /// <param name="copy">Text to copy</param>
            ////////////////////////////////////////////////////////////
            public Text(Text copy) :
                base(sfText_copy(copy.CPointer))
            {
                Origin = copy.Origin;
                Position = copy.Position;
                Rotation = copy.Rotation;
                Scale = copy.Scale;

                Font = copy.Font;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Global color of the object
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Color Color
            {
                get { return sfText_getColor(CPointer); }
                set { sfText_setColor(CPointer, value); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// String which is displayed
            /// </summary>
            ////////////////////////////////////////////////////////////
            public string DisplayedString
            {
                get
                {
                    // Get a pointer to the source string (UTF-32)
                    IntPtr source = sfText_getUnicodeString(CPointer);

                    // Find its length (find the terminating 0)
                    uint length = 0;
                    unsafe
                    {
                        for (uint* ptr = (uint*)source.ToPointer(); *ptr != 0; ++ptr)
                            length++;
                    }

                    // Copy it to a byte array
                    byte[] sourceBytes = new byte[length * 4];
                    Marshal.Copy(source, sourceBytes, 0, sourceBytes.Length);

                    // Convert it to a C# string
                    return System.Text.Encoding.UTF32.GetString(sourceBytes);
                }

                set
                {
                    // Copy the string to a null-terminated UTF-32 byte array
                    byte[] utf32 = System.Text.Encoding.UTF32.GetBytes(value + '\0');

                    // Pass it to the C API
                    unsafe
                    {
                        fixed (byte* ptr = utf32)
                        {
                            sfText_setUnicodeString(CPointer, (IntPtr)ptr);
                        }
                    }
                }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Font used to display the text
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Font Font
            {
                get {return myFont;}
                set {myFont = value; sfText_setFont(CPointer, value != null ? value.CPointer : IntPtr.Zero);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Base size of characters
            /// </summary>
            ////////////////////////////////////////////////////////////
            public uint CharacterSize
            {
                get {return sfText_getCharacterSize(CPointer);}
                set {sfText_setCharacterSize(CPointer, value);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Style of the text (see Styles enum)
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Styles Style
            {
                get {return sfText_getStyle(CPointer);}
                set {sfText_setStyle(CPointer, value);}
            }

            public Texture2D CSprite { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public Vector2UI ObjectSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public Texture2D texture { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Return the visual position of the Index-th character of the text,
            /// in coordinates relative to the text
            /// (note : translation, origin, rotation and scale are not applied)
            /// </summary>
            /// <param name="index">Index of the character</param>
            /// <returns>Position of the Index-th character (end of text if Index is out of range)</returns>
            ////////////////////////////////////////////////////////////
            public Vector2 FindCharacterPos(uint index)
            {
                return sfText_findCharacterPos(CPointer, index);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Get the local bounding rectangle of the entity.
            ///
            /// The returned rectangle is in local coordinates, which means
            /// that it ignores the transformations (translation, rotation,
            /// scale, ...) that are applied to the entity.
            /// In other words, this function returns the bounds of the
            /// entity in the entity's coordinate system.
            /// </summary>
            /// <returns>Local bounding rectangle of the entity</returns>
            ////////////////////////////////////////////////////////////
            public FloatRect GetLocalBounds()
            {
                return sfText_getLocalBounds(CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Get the global bounding rectangle of the entity.
            ///
            /// The returned rectangle is in global coordinates, which means
            /// that it takes in account the transformations (translation,
            /// rotation, scale, ...) that are applied to the entity.
            /// In other words, this function returns the bounds of the
            /// sprite in the global 2D world's coordinate system.
            /// </summary>
            /// <returns>Global bounding rectangle of the entity</returns>
            ////////////////////////////////////////////////////////////
            public FloatRect GetGlobalBounds()
            {
                // we don't use the native getGlobalBounds function,
                // because we override the object's transform
                return Transform.TransformRect(GetLocalBounds());
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Provide a string describing the object
            /// </summary>
            /// <returns>String description of the object</returns>
            ////////////////////////////////////////////////////////////
            public override string ToString()
            {
                return "[Text]" +
                       " Color(" + Color + ")" +
                       " String(" + DisplayedString + ")" +
                       " Font(" + Font + ")" +
                       " CharacterSize(" + CharacterSize + ")" +
                       " Style(" + Style + ")";
            }

            ////////////////////////////////////////////////////////////
            /// <summmary>
            /// Draw the object to a render target
            ///
            /// This is a pure virtual function that has to be implemented
            /// by the derived class to define how the drawable should be
            /// drawn.
            /// </summmary>
            /// <param name="target">Render target to draw to</param>
            /// <param name="states">Current render states</param>
            ////////////////////////////////////////////////////////////
            public void Render(RenderTarget target, RenderStates states)
            {
                states.Transform *= Transform;
                RenderStates.MarshalData marshaledStates = states.Marshal();

                if (target is EngineWindow)
                {
                    sfRenderWindow_drawText(((EngineWindow)target).CPointer, CPointer, ref marshaledStates);
                }
                else if (target is RenderTexture)
                {
                    sfRenderTexture_drawText(((RenderTexture)target).CPointer, CPointer, ref marshaledStates);
                }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Handle the destruction of the object
            /// </summary>
            /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
            ////////////////////////////////////////////////////////////
            protected override void Destroy(bool disposing)
            {
                sfText_destroy(CPointer);
            }

            private Font myFont = null;

            #region Imports

            [DllImport("recon-graphics-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfText_create();

            [DllImport("recon-graphics-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfText_copy(IntPtr Text);

            [DllImport("recon-graphics-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfText_destroy(IntPtr CPointer);

            [DllImport("recon-graphics-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfText_setColor(IntPtr CPointer, Color Color);

            [DllImport("recon-graphics-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Color sfText_getColor(IntPtr CPointer);

            [DllImport("recon-graphics-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_drawText(IntPtr CPointer, IntPtr Text, ref RenderStates.MarshalData states);

            [DllImport("recon-graphics-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderTexture_drawText(IntPtr CPointer, IntPtr Text, ref RenderStates.MarshalData states);

            [DllImport("recon-graphics-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfText_setUnicodeString(IntPtr CPointer, IntPtr Text);

            [DllImport("recon-graphics-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfText_setFont(IntPtr CPointer, IntPtr Font);

            [DllImport("recon-graphics-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfText_setCharacterSize(IntPtr CPointer, uint Size);

            [DllImport("recon-graphics-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfText_setStyle(IntPtr CPointer, Styles Style);

            [DllImport("recon-graphics-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfText_getString(IntPtr CPointer);

            [DllImport("recon-graphics-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfText_getUnicodeString(IntPtr CPointer);

            [DllImport("recon-graphics-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern uint sfText_getCharacterSize(IntPtr CPointer);

            [DllImport("recon-graphics-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Styles sfText_getStyle(IntPtr CPointer);

            [DllImport("recon-graphics-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern FloatRect sfText_getRect(IntPtr CPointer);

            [DllImport("recon-graphics-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Vector2 sfText_findCharacterPos(IntPtr CPointer, uint Index);

            [DllImport("recon-graphics-con.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern FloatRect sfText_getLocalBounds(IntPtr CPointer);

            public Image GetImage()
            {
                return this.Font.GetTexture(CharacterSize).CopyToImage();
            }

            public Vector2 GetPosition()
            {
                return Position;
            }

            public Texture2D GetSprite()
            {
                return new Texture2D(Font.GetTexture(CharacterSize));
            }

            public Texture GetTexture()
            {
                return Font.GetTexture(this.CharacterSize);
            }

            public void Initialize()
            {
            }

            public bool isCollide(GameObject Object)
            {
                return false;
            }

            public bool isOverlaping(GameObject OBJECT)
            {
                return false;
            }

            public void Kill()
            {
            }

            public void LoadSprite(string imgFile)
            {
            }

            public void Revive()
            {
            }

            public void SetPosition(Vector2 position)
            {
                Position = position;
            }

            public void SetScale(Vector2 scale)
            {
                Scale = scale;
            }

            public void Translate(float x, float y)
            {
                Position += new Vector2(x, y);
            }

            public void Translate(Vector2 position)
            {
                Position += position;
            }

            public void Unload()
            {
                Dispose();
            }

            public void Update()
            {
            }

            Texture2D IObjectBase.GetSprite()
            {
                throw new NotImplementedException();
            }

            #endregion
        }
    }
}
