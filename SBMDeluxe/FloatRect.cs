using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SMBDeluxe
{
    class FloatRect
    {
        public float X;
        public float Y;
        public float width;
        public float height;

        public FloatRect(float X, float Y, float width, float height)
        {
            this.X = X;
            this.Y = Y;
            this.width = width;
            this.height = height;
        }

        public FloatRect(Vector2 pos, Vector2 size)
        {
            this.X = pos.X;
            this.Y = pos.Y;
            this.width = size.X;
            this.height = size.Y;
        }

        // Original implementation by LazyFoo (lazyfoo.net)
        public bool Intersects(FloatRect other)
        {
            // The sides of the rectangles
            float leftA, leftB;
            float rightA, rightB;
            float topA, topB;
            float bottomA, bottomB;

            // Calculate the sides of rect A
            leftA = this.X;
            rightA = this.X + this.width;
            topA = this.Y;
            bottomA = this.Y + this.height;

            // Calculate the sides of rect B
            leftB = other.X;
            rightB = other.X + other.width;
            topB = other.Y;
            bottomB = other.Y + other.height;

            // If any of the sides form A are outside of B
            if (bottomA <= topB)
                return false;
            if (topA >= bottomB)
                return false;
            if (rightA <= leftB)
                return false;
            if (leftA >= rightB)
                return false;

            // If none of the sides from A are outside B
            return true;
        }

        public Vector2 GetPos()
        {
            return new Vector2(X, Y);
        }
    }
}
