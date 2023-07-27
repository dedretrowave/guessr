using System;

namespace Core.Differs.Model
{
    public class DiffersModel
    {
        private int _totalDiffers;
        private int _foundDiffers;

        public int TotalDiffers => _totalDiffers;
        public int FoundDiffers => _foundDiffers;

        public Action OnAllDiffersFound;

        public DiffersModel(int totalDiffers, int foundDiffers = 0)
        {
            _totalDiffers = totalDiffers;
            _foundDiffers = foundDiffers;
        }

        public void IncreaseFoundDiffers()
        {
            _foundDiffers++;

            if (_foundDiffers == _totalDiffers)
            {
                OnAllDiffersFound?.Invoke();
            }
        }
    }
}