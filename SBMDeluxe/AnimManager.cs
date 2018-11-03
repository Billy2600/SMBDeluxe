using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using System.Xml;

namespace SMBDeluxe
{
    public class Animation
    {
        public List<Rectangle> frames;
        public short frameCounter;
        public double delay; // Delay between frames
        public double lastTime; // Last time we changed frame
    }

    public class AnimManager
    {
        private Dictionary<string, Animation> animations;

        public GameTime gameTime;

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
                            delay = double.Parse(reader.GetAttribute("delay")),
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
            if(gameTime.TotalGameTime.TotalMilliseconds > animations[name].lastTime + animations[name].delay)
            {
                animations[name].frameCounter++;
                if(animations[name].frameCounter >= animations[name].frames.Count)
                {
                    if (stopOnLastFrame)
                        return animations[name].frames[animations[name].frames.Count - 1];
                    else
                        ResetAnim(name);
                }

                animations[name].lastTime = gameTime.TotalGameTime.TotalMilliseconds;
            }

            if (!IsAnimEmpty(name))
                return animations[name].frames[animations[name].frameCounter];
            else
                return new Rectangle() { X = 0, Y = 0, Width = 0, Height = 0 };
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
