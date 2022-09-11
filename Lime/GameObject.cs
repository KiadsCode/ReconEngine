using System;
using System.Collections.Generic;
using System.Text;
using Recon.Graphics;
using Recon.Window;
using Recon.Physics;

namespace Recon.Lime {
    public class GameObject {
        public PhysicBody physicBody;
        public bool Solid = true;
        private bool prevSolid = true;
        private Sprite sprite = null;
        private Image img = null;
        private Texture texture = null;
        public bool alive = true;

        #region GetFunctions

        public Vector2 GetPosition() {
            return sprite.Position;
        }

        public Sprite GetSprite() {
            return sprite;
        }
        public Texture GetTexture() {
            return texture;
        }
        public Image GetImage() {
            return img;
        }

        #endregion
        #region SetFunctions

        public void SetPosition(Vector2 position) {
            physicBody.transform = position;
        }
        public void SetScale(Vector2 scale) {
            sprite.Scale = scale;
        }

        #endregion

        /// <summary>
        /// Loading new sprite with old settings
        /// </summary>
        /// <param name="imgFile">Image you want load to</param>
        public void LoadSprite(string imgFile) {
            Sprite backupSprite = sprite;
            string actualPath = Settings.contentFolder + Settings.imagesFolder + imgFile;
            img = new Image(actualPath);
            texture = new Texture(actualPath);
            sprite = new Sprite(texture);



            //Loading from backup file
            sprite.Scale = backupSprite.Scale;
            sprite.Position = backupSprite.Position;
            sprite.Origin = backupSprite.Origin;
            sprite.Rotation = backupSprite.Rotation;
        }

        /// <summary>
        /// Special render for engine
        /// </summary>
        /// <param name="engine">Engine RenderWindow</param>
        internal void render(EngineWindow engine) {
            if (alive)
                engine.Draw(sprite);
        }

        public void Translate(Vector2 position)
        {
            physicBody.transform += position;
        }

        public void Translate(float x, float y)
        {
            Vector2 position = new Vector2(x, y);
            physicBody.transform += position;
        }

        public Sprite CSprite {
            get {
                return sprite;
            }
            set {
                CSprite = value;
            }
        }

        public void screenCenter() {
            SetPosition(new Vector2(RcG.engine.Size.X / 2, RcG.engine.Size.Y / 2));
        }

        public void Revive() {
            alive = true;
            Solid = prevSolid;
        }

        public void Kill() {
            prevSolid = Solid;
            Solid = false;
            alive = false;
        }
        public virtual void Initialize() { }
        public virtual void Update() {
            physicBody.UpdatePhysic();
            sprite.Position = physicBody.transform;
        }

        public GameObject() {
            sprite = new Sprite();
            physicBody = new PhysicBody(sprite);
        }
        public GameObject(String imgFile, Vector2 position)
        {
            string actualPath = Settings.contentFolder + Settings.imagesFolder + imgFile;
            img = new Image(actualPath);
            texture = new Texture(img);
            sprite = new Sprite(texture);

            //Check is position empty or null
            if (position.Equals(new Vector2()) || position.Equals(null))
                position = new Vector2(0, 0);

            sprite.Position = position;
            physicBody = new PhysicBody(sprite);
        }

        /// <summary>
        /// Unloading content of object
        /// </summary>
        public void Unload() {
            sprite.Dispose();
            img.Dispose();
            texture.Dispose();
        }

        /// <summary>
        /// Checking is this object overlaping other
        /// </summary>
        /// <param name="OBJECT">object you want to check collision</param>
        /// <returns>collision of objects</returns>
        public bool isOverlaping(GameObject OBJECT) {
            // Compute the intersection boundaries
            FloatRect rect = OBJECT.GetSprite().GetGlobalBounds();
            FloatRect myRect = sprite.GetGlobalBounds();
            float left = System.Math.Max(myRect.Left, rect.Left);
            float top = System.Math.Max(myRect.Top, rect.Top);
            float right = System.Math.Min(myRect.Left + myRect.Width, rect.Left + rect.Width);
            float bottom = System.Math.Min(myRect.Top + myRect.Height, rect.Top + rect.Height);

            if (Solid && OBJECT.Solid)
                return (left < right) && (top < bottom);
            else
                return false;
        }

        /// <summary>
        /// Size of object sprite in pixels
        /// </summary>
        public Vector2UI ObjectSize {
            get {
                return sprite.Texture.Size;
            }
            set {
                ObjectSize = value;
            }
        }

        /// <summary>
        /// Checking is object colliding or not
        /// </summary>
        /// <param name="Object">object wou want check collision</param>
        /// <returns>result of collision</returns>
        public bool isCollide(GameObject Object) {
            Vector2 _point = getScreenXY();
            float tx = _point.X;
            float ty = _point.Y;
            _point = Object.getScreenXY();
            if ((_point.X <= tx - Object.ObjectSize.X) || (_point.X >= tx + ObjectSize.X) || (_point.Y <= ty - Object.ObjectSize.Y) || (_point.Y >= ty + ObjectSize.Y))
                return false;

            return true;
        }

        private Vector2 getScreenXY() {
            return sprite.Position;
        }
    }
}