using Core.Score.Model;
using Core.Score.View;

namespace Core.Score.Presenter
{
    public class ScorePresenter
    {
        private ScoreView _view;

        private ScoreModel _model;

        public ScorePresenter(ScoreView view)
        {
            _view = view;

            _model = new();
        }

        public void IncreaseScore()
        {
            _model.IncreaseScore();
            _view.SetScore(_model.Score);
        }

        public int GetCurrentScore()
        {
            return _model.Score;
        }
    }
}