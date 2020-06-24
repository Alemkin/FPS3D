using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class PlayerFootsteps : MonoBehaviour
    {
        private AudioSource _footstepSound;

        [SerializeField]
        private AudioClip[] _footstepClip;

        private CharacterController _characterController;

        [HideInInspector]
        public float VolumeMin, VolumeMax;

        private float _accumulatedDistance;

        [HideInInspector]
        public float StepDistance;

        private bool _wasNotGrounded;

        void Awake()
        {
            _footstepSound = GetComponent<AudioSource>();
            _characterController = GetComponentInParent<CharacterController>();
        }

        void Update()
        {
            PlayFootstepSounds();
        }

        void PlayFootstepSounds()
        {
            if (!_characterController.isGrounded)
            {
                _wasNotGrounded = true;
                return;
            }
            if (_wasNotGrounded)
            {
                _wasNotGrounded = false;
                _footstepSound.clip = _footstepClip[Random.Range(0, _footstepClip.Length)];
                _footstepSound.Play();
            }
            if (_characterController.velocity.sqrMagnitude > 0)
            {
                _accumulatedDistance += Time.deltaTime;

                if (_accumulatedDistance > StepDistance)
                {
                    _footstepSound.volume = Random.Range(VolumeMin, VolumeMax);
                    _footstepSound.clip = _footstepClip[Random.Range(0, _footstepClip.Length)];
                    _footstepSound.Play();
                    _accumulatedDistance = 0f;
                }
            }
            else
            {
                _accumulatedDistance = 0f;
            }
        }
    }
}
