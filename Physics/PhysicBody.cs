using System;
using System.Collections.Generic;
using System.Text;
using Recon.Window;
using Recon.Graphics;
using Recon.Lime;
using System.Security.Policy;

namespace Recon.Physics {
    /// <summary>
    /// Physic Body of objects
    /// </summary>
    public class PhysicBody {
        public Vector2 transform = Vector2.Zero;
        public Vector2 impulsePower = Vector2.Zero;
        public Vector2 velocity = Vector2.Zero;
        public Vector2 gravity = Vector2.Zero;

        public PhysicBody(Transformable transform) {
            this.transform = transform.Position;
        }

        internal void UpdatePhysic() {
            if (impulsePower.X != 50 || impulsePower.X != -50)
                impulsePower.X += gravity.X;
            if (impulsePower.Y != 50 || impulsePower.Y != -50)
                impulsePower.Y += gravity.Y;

            transform.X += velocity.X / 100;
            transform.Y += velocity.Y / 100;

            ImpulseUpdate();
        }

        private void ImpulseUpdate() {
            if (impulsePower.X != 0)
                transform = new Vector2(transform.X + impulsePower.X / 100, transform.Y);
            if (impulsePower.Y != 0)
                transform = new Vector2(transform.X, transform.Y + impulsePower.Y / 100);
        }

        /// <summary>
        /// Making impulse for object
        /// </summary>
        /// <param name="power">power of impulse</param>
        public void Impulse(Vector2 power) {
            this.impulsePower += power * 100f;
        }
    }
}