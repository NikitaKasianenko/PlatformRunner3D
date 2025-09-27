using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.SceneAccess
{
    public class CheckSceneAccess : MonoBehaviour
    {
        private const string initialSceneName = "Initial";

        private void Awake()
        {
            if (!SceneAccess.WasOnInitial)
            {
                SceneManager.LoadScene(initialSceneName, LoadSceneMode.Single);
            }
        }
        
    }
}