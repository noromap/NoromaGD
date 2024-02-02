namespace NoromaGD
{
    public class SubtractiveTimer : TimerBase
    {
        public SubtractiveTimer(float duration) : base(duration)
        {
        }

        public SubtractiveTimer()
        { }

        protected override void OnUpdate(float delta)
        {
            if (currentTime == 0) return;
            currentTime -= delta;
            if (currentTime < 0) currentTime = 0;
        }

        public override void Reset()
        {
            currentTime = 0;
        }

        public override void Set()
        {
            currentTime = duration;
        }

        public override bool IsInTime()
        {
            return currentTime > 0;
        }
    }
}