using Godot;
using System.Collections.Generic;
using System.Linq;

namespace NoromaGD.Inputs
{
    public class CompositeDirectionInput : IDirectionInput
    {
        private IDirectionInput[] _inputs;
        public Vector2 Direction => new Vector2(_inputs.Sum(i => i.Direction.X), _inputs.Sum(i => i.Direction.Y));

        public CompositeDirectionInput(IEnumerable<IDirectionInput> inputs)
        {
            Init(inputs);
        }

        public CompositeDirectionInput(params IDirectionInput[] inputs)
        {
            Init(inputs);
        }

        private void Init(IEnumerable<IDirectionInput> inputs)
        {
            _inputs = inputs.ToArray();
            if (inputs == null) GD.PrintErr("CompositeDirectionInput: inputs is null");
            if (inputs.Count() == 0) GD.PrintErr("CompositeDirectionInput: inputs = 0　で生成しようとしています");
        }
    }
}