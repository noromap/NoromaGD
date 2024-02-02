using Godot;

namespace NoromaGD.Inputs
{
    public class EmptyInput : IAxisInput, IButtonInput, IDirectionInput
    {
        public static readonly EmptyInput Instance = new EmptyInput();
        public float Axis { get; } = 0;

        public bool Pressed { get; } = false;

        public bool Up { get; } = false;

        public bool Down { get; } = false;

        public Vector2 Direction => Vector2.Zero;
    }
}