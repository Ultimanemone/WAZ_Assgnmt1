using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WAZ_Assgnmt1.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }
        public string currentScene { get; private set; }
        public bool isPaused { get; private set; }
        private GameManager() { }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Debug.LogError($"Duplicate GameManager on {gameObject.name}!");
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
#if !UNITY_EDITOR
            LoadScene("Title");
#endif
        }

        private void OnDestroy()
        {
            if (instance == this)
                SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            currentScene = scene.name;
            isPaused = false;
            Time.timeScale = 1f;
        }

        /// <summary>
        /// Load a scene
        /// </summary>
        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
            instance.currentScene = scene;
        }

        /// <summary>
        /// Load the next scene in build settings
        /// </summary>
        public void LoadNextScene()
        {
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentIndex + 1);
        }

        /// <summary>
        /// Reload the current scene
        /// </summary>
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /// <summary>
        /// Pause the game
        /// </summary>
        public void SetPaused(bool paused)
        {
            isPaused = paused;
            Time.timeScale = paused ? 0f : 1f;
        }

        /// <summary>
        /// Quit the game
        /// </summary>
        public void QuitGame()
        {
            Debug.Log("Game Quit");
            Application.Quit();
        }
    }
}