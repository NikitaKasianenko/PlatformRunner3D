using UnityEngine.SceneManagement;

namespace Game
{
    public static class GlobalUtils
    {
        public static string CurrentLevel()
        {
            return SceneManager.GetActiveScene().name;
        }
    }

}