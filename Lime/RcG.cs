using System;
using System.Collections.Generic;
using System.Text;
using Recon.Graphics;
using Recon.Lime;
using Recon.Window;

namespace Recon.Lime {
    public static class RcG {
        public static EngineWindow engine;

        public static bool overlap(FloatRect a, GameObject b) {
            return a.Intersects(b.CSprite.GetGlobalBounds());
        }
        public static bool overlap(FloatRect a, Texture2D b) {
            return a.Intersects(b.GetGlobalBounds());
        }
        public static bool overlap(FloatRect a, Shape b) {
            return a.Intersects(b.GetGlobalBounds());
        }
        /*public static bool overlaps(GameObject ObjectA, GameObject ObjectB) {
            Vector2f _point = ObjectA.Texture.Position;
            float tx = _point.X;
            float ty = _point.Y;
            _point = ObjectB.Texture.Position;
            if ((_point.X <= tx - ObjectB.Texture.Texture.Size.X) || (_point.X >= tx + ObjectA.Texture.Texture.Size.X) || (_point.Y <= ty - ObjectB.Texture.Texture.Size.Y) || (_point.Y >= ty + ObjectA.Texture.Texture.Size.Y))
                return false;

            return true;
        }*/
    }
}
