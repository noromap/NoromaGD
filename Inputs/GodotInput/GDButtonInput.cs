using Godot;

namespace NoromaGD.Inputs
{
    public class GDButtonInput : IButtonInput
    {
        public bool Pressed => Input.IsActionPressed(_actionName);

        public bool Up => Input.IsActionJustReleased(_actionName);

        public bool Down => Input.IsActionJustPressed(_actionName);

        private string _actionName;

        public GDButtonInput(string actionName)
        {
            _actionName = actionName;
        }
    }
}