using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(Collider2D))]
    public class Passage : MonoBehaviour
    {
        public Transform connection;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var position = connection.position;
            var otherTransform = other.transform;

            position.z = otherTransform.position.z;
            otherTransform.position = position;
        }
    }
}