using UnityEngine;

namespace Assets.Scripts.WeaponsScripts
{
    public class ArrowAndSpearHandler : MonoBehaviour
    {
        private Rigidbody _rigidBody;

        public float Speed = 30f;
        public float DeactivateTime = 3f;
        public float Damage = 15f;

        void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        void Start()
        {
            Invoke("DeactivateGameObject", DeactivateTime);
        }

        public void Launch(Camera mainCamera)
        {
            _rigidBody.velocity = mainCamera.transform.forward * Speed;
            transform.LookAt(transform.position + _rigidBody.velocity);
        }

        void DeactivateGameObject()
        {
            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
            }
        }

        void OnTriggerEnter(Collider target)
        {
            //deactivate once touch enemy
        }
    }
}
