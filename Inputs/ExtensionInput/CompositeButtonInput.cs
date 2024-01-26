using Godot;
using Nephemee;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoromaGD.Inputs
{
    public class CompositeButtonInput : IButtonInput
    {
        private IButtonInput[] _inputs;
        private bool and;

        public CompositeButtonInput(IEnumerable<IButtonInput> inputs, bool and = false)
        {
            Init(inputs, and);
        }

        public CompositeButtonInput(bool and, params IButtonInput[] inputs)
        {
            Init(inputs, and);
        }

        private void Init(IEnumerable<IButtonInput> inputs, bool and)
        {
            _inputs = inputs.ToArray();
            if (inputs == null) GD.PrintErr("CompositeButtonInput: inputs is null");
            if (inputs.Count() == 0) GD.PrintErr("CompositeButtonInput: inputs = 0　で生成しようとしています");
            this.and = and;
        }

        public bool Pressed
        {
            get
            {
                if (and) return _inputs.All(i => i.Pressed);
                else return _inputs.Any(i => i.Pressed);
            }
        }

        public bool Up
        {
            get
            {
                if (and) return _inputs.All(i => i.Up);
                else return _inputs.Any(i => i.Up);
            }
        }

        public bool Down
        {
            get
            {
                if (and) return _inputs.All(i => i.Down);
                else return _inputs.Any(i => i.Down);
            }
        }
    }
}