using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using System.Xml;

namespace SMBDeluxe
{
    class AnimManager
    {
        private Dictionary<string, List<Rectangle>> animations;
        private Dictionary<string, short> frameCounters;
        private Dictionary<string, int> delays;
        private Dictionary<string, float> lastTime; // Last time we changed frame

        public AnimManager()
        {
            animations = new Dictionary<string, List<Rectangle>>();
            frameCounters = new Dictionary<string, short>();
            lastTime = new Dictionary<string, float>();
            delays = new Dictionary<string, int>();
        }

        public void LoadFromFile(string filename)
        {
            //using (XmlReader reader = XmlReader.Create(filename))
            //{
            //    //    reader.MoveToContent();
            //    //    reader.ReadStartElement();
            //    //    while(reader.NodeType != XmlNodeType.EndElement)
            //    //    {
            //    //        using (var subtree = reader.ReadSubtree())
            //    //        {
            //    //            string animName = "";
            //    //            List<Rectangle> clips = null;
            //    //            switch (subtree.Name)
            //    //            {
            //    //                case "anim":
            //    //                    // Add previous animation before we move onto the next one
            //    //                    if (clips != null)
            //    //                        animations.Add(animName, clips);

            //    //                    clips = new List<Rectangle>();
            //    //                    animName = reader.GetAttribute("name");
            //    //                    delays.Add(animName, System.Convert.ToInt32(reader.GetAttribute("delay")));
            //    //                    break;
            //    //                case "frame":
            //    //                    clips.Add(new Rectangle()
            //    //                    {
            //    //                        X = System.Convert.ToInt32(reader.GetAttribute("x")),
            //    //                        Y = System.Convert.ToInt32(reader.GetAttribute("y")),
            //    //                        Width = System.Convert.ToInt32(reader.GetAttribute("w")),
            //    //                        Height = System.Convert.ToInt32(reader.GetAttribute("h"))
            //    //                    });
            //    //                    break;
            //    //            }
            //    //        }
            //    //        reader.Read();
            //    //    }
            //    //    reader.ReadEndElement();
            //    //}
            //}
        }

        public Rectangle Animate(string name, bool stopOnLastFrame = false)
        {
            return new Rectangle();
        }

        public void ResetAnim(string name)
        {
            frameCounters[name] = 0;
        }

        // Will return true if animation map is empty
        public bool IsEmpty()
        {
            return animations.Count == 0;
        }

        // Will return true if specified animation is empty
        public bool  IsAnimEmpty(string name)
        {
            return animations[name].Count == 0;
        }
    }
}
