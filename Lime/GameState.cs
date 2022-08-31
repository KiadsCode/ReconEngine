using System;
using System.Collections.Generic;
using System.Text;
using Recon.Lime;
using Recon.Graphics;
using Recon.Window;

namespace Recon.Lime
{
    public class GameState
    {
        private double xtimer = 0;
        public double XTimer
        {
            get
            {
                return xtimer;
            }
        }
        private GameObject @object = new GameObject();
        internal List<GameObject> objs = new List<GameObject>();
        internal List<Text> textObjs = new List<Text>();
        internal List<Shape> shapeObjs = new List<Shape>();

        public virtual void Render(EngineWindow rendr)
        {
            foreach (GameObject obj in objs)
            {
                obj.render(rendr);
            }
            foreach (Text obj in textObjs)
            {
                obj.Render(rendr, RenderStates.Default);
            }
            foreach (Shape obj in shapeObjs)
            {
                obj.Render(rendr, RenderStates.Default);
            }
        }
        public Color backGroundColor = Color.Black;
        internal void GameLoop()
        {
            while (RcG.engine.IsOpen())
            {
                RcG.engine.DispatchEvents();
                RcG.engine.Clear(backGroundColor);
                Update();
                Render(RcG.engine);
                RcG.engine.Display();
            }
        }
        public virtual void UnloadContent()
        {
            cleanObjects();
        }
        public virtual void Init()
        {
            ContentLoad();

            add(@object);

            foreach (GameObject obj in objs)
            {
                obj.Initialize();
            }
        }
        public virtual void ContentLoad()
        {
            GameLoop();
        }
        public void add(GameObject obj)
        {
            objs.Add(obj);
        }
        public void add(Text obj)
        {
            textObjs.Add(obj);
        }
        public void add(Shape obj)
        {
            shapeObjs.Add(obj);
        }
        public void cleanObjects()
        {
            foreach (GameObject obj in objs)
            {
                obj.Unload();
            }
            objs.Clear();
            textObjs.Clear();
            shapeObjs.Clear();
        }
        public virtual void Update()
        {
            xtimer++;
            @object.SetPosition(new Vector2());
            foreach (GameObject obj in objs)
            {
                obj.Update();
            }
        }
    }
}