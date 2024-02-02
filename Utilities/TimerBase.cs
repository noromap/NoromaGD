using System;

namespace NoromaGD
{
    public abstract class TimerBase
    {
        public float duration;
        public float currentTime;

        public float normalizedTime
        {
            get
            {
                if (duration == 0) return 0;
                float time = currentTime / duration;
                if (time > 1) time = 1;
                return time;
            }
        }

        public TimerBase()
        { }

        public abstract void Reset();

        public TimerBase(float duration)
        {
            this.duration = duration;
        }
        public Func<bool> timerResetJudger = null;

        //タイマーが終了したか
        public bool End
        { get { return !IsInTime(); } }

        public void Update(double delta)
        {
            if (duration == 0) return;
            if (timerResetJudger != null && timerResetJudger()) Set();
            if (!IsInTime()) return;
            OnUpdate((float)delta);
        }

        protected abstract void OnUpdate(float delta);

        public abstract void Set();

        public void Set(float duration)
        {
            this.duration = duration;
            Set();
        }

        public abstract bool IsInTime();
    }
}