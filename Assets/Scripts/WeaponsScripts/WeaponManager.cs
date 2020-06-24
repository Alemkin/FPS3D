using UnityEngine;

namespace Assets.Scripts.WeaponsScripts
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField]
        private WeaponHandler[] _weapons;

        private int _currentWeaponIndex;

        void Start()
        {
            _currentWeaponIndex = 0;
            _weapons[_currentWeaponIndex].gameObject.SetActive(true);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                TurnOnSelectedWeapon(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                TurnOnSelectedWeapon(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                TurnOnSelectedWeapon(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                TurnOnSelectedWeapon(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                TurnOnSelectedWeapon(4);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                TurnOnSelectedWeapon(5);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                var nextIndex = _currentWeaponIndex < _weapons.Length - 1 ? _currentWeaponIndex + 1 : 0;
                TurnOnSelectedWeapon(nextIndex);
            }
        }

        void TurnOnSelectedWeapon(int weaponIndex)
        {
            if (weaponIndex == _currentWeaponIndex) return;
            _weapons[_currentWeaponIndex].gameObject.SetActive(false);
            _weapons[weaponIndex].gameObject.SetActive(true);
            _currentWeaponIndex = weaponIndex;
        }

        public WeaponHandler GetCurrentSelectedWeapon()
        {
            return _weapons[_currentWeaponIndex];
        }
    }
}
