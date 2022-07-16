using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace.GameSession
{
    public class SceneContext : MonoBehaviour
    {
        public static SceneContext Instance;

        public PauseManager PauseManager;
        public bool IsSceneReload { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                
                DontDestroyOnLoad(gameObject);
            }
        }
        
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            PauseManager.SetPaused(false);
            
            IsSceneReload = true;
        }

        public void GameOver()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}