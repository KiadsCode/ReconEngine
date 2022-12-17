using Recon.Audio;
using Recon.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.WebUI;

namespace Recon
{
    public class ContentManager
    {
        public ContentManager(string directory)
        {
            rootDirectory = directory;
        }

        public string RootDirectory
        {
            get
            {
                return rootDirectory;
            }
            set
            {
                rootDirectory = value;
            }
        }
        internal string rootDirectory;

        public void SetDirectory(string directory)
        {
            rootDirectory = directory;
        }

        /// <summary>
        /// Loading asset from memory
        /// </summary>
        /// <typeparam name="T">Asset type</typeparam>
        /// <param name="assetName">Name of asset to load</param>
        /// <returns>Asset</returns>
        /// <exception cref="Exception"></exception>
        public T Load<T>(string assetName)
        {
            string AssetPath = rootDirectory + "/" + assetName;
            T file = default(T);
            object obj = new object();

            if (typeof(T) == typeof(Texture2D))
            {
                Image image;
                Texture texture;

                if (!File.Exists(AssetPath))
                {
                    for (int i = 0; i < imageTypes.Length; i++)
                    {
                        if (File.Exists(AssetPath + imageTypes[i]))
                        {
                            AssetPath = AssetPath + imageTypes[i];
                            break;
                        }
                    }
                }

                try
                {
                    image = new Image(AssetPath);
                    texture = new Texture(image);
                }
                catch
                {
                    throw new Exception("file corrupted error");
                }
                obj = new Texture2D(texture);
            }
            if (typeof(T) == typeof(Image))
            {
                Image image;

                if (!File.Exists(AssetPath))
                {
                    for (int i = 0; i < imageTypes.Length; i++)
                    {
                        if (File.Exists(AssetPath + imageTypes[i]))
                        {
                            AssetPath = AssetPath + imageTypes[i];
                            break;
                        }
                    }
                }

                try
                {
                    image = new Image(AssetPath);
                }
                catch
                {
                    throw new Exception("file corrupted error");
                }
                obj = image;
            }
            if (typeof(T) == typeof(Texture))
            {
                Image image;
                Texture texture;

                if (!File.Exists(AssetPath))
                {
                    for (int i = 0; i < imageTypes.Length; i++)
                    {
                        if (File.Exists(AssetPath + imageTypes[i]))
                        {
                            AssetPath = AssetPath + imageTypes[i];
                            break;
                        }
                    }
                }

                try
                {
                    image = new Image(AssetPath);
                    texture = new Texture(image);
                }
                catch
                {
                    throw new Exception("file corrupted error");
                }
                obj = texture;
            }
            if (typeof(T) == typeof(Sound))
            {
                SoundBuffer buffer;

                if (!File.Exists(AssetPath))
                {
                    for (int i = 0; i < soundTypes.Length; i++)
                    {
                        if (File.Exists(AssetPath + soundTypes[i]))
                        {
                            AssetPath = AssetPath + soundTypes[i];
                            break;
                        }
                    }
                }

                try
                {
                    buffer = new SoundBuffer(AssetPath);
                }
                catch
                {
                    throw new Exception("file corrupted error");
                }
                obj = new Sound(buffer);
            }

            if (!(obj is T))
            {
                throw new Exception("bad file format\ntry change EngineProfile");
            }

            file = (T)obj;

            return file;
        }
        public string[] imageTypes =
        {
            ".png",
            ".bmp"
        };
        public string[] soundTypes =
        {
            ".wav",
            ".ogg"
        };
    }
}
