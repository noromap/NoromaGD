using System.Collections.Generic;

namespace NoromaGD.Inputs
{
    public static class InputExtensions
    {
        public static CompositeButtonInput ToCompositeButtonInput(this IEnumerable<IButtonInput> buttonInputs, bool judgeByAnd = false)
        {
            return new CompositeButtonInput(buttonInputs, judgeByAnd);
        }

        public static CompositeDirectionInput ToCompositeDirectionInput(this IEnumerable<IDirectionInput> DirectionInputs)
        {
            return new CompositeDirectionInput(DirectionInputs);
        }

        public static CompositeAxisInput ToCompositeAxisInput(this IEnumerable<IAxisInput> AxisInputs)
        {
            return new CompositeAxisInput(AxisInputs);
        }
    }
}