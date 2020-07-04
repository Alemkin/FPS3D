using Assets.Scripts.WeaponsScripts;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class PlayerAttack : MonoBehaviour
    {

        private WeaponManager _weaponManager;

        public float FireRate = 15f;
        public float Damage = 20f;

        private float _nextTimeToFire;
        private float _nextTimeToLaunchSpear;
        private float _nextTimeToLaunchArrow;
        private Animator _zoomCameraAnim;
        private bool _isZoomed;

        private Camera _mainCamera;

        private GameObject _crossHair;

        private bool _isAiming;

        [SerializeField]
        private GameObject _arrowPrefab, _spearPrefab;

        [SerializeField]
        private Transform _arrowStartPosition;

        [SerializeField]
        private Transform _spearStartPosition;

        // Start is called before the first frame update
        void Awake()
        {
            _weaponManager = GetComponent<WeaponManager>();
            _zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
            _crossHair = GameObject.FindWithTag(Tags.CROSS_HAIR);

            _mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            ShootWeapon();
            ZoomInAndOut();
        }

        void ShootWeapon()
        {
            var currentWeapon = _weaponManager.GetCurrentSelectedWeapon();
            if (currentWeapon.FireType == WeaponFireType.Multiple && Input.GetMouseButton(0) && Time.time > _nextTimeToFire)
            {
                _nextTimeToFire = Time.time + 1f / FireRate;

                currentWeapon.AttackAnimation();

                BulletFired();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                if (currentWeapon.tag == Tags.AXE_TAG)
                {
                    currentWeapon.AttackAnimation();
                }

                if (currentWeapon.BulletType == WeaponBulletType.Bullet)
                {
                    currentWeapon.AttackAnimation();

                    BulletFired();
                }
                else if (_isAiming)
                {
                    //Arrow or Spear
                    switch (currentWeapon.BulletType)
                    {
                        case WeaponBulletType.Arrow:
                            ThrowArrowOrSpear(true);
                            break;
                        case WeaponBulletType.Spear:
                            ThrowArrowOrSpear(false);
                            break;
                    }
                }
            }
        }

        void ZoomInAndOut()
        {
            var currentWeapon = _weaponManager.GetCurrentSelectedWeapon();
            if (currentWeapon.WeaponAim == WeaponAim.Aim)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    _zoomCameraAnim.Play(AnimationTags.ZOOM_IN);
                    _crossHair.SetActive(false);
                }
                if (Input.GetMouseButtonUp(1))
                {
                    _zoomCameraAnim.Play(AnimationTags.ZOOM_OUT);
                    _crossHair.SetActive(true);
                }
            }

            if (currentWeapon.WeaponAim == WeaponAim.SelfAim)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    currentWeapon.Aim(true);
                    _isAiming = true;
                }
                if (Input.GetMouseButtonUp(1))
                {
                    currentWeapon.Aim(false);
                    _isAiming = false;
                }
            }
        }

        void ThrowArrowOrSpear(bool throwArrow)
        {
            if (throwArrow && Time.time > _nextTimeToLaunchArrow)
            {
                _weaponManager.GetCurrentSelectedWeapon().AttackAnimation();
                _nextTimeToLaunchArrow = Time.time + 2.8f;
                GameObject arrow = Instantiate(_arrowPrefab);
                arrow.transform.position = _arrowStartPosition.position;

                arrow.GetComponent<ArrowAndSpearHandler>().Launch(_mainCamera);
            }
            else if (!throwArrow && Time.time > _nextTimeToLaunchSpear)
            {
                _weaponManager.GetCurrentSelectedWeapon().AttackAnimation();
                _nextTimeToLaunchSpear = Time.time + 3.2f;
                GameObject spear = Instantiate(_spearPrefab);
                spear.transform.position = _spearStartPosition.position;

                spear.GetComponent<ArrowAndSpearHandler>().Launch(_mainCamera);
            }
        }

        void BulletFired()
        {
            if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out var hit))
            {
                print("We hit: " + hit.transform.gameObject.name);
            }
        }
    }
}
