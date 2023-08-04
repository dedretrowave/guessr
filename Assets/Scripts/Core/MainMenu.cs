using System;
using Core.Music;
using Core.Score;
using DI;
using Save;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private MusicInstaller _music;
        [SerializeField] private ScoreInstaller _score;
        [SerializeField] private SaveMediator _saveMediator;

        private void Awake()
        {
            DependencyContext.Dependencies.Add(new(typeof(SaveMediator), () => _saveMediator));
            
            _music.Construct();
            _score.Construct();
        }

        private void OnDisable()
        {
            _music.Disable();
            _score.Disable();
        }

        public void LaunchLevel()
        {
            SceneManager.LoadScene("Level");
        }
    }
}