using UnityEngine;

namespace Assets.Scripts.WeaponsScripts
{

    public enum WeaponAim
    {
        None,
        SelfAim,
        Aim
    }

    public enum WeaponFireType
    {
        Single,
        Multiple
    }

    public enum WeaponBulletType
    {
        Bullet,
        Arrow,
        Spear,
        None
    }

    public class WeaponHandler : MonoBehaviour
    {

        private Animator _animator;

        [SerializeField]
        private GameObject _muzzleFlash;

        [SerializeField]
        private AudioSource _shootSound, _reloadSound;

        public WeaponFireType FireType;

        public WeaponBulletType BulletType;

        public GameObject AttackPoint;

        public WeaponAim WeaponAim;

        void Awake()
        {
            _animator = GetComponent<Animator>();

        }

        public void AttackAnimation()
        {
            _animator.SetTrigger(AnimationTags.ATTACK_TRIGGER);
        }

        public void Aim(bool canAim)
        {
            _animator.SetBool(AnimationTags.AIM_PARAMETER, canAim);
        }

        void TurnOnMuzzleFlash()
        {
            _muzzleFlash.SetActive(true);
        }

        void TurnOffMuzzleFlash()
        {
            _muzzleFlash.SetActive(false);
        }

        void PlayShootSound()
        {
            _shootSound.Play();
        }

        void PlayReloadSound()
        {
            _reloadSound.Play();
        }

        void TurnOnAttackPoint()
        {
            AttackPoint.SetActive(true);
        }

        void TurnOffAttackPoint()
        {
            if (AttackPoint.activeInHierarchy)
            {
                AttackPoint.SetActive(false);
            }
        }
    }
}
