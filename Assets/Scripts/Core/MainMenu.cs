using System;
using Core.Music;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private MusicInstaller _music;
        [SerializeField] private Button _button;

        private void Awake()
        {
            _music.Construct();
            _button.onClick.AddListener(LaunchLevel);
        }

        private void LaunchLevel()
        {
            SceneManager.LoadScene("Level");
        }
    }
}