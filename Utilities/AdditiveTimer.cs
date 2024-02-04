namespace NoromaGD
{
    public class AdditiveTimer : TimerBase
    {
        public AdditiveTimer()
        { }

        public AdditiveTimer(float duration) : base(duration)
        {
        }

        public override bool IsInTime()
        {
            return currentTime < duration;
        }

        public override void Reset()
        {
            currentTime = duration;
        }

        public override void Set()
        {
            currentTime = 0;
        }

        protected override void OnUpdate(float delta)
        {
            currentTime += delta;
            if (currentTime > duration) currentTime = duration;
        }
    }
}