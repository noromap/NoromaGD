using Godot;

namespace NoromaGD.Inputs
{
    public class GDAxisInput : IAxisInput
    {
        public float Axis => Input.GetAxis(NegativeAction, PositiveAction);

        public string NegativeAction { get; set; }

        public string PositiveAction { get; set; }

        public GDAxisInput(string negativeAction, string positiveAction)
        {
            NegativeAction = negativeAction;
            PositiveAction = positiveAction;
            PositiveAction = positiveAction;
        }
    }
}