using Core.Guesses.Presenter;
using Core.Guesses.View;
using EventBus;
using UnityEngine;

namespace Core.Guesses
{
    public class GuessesInstaller : MonoBehaviour
    {
        [SerializeField] private GuessesView _view;

        private EventBus.EventBus _eventBus;

        private GuessesPresenter _presenter;

        public void Construct(int totalAmountOfGuesses)
        {
            _eventBus = EventBus.EventBus.Instance;

            _presenter = new GuessesPresenter(_view, totalAmountOfGuesses);
            
            _eventBus.AddListener(EventName.ON_DIFFER_FOUND, _presenter.OnGuess);
        }

        public void Disable()
        {
            _eventBus.RemoveListener(EventName.ON_DIFFER_FOUND, _presenter.OnGuess);
        }
    }
}