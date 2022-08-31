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
        public static bool overlap(FloatRect a, Sprite b) {
            return a.Intersects(b.GetGlobalBounds());
        }
        public static bool overlap(FloatRect a, Shape b) {
            return a.Intersects(b.GetGlobalBounds());
        }
        /*public static bool overlaps(GameObject ObjectA, GameObject ObjectB) {
            Vector2f _point = ObjectA.CSprite.Position;
            float tx = _point.X;
            float ty = _point.Y;
            _point = ObjectB.CSprite.Position;
            if ((_point.X <= tx - ObjectB.CSprite.Texture.Size.X) || (_point.X >= tx + ObjectA.CSprite.Texture.Size.X) || (_point.Y <= ty - ObjectB.CSprite.Texture.Size.Y) || (_point.Y >= ty + ObjectA.CSprite.Texture.Size.Y))
                return false;

            return true;
        }*/
    }
}
