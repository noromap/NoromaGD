using Godot;

namespace NoromaGD.Inputs
{
    public class GDDirectionInput : IDirectionInput
    {
        public string LeftAction { get; set; }

        public string RightAction { get; set; }

        public string DownAction { get; set; }

        public string UpAction { get; set; }

        public Vector2 Direction
        {
            get
            {
                if (Normalize) return Input.GetVector(LeftAction, RightAction, UpAction, DownAction);
                else return new Vector2(Input.GetAxis(LeftAction, RightAction), Input.GetAxis(UpAction, DownAction));
            }
        }

        public bool Normalize;

        public GDDirectionInput(string leftAction, string rightAction, string downAction, string upAction, bool normalize = true)
        {
            LeftAction = leftAction;
            RightAction = rightAction;
            DownAction = downAction;
            UpAction = upAction;
            Normalize = normalize;
        }
    }
}