using Godot;

namespace NoromaGD.Inputs
{
    public class GDDirectionInput : IDirectionInput
    {
        public string LeftAction { get; set; }

        public string RightAction { get; set; }

        public string DownAction { get; set; }

        public string UpAction { get; set; }

        public Vector2 Direction => Input.GetVector(LeftAction, RightAction, DownAction, UpAction);

        public GDDirectionInput(string leftAction, string rightAction, string downAction, string upAction)
        {
            LeftAction = leftAction;
            RightAction = rightAction;
            DownAction = downAction;
            UpAction = upAction;
        }
    }
}