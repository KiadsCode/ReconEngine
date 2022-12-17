using Recon.Graphics;
using Recon.Window;

namespace Recon.Lime
{
    public interface IObjectBase : Drawable
    {
        Texture2D texture { get; set; }
        Vector2UI ObjectSize { get; set; }

        void Dispose();
        Vector2 GetPosition();
        Texture2D GetSprite();
        void Initialize();
        bool isCollide(GameObject Object);
        bool isOverlaping(GameObject OBJECT);
        void Kill();
        void LoadSprite(string imgFile);
        void Revive();
        void screenCenter();
        void SetPosition(Vector2 position);
        void SetScale(Vector2 scale);
        void Translate(float x, float y);
        void Translate(Vector2 position);
        void Unload();
        void Update();
    }
}