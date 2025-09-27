using UnityEngine;

namespace Game.Scene
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Vector3 rotationSpeed = new Vector3(0, 0, 100);

        void Update()
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }
    }
}