using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using System.Xml;

namespace SMBDeluxe
{
    class Animation
    {
        public List<Rectangle> frames;
        public short frameCounter;
        public float delay; // Delay between frames
        public float lastTime; // Last time we changed frame
    }

    class AnimManager
    {
        private Dictionary<string, Animation> animations;

        public AnimManager()
        {
            animations = new Dictionary<string, Animation>();
        }

        public void LoadFromFile(string filename)
        {
            using (var reader = XmlReader.Create(filename))
            {
                Animation animation = null;

                reader.MoveToContent();
                reader.ReadStartElement();
                while(reader.NodeType != XmlNodeType.EndElement)
                {
                    if(reader.Name == "anim")
                    {
                        animation = new Animation()
                        {
                            delay = float.Parse(reader.GetAttribute("delay")),
                            frameCounter = 0,
                            lastTime = 0,
                            frames = new List<Rectangle>()
                        };
                        animations.Add(reader.GetAttribute("name"), animation);
                    }

                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        using (var subtree = reader.ReadSubtree())
                        {
                            while (subtree.Read())
                            {
                                if(subtree.Name == "frame" && subtree.NodeType != XmlNodeType.EndElement && animation != null)
                                {
                                    animation.frames.Add(new Rectangle()
                                    {
                                        X = int.Parse(subtree.GetAttribute("x")),
                                        Y = int.Parse(subtree.GetAttribute("y")),
                                        Width = int.Parse(subtree.GetAttribute("w")),
                                        Height = int.Parse(subtree.GetAttribute("h"))
                                    });
                                }
                            }
                        }
                    }
                    reader.Read();
                }
                reader.ReadEndElement();
            }
        }

        public Rectangle Animate(string name, bool stopOnLastFrame = false)
        {
            return new Rectangle();
        }

        public void ResetAnim(string name)
        {
            animations[name].frameCounter = 0;
        }

        // Will return true if animation map is empty
        public bool IsEmpty()
        {
            return animations.Count == 0;
        }

        // Will return true if specified animation is empty
        public bool  IsAnimEmpty(string name)
        {
            return animations[name].frames.Count == 0;
        }
    }
}
