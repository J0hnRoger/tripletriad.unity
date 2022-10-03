using System;
using System.Collections;
using System.Collections.Generic;
using TripleTriad.Core.EventArchitecture.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TripleTriad
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private GameObject _pfLoadingScreen;

        private SceneInstance _prevScene;
        private readonly List<AsyncOperation> _loadOperations = new();

        public void Load(SceneInstance scene)
        {
            StartCoroutine(LoadAsync(scene));
        }

        IEnumerator LoadAsync(SceneInstance scene)
        {
            // display transition 
            _pfLoadingScreen.gameObject.SetActive(true);

            if (_prevScene != null)
            {
               AsyncOperation unloadTask = SceneManager.UnloadSceneAsync(_prevScene.SceneName.ToString());
               yield return new WaitUntil(() => unloadTask.isDone);
            }
            
            AsyncOperation operation = SceneManager.LoadSceneAsync(scene.SceneName.ToString(),
                (scene.IsAdditive) ? LoadSceneMode.Additive : LoadSceneMode.Single);
            if (operation == null)
                throw new Exception($"Scene {scene.SceneName.ToString()} inexistante");

            _loadOperations.Add(operation);
            while (!operation.isDone)
            {
                yield return null;
            }

            if (_loadOperations.Contains(operation))
                _loadOperations.Remove(operation);

      
            _prevScene = scene; 
            
            _pfLoadingScreen.gameObject.SetActive(false);
            scene.OnLoaded?.Invoke();
        }
    }
}