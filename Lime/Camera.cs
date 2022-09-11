using Recon.Graphics;
using Recon.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recon.Lime
{
    public class Camera
    {
        private View view;
        private Vector2 position = Vector2.Zero;
        private Vector2 size = Vector2.Zero;
        public bool autoSizeDetection = true;

        public Camera()
        {
            position = new Vector2(RcG.engine.Size.X / 2, RcG.engine.Size.Y / 2);
            size = new Vector2(RcG.engine.Size.X, RcG.engine.Size.Y);
        }

        internal void UpdateCamera()
        {
            if (autoSizeDetection)
                size = new Vector2(RcG.engine.Size.X, RcG.engine.Size.Y);
            view = new View(position, size);
        }

        public View GetView()
        {
            return view;
        }

        public void SetPosition(Vector2 vector)
        {
            position = vector;
        }

        public void SetView(View view)
        {
            position = view.Center;
            size = view.Size;
            this.view = view;
        }

        public void SetSize(Vector2 vector)
        {
            if (autoSizeDetection == false)
                size = vector;
        }
    }
}