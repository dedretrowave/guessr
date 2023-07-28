using Core.Score.Model;
using Core.Score.View;

namespace Core.Score.Presenter
{
    public class ScorePresenter
    {
        private ScoreView _view;

        private ScoreModel _model;

        public int Score => _model.Score;

        public ScorePresenter(ScoreView view)
        {
            _view = view;

            _model = new();
            
            _view.SetScore(_model.Score);
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