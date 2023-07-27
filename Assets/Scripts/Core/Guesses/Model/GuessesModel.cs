namespace Core.Guesses.Model
{
    public class GuessesModel
    {
        private int _currentGuessIndex;
        private int _totalAmountOfGuesses;

        public int CurrentGuessIndex => _currentGuessIndex;
        public int TotalAmountOfGuesses => _totalAmountOfGuesses;

        public GuessesModel(int totalAmountOfGuesses)
        {
            _currentGuessIndex = 0;
            _totalAmountOfGuesses = totalAmountOfGuesses;
        }

        public void IncreaseIndex()
        {
            _currentGuessIndex++;
        }
    }
}