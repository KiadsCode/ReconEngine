using System;
using System.Collections.Generic;
using System.Text;
using Recon.Graphics;
using Recon.Window;
using Recon.Physics;

namespace Recon.Lime {
    public class GameObject : IDisposable, IObjectBase
    {
        public PhysicBody physicBody;
        public bool Solid = true;
        private bool prevSolid = true;
        private Texture2D sprite = null;
        public bool alive = true;

        #region GetFunctions

        public Vector2 GetPosition() {
            return sprite.Position;
        }

        public Texture2D GetSprite() {
            return sprite;
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
        public void LoadSprite(ContentManager Content, string imgFile) {
            Texture2D backupSprite = sprite;
            sprite = Content.Load<Texture2D>(imgFile);



            //Loading from backup file
            sprite.Scale = backupSprite.Scale;
            sprite.Position = backupSprite.Position;
            sprite.Origin = backupSprite.Origin;
            sprite.Rotation = backupSprite.Rotation;
        }

        public void ChangeTexture(Texture2D texture)
        {
            this.sprite = texture;
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

        public Texture2D CSprite {
            get {
                return sprite;
            }
            set {
                sprite = value;
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
        public void Initialize() { }
        public void Update() {
            physicBody.UpdatePhysic();
            sprite.Position = physicBody.transform;
        }

        public GameObject() {
            sprite = new Texture2D();
            physicBody = new PhysicBody(sprite);
        }
        public GameObject(GameState gameState, String imgFile, Vector2 position)
        {
            sprite = gameState.Content.Load<Texture2D>(imgFile);

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

        Texture2D IObjectBase.texture { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        public void Dispose()
        {
            CSprite.Dispose();
        }

        public void Render(RenderTarget target, RenderStates states)
        {
            if (alive)
                RcG.engine.Draw(sprite);
        }

        Texture2D IObjectBase.GetSprite()
        {
            throw new NotImplementedException();
        }

        public void LoadSprite(string imgFile)
        {
            throw new NotImplementedException();
        }
    }
}