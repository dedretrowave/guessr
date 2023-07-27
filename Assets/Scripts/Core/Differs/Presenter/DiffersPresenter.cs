using System;
using Core.Differs.Model;
using Core.Differs.View;
using UnityEngine;

namespace Core.Differs.Presenter
{
    public class DiffersPresenter
    {
        private DiffersView _view;
        private DiffersModel _model;

        public Action OnAllDiffersFound;
        public Action OnFound;

        public DiffersPresenter(DiffersView view, DiffersModel model)
        {
            _view = view;
            
            _model = model;
        }

        public void OnRandomDifferFound()
        {
            int index = _view.OnRandomGuessMarkAndReturn();
            
            OnDifferFound(index);
        }

        public void OnDifferFound(int panelId)
        {
            _model.IncreaseFoundDiffers();
            _view.SyncCorrectPanels(panelId);

            OnFound?.Invoke();

            if (_model.FoundDiffers == _model.TotalDiffers)
            {
                OnAllDiffersFound?.Invoke();
            }
        }
    }
}