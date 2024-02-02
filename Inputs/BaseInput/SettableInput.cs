using Godot;

namespace NoromaGD.Inputs
{
    public class SettableAxisInput : IAxisInput
    {
        public float Axis { get; set; }

        public void Reset()
        {
            Axis = 0;
        }
    }

    public class SettableButtonInput : IButtonInput
    {
        public bool Pressed { get; set; }

        public bool Up { get; set; }

        public bool Down { get; set; }

        public void Reset()
        {
            Pressed = false;
            Up = false;
            Down = false;
        }
    }

    public class SettableDirectionInput : IDirectionInput
    {
        public Vector2 Direction { get; set; }

        public void Reset()
        {
            Direction = Vector2.Zero;
        }
    }

    public class SettableInput : IAxisInput, IButtonInput, IDirectionInput
    {
        public float Axis { get; set; }

        public bool Pressed { get; set; }

        public bool Up { get; set; }

        public bool Down { get; set; }

        public Vector2 Direction { get; set; }

        public void Reset()
        {
            Axis = 0;
            Direction = Vector2.Zero;
            Pressed = false;
        }
    }
}