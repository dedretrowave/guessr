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
            _currentTime -= amount;
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