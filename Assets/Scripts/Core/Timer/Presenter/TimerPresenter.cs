using System;
using System.Collections;
using Core.Timer.Model;
using Core.Timer.View;

namespace Core.Timer.Presenter
{
    public class TimerPresenter
    {
        private TimerModel _model;

        private TimerView _view;

        public Action OnTimeout;

        public TimerPresenter(TimerView view, int initialTime)
        {
            _model = new(initialTime);
            _view = view;
            _view.SetTime(_model.CurrentTime);
        }

        public void Increase(int amount)
        {
            _model.Increase(amount);
            _view.SetTime(_model.CurrentTime);
        }

        public void Decrease(int amount)
        {
            if (_model.CurrentTime <= 0) return;
            
            _model.Decrease(amount);
            _view.SetTime(_model.CurrentTime);

            if (_model.CurrentTime == 0)
            {
                OnTimeout?.Invoke();
            }
        }

        public void DecreaseByOneSec()
        {
            if (_model.CurrentTime <= 0)
            {
                return;
            }

            _model.DecreaseByOneSec();
            _view.SetTime(_model.CurrentTime);

            if (_model.CurrentTime == 0)
            {
                OnTimeout?.Invoke();
            }
        }
    }
}