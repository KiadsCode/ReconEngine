using System;
using System.Collections.Generic;
using System.Text;
using Recon.Lime;
using Recon.Graphics;
using Recon.Window;
using Recon.Util;
using Recon.Generic;

namespace Recon.Lime
{
    public class GameState
    {
        public ContentManager Content 
        {
            get
            {
                return contentManager;
            }
        }
        private ContentManager contentManager;
        public string mapFormat = ".map";
        public Camera camera = new Camera();
        private double xtimer = 0;
        public double XTimer
        {
            get
            {
                return xtimer;
            }
        }
        private GameObject @object = new GameObject();
        internal List<IObjectBase> objs = new List<IObjectBase>();

        public virtual void Render(EngineWindow rendr)
        {
            foreach (IObjectBase obj in objs)
            {
                obj.Render(rendr, RenderStates.Default);
            }
        }
        public Color backGroundColor = Color.Black;
        internal void MessageLoop()
        {
            while (RcG.engine.IsOpen())
            {
                RcG.engine.DispatchEvents();
                RcG.engine.Clear(backGroundColor);
                camera.UpdateCamera();
                Update();
                Render(RcG.engine);
                RcG.engine.Display();
            }
        }
        public virtual void UnloadContent()
        {
            cleanObjects();
        }
        public virtual void Initialize()
        {
            LoadContent();
            //camera.autoSizeDetection = true;

            add(@object);

            foreach (IObjectBase obj in objs)
            {
                obj.Initialize();
            }
        }
        public virtual void LoadContent()
        {
            MessageLoop();
        }
        public void add(IObjectBase obj)
        {
            objs.Add(obj);
        }
        public void cleanObjects()
        {
            foreach (IObjectBase obj in objs)
            {
                obj.Unload();
            }
            objs.Clear();
        }
        public virtual void Update()
        {
            xtimer++;
            @object.SetPosition(new Vector2());
            this.camera.UpdateCamera();
            RcG.engine.currentState = this;
            RcG.engine.camera = this.camera;
            //RcG.engine.SetView(camera.GetView());
            foreach(IObjectBase @base in objs)
            {
                @base.Update();
            }
        }

        public void LoadMap(string mapName)
        {
            int x = 0, y = 0;
            string objectName = "oHGPole";
            DataReader dr = new DataReader(mapName + mapFormat);
            mega<string> mapData = dr.GetText();

            foreach (string variable in mapData.ToArray())
            {
                const string reqKeyWord = "obj";
                string valid = "";
                for (int i = 0; i < variable.Length; i++)
                {
                    if (valid != reqKeyWord)
                        valid += variable[i];
                    else
                        break;
                }
                if (valid != reqKeyWord)
                    return;

                objectName = "";
                int arg = 0;
                string[] intDtaa = new string[2];

                //Parsing object data
                for (int i = valid.Length + 1; i < variable.Length; i++)
                {
                    if (variable[i] == ',')
                        arg++;
                    if (variable[i] != '(' && variable[i] != ')' && variable[i] != '\"')
                    {
                        if (variable[i] != ' ' && variable[i] != ',' && arg == 0)
                            objectName += variable[i];
                        if (variable[i] != ' ' && variable[i] != ',' && arg == 1)
                            intDtaa[0] += variable[i];
                        if (variable[i] != ' ' && variable[i] != ',' && arg == 2)
                            intDtaa[1] += variable[i];
                    }
                }

                x = Convert.ToInt32(intDtaa[0]);
                y = Convert.ToInt32(intDtaa[1]);

                OnObjectNode(new Vector2(x, y), objectName);
            }
        }

        public virtual void OnObjectNode(Vector2 pos, string name)
        {

        }

        public GameState()
        {
            camera = new Camera();
            contentManager = new ContentManager("Content");

            Initialize();
        }

        public void SetCamera(Camera camera)
        {
            this.camera = camera;
        }

    }
}