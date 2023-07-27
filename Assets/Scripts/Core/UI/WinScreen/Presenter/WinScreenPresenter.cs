using Core.UI.WinScreen.View;

namespace Core.UI.WinScreen.Presenter
{
    public class WinScreenPresenter
    {
        private WinScreenView _view;

        public WinScreenPresenter(WinScreenView view)
        {
            _view = view;
        }

        public void DisplayTotalScore(int score)
        {
            _view.DisplayTotalScore(score);
        }

        public void Show()
        {
            _view.Show();
        }
    }
}