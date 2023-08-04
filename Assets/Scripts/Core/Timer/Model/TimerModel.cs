namespace Core.Timer.Model
{
    public class TimerModel
    {
        private int _currentTime;

        public int CurrentTime => _currentTime;

        public TimerModel(int initialTime)
        {
            _currentTime = initialTime;
        }

        public void Decrease(int amount)
        {
            int newTime = _currentTime - amount;

            if (newTime < 0)
            {
                _currentTime = 0;
                return;
            }

            _currentTime = newTime;
        }

        public void Increase(int amount)
        {
            _currentTime += amount;
        }

        public void DecreaseByOneSec()
        {
            _currentTime--;
        }
    }
}