using Godot;

namespace NoromaGD
{
    public partial class UniNode2D : Node2D
    {
        public bool Enabled
        {
            get => _enabled;
            set => SetActive(value);
        }

        private bool _enterTree;
        private bool _ready;
        private bool _enabled;
        private bool _awaked;
        private bool _started;
        private bool _exitTree;

        public virtual void _Awake()
        { }

        public virtual void _Start()
        { }

        public virtual void _Update(double delta)
        { }

        public virtual void _FixedUpdate(double delta)
        { }

        public virtual void _OnEnable()
        { }

        public virtual void _OnDisable()
        { }

        public virtual void _OnDestroy()
        { }

        public void SetActive(bool active)
        {
            if (_enabled == active) return;
            _enabled = active;
            if (active)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public override void _EnterTree()
        {
            _enterTree = true;
            VisibilityChanged += OnVisibilityChanged;
            if (IsVisibleInTree())
            {
                _enabled = true;
                _awaked = true;
                _Awake();
                _OnEnable();
            }
        }

        public override void _ExitTree()
        {
            _exitTree = true;
            VisibilityChanged -= OnVisibilityChanged;
            if (IsVisibleInTree())
                _OnDisable();
            _OnDestroy();
        }

        public override void _Ready()
        {
            _ready = true;
            if (IsVisibleInTree())
            {
                _enabled = true;
                _started = true;
                _Start();
            }
        }

        public override void _Process(double delta)
        {
            if (_enabled == false) return;
            _Update(delta);
        }

        public override void _PhysicsProcess(double delta)
        {
            if (_enabled == false) return;
            _FixedUpdate(delta);
        }

        private void OnVisibilityChanged()
        {
            if (_enabled == IsVisibleInTree()) return;
            _enabled = !_enabled;
            if (_enabled)
            {
                if (_enterTree && _awaked == false)
                {
                    _awaked = true;
                    _Awake();
                }

                _OnEnable();

                if (_ready && _started == false)
                {
                    _started = true;
                    _Start();
                }
            }
            else
            {
                if (_exitTree == false) _OnDisable();
            }
            SetProcess(_enabled);
            SetPhysicsProcess(_enabled);
        }

        public void Destroy()
        {
            QueueFree();
        }
    }
}