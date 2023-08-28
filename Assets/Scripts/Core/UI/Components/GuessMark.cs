using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Components
{
    public class GuessMark : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Animator _animator;
        
        [SerializeField] private Sprite _emptyMark;
        [SerializeField] private Sprite _fullMark;
        
        private readonly int _show = Animator.StringToHash("Show");

        private void Awake()
        {
            SetSprite(_emptyMark);
        }

        public void SetMarked()
        {
            SetSprite(_fullMark);
            PlayMarked();
        }

        private void SetSprite(Sprite image)
        {
            _image.sprite = image;
        }

        private void PlayMarked()
        {
            _animator.SetTrigger(_show);
        }
    }
}