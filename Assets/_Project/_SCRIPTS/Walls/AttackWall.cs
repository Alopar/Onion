using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class AttackWall : Wall
    {
        [SerializeField] private WallWeapon _wallWeaponPrefab;

        private int _weaponsCount;
        private List<WallWeapon> _weapons = new();

        private void Update()
        {
            foreach (WallWeapon weapon in _weapons)
                weapon.Shoot(_wallDirection);
        }

        public void CreateWeapons(float angle, float radius, WallsProgress wallsProgress)
        {
            _weaponsCount = wallsProgress.GetWallsCount(_wallRing);
            angle = angle / 180;
            if (_weaponsCount % 2 == 1)
            {
                float step = angle * Mathf.PI / _weaponsCount;
                int centerIndex = (_weaponsCount - 1) / 2;
                float center = angle * Mathf.PI / 2;

                for (int i = 0; i < _weaponsCount; i++)
                {
                    float x = Mathf.Cos(center + (i - centerIndex) * step) * radius;
                    float y = Mathf.Sin(center + (i - centerIndex) * step) * radius;
                    Vector3 position = new Vector3(x, y, 0);
                    WallWeapon weapon = Instantiate(_wallWeaponPrefab, transform);
                    weapon.transform.localPosition = position;
                    Vector3 lookDirection = transform.position - weapon.transform.position;
                    weapon.transform.right = -lookDirection;
                    _weapons.Add(weapon);
                }
            }
            else
            {
                float step = angle * Mathf.PI / _weaponsCount;
                float startOffset = step / 2f;

                for (int i = 0; i < _weaponsCount; i++)
                {
                    float x = Mathf.Cos(startOffset + i * step) * radius;
                    float y = Mathf.Sin(startOffset + i * step) * radius;
                    Vector3 position = new Vector3(x, y, 0);

                    WallWeapon weapon = Instantiate(_wallWeaponPrefab, transform);
                    weapon.transform.localPosition = position;

                    Vector3 lookDirection = transform.position - weapon.transform.position;
                    weapon.transform.right = -lookDirection;
                    _weapons.Add(weapon);
                }
            }
        }
    }
}