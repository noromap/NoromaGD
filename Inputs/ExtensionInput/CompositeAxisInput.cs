using Godot;
using Nephemee;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoromaGD.Inputs
{
    public class CompositeAxisInput : IAxisInput
    {
        private IAxisInput[] _inputs;
        public float Axis => _inputs.Sum(x => x.Axis);

        public CompositeAxisInput(IEnumerable<IAxisInput> inputs)
        {
            Init(inputs);
        }

        public CompositeAxisInput(params IAxisInput[] inputs)
        {
            Init(inputs);
        }

        private void Init(IEnumerable<IAxisInput> inputs)
        {
            _inputs = inputs.ToArray();
            if (inputs == null) GD.PrintErr("CompositeAxisInput: inputs is null");
            if (inputs.Count() == 0) GD.PrintErr("CompositeAxisInput: inputs = 0　で生成しようとしています");
        }
    }
}