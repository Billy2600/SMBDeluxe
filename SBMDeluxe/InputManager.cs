using Microsoft.Xna.Framework.Input;

namespace SMBDeluxe
{
    public struct Inputs
    {
        public bool Up;
        public bool Down;
        public bool Left;
        public bool Right;
        public bool Run;
        public bool Jump;
    }

    public class InputManager
    {
        public Inputs ReadInputs()
        {
            Inputs inputs = new Inputs();

            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
                inputs.Up = true;
            if (kstate.IsKeyDown(Keys.Down))
                inputs.Down = true;
            if (kstate.IsKeyDown(Keys.Left))
                inputs.Left = true;
            if (kstate.IsKeyDown(Keys.Right))
                inputs.Right = true;
            if (kstate.IsKeyDown(Keys.Z))
                inputs.Run = true;
            if (kstate.IsKeyDown(Keys.X))
                inputs.Jump = true;

            return inputs;
        }
    }
}
