using Godot;

namespace NoromaGD.Test
{
    internal partial class TestUniNode2D : UniNode2D
    {
        [Export] public bool LogUpdate = true;
        [Export] public float DisableTime = 2;

        private float _currentTime;

        public override void _Awake()
        {
            Log("Awake");
        }

        public override void _OnEnable()
        {
            _currentTime = DisableTime;
            Log("OnEnable");
        }

        public override void _OnDisable()
        {
            Log("OnDisable");
        }

        public override void _Start()
        {
            Log("Start");
        }

        public override void _OnDestroy()
        {
            Log("OnDestroy");
        }

        public override void _Update(double delta)
        {
            Log("Update");
            _currentTime -= (float)delta;
            if (_currentTime < 0)
            {
                SetActive(false);
            }
        }

        private void Log(string message)
        {
            GD.Print($"{nameof(TestUniNode2D)} : {message}");
        }
    }
}