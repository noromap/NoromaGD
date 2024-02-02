namespace NoromaGD.Inputs
{
    public interface IButtonInput
    {
        bool Pressed { get; }
        bool Up { get; }
        bool Down { get; }
    }

    public static class ButtonInputExt
    {
        public static bool CheckButtonInput(this IButtonInput input, ButtonInputType type)
        {
            switch (type)
            {
                case ButtonInputType.Pressed: return input.Pressed;
                case ButtonInputType.Down: return input.Down;
                case ButtonInputType.Up: return input.Up;
                default: return false;
            }
        }

        public static bool wasPressed(this IButtonInput input)
        {
            return (input.Pressed && !input.Down) || input.Up;
        }
    }
}