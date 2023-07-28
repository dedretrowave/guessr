using System;
using Core.Music;
using Core.Score;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private MusicInstaller _music;
        [SerializeField] private ScoreInstaller _score;

        private void Awake()
        {
            _music.Construct();
            _score.Construct();
        }

        private void OnDisable()
        {
            _score.Disable();
        }

        public void LaunchLevel()
        {
            SceneManager.LoadScene("Level");
        }
    }
}