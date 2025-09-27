using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.SceneAccess
{
    public class CheckSceneAccess : MonoBehaviour
    {
        private const string InitialSceneName = "Initial";

        private void Awake()
        {
            if (!SceneAccess.WasOnInitial)
            {
                SceneManager.LoadScene(InitialSceneName, LoadSceneMode.Single);
            }
        }
        
    }
}