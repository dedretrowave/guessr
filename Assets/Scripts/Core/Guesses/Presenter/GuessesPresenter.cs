using System;
using Core.Guesses.Model;
using Core.Guesses.View;
using UnityEngine;

namespace Core.Guesses.Presenter
{
    public class GuessesPresenter
    {
        private GuessesView _view;

        private GuessesModel _model;

        public GuessesPresenter(GuessesView view, int totalAmountOfGuesses)
        {
            _view = view;
            _model = new(totalAmountOfGuesses);
            _view.Clear();
            _view.Draw(totalAmountOfGuesses);
        }

        public void OnGuess()
        {
            if (_model.CurrentGuessIndex == _model.TotalAmountOfGuesses)
                return;

            _view.Mark(_model.CurrentGuessIndex);
            _model.IncreaseIndex();
        }
    }
}