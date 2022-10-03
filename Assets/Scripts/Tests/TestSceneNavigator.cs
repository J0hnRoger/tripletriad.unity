using UnityEngine;
using UnityEngine.InputSystem;

namespace TripleTriad
{
    public class TestSceneNavigator : MonoBehaviour
    {
        private GameManager _gm;
        [SerializeField] private InputAction goToSceneAction;

         private void OnEnable()
        {
           goToSceneAction.Enable(); 
        }

        private void OnDisable()
        {
           goToSceneAction.Disable(); 
        }

        public void Awake()
        {
            _gm = FindObjectOfType<GameManager>();
            goToSceneAction.performed += LoadScene;
        }

        private void LoadScene(InputAction.CallbackContext context)
        {
            float sceneToLoad = context.ReadValue<float>();
            _gm.Load((SceneName)sceneToLoad);
        }
    }
}
