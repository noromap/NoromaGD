using Godot;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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