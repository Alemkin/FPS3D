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


        // Start is called before the first frame update
        void Awake()
        {
            _weaponManager = GetComponent<WeaponManager>();
        }

        // Update is called once per frame
        void Update()
        {
            ShootWeapon();
        }

        void ShootWeapon()
        {
            var currentWeapon = _weaponManager.GetCurrentSelectedWeapon();
            if (currentWeapon.FireType == WeaponFireType.Multiple)
            {
                if (Input.GetMouseButton(0) && Time.time > _nextTimeToFire)
                {
                    _nextTimeToFire = Time.time + 1f / FireRate;

                    currentWeapon.AttackAnimation();

                    //BulletFired();
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (currentWeapon.tag == Tags.AXE_TAG)
                    {
                        currentWeapon.AttackAnimation();
                    }

                    if (currentWeapon.BulletType == WeaponBulletType.Bullet)
                    {
                        currentWeapon.AttackAnimation();

                        //BulletFired();
                    }
                    else
                    {
                        //arrow or spear, must be aiming
                    }
                }
            }
        }
    }
}
