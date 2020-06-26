using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class PlayerSprintAndCrouch : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private Transform _lookRoot;

        public float SprintSpeed = 10f;
        public float MoveSpeed = 5f;
        public float CrouchSpeed = 2f;

        private float _standHeight = 1.6f;
        private float _crouchHeight = 1f;

        private bool _isCrouching;

        private PlayerFootsteps _playerFootsteps;
        private CharacterController _characterController;
        private float _sprintVolume = 1f;
        private float _crouchVolume = 0.1f;
        private float _walkVolumeMin = 0.2f, _walkVolumeMax = 0.6f;
        private float _walkStepDistance = 0.4f;
        private float _sprintStepDistance = 0.25f;
        private float _crouchStepDistance = 0.5f;

        void Start()
        {
            _playerFootsteps.VolumeMin = _walkVolumeMin;
            _playerFootsteps.VolumeMax = _walkVolumeMax;
            _playerFootsteps.StepDistance = _walkStepDistance;
        }

        void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _lookRoot = transform.GetChild(0);
            _playerFootsteps = GetComponentInChildren<PlayerFootsteps>();
            _characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            Sprint();
            Crouch();
        }

        void Sprint()
        {
            if (Input.GetKey(KeyCode.LeftShift) && !_isCrouching && _characterController.isGrounded)
            {
                _playerMovement.Speed = SprintSpeed;
                _playerFootsteps.VolumeMin = _sprintVolume;
                _playerFootsteps.VolumeMax = _sprintVolume;
                _playerFootsteps.StepDistance = _sprintStepDistance;
            } else if (!_isCrouching && _characterController.isGrounded)
            {
                _playerMovement.Speed = MoveSpeed;
                _playerFootsteps.VolumeMin = _walkVolumeMin;
                _playerFootsteps.VolumeMax = _walkVolumeMax;
                _playerFootsteps.StepDistance = _walkStepDistance;
            }
        }

        void Crouch()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (_isCrouching)
                {
                    _lookRoot.localPosition = new Vector3(0f, _standHeight, 0f);
                    _playerMovement.Speed = MoveSpeed;
                    _isCrouching = false;
                    _playerFootsteps.VolumeMin = _walkVolumeMin;
                    _playerFootsteps.VolumeMax = _walkVolumeMax;
                    _playerFootsteps.StepDistance = _walkStepDistance;
                }
                else if (_characterController.isGrounded)
                {
                    _lookRoot.localPosition = new Vector3(0f, _crouchHeight, 0f);
                    _playerMovement.Speed = CrouchSpeed;
                    _isCrouching = true;
                    _playerFootsteps.VolumeMin = _crouchVolume;
                    _playerFootsteps.VolumeMax = _crouchVolume;
                    _playerFootsteps.StepDistance = _crouchStepDistance;

                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && _isCrouching)
            {
                _lookRoot.localPosition = new Vector3(0f, _standHeight, 0f);
                _playerMovement.Speed = MoveSpeed;
                _isCrouching = false;
            }
        }
    }
}
