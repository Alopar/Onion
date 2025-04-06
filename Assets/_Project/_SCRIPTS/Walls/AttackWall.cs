using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class AttackWall : Wall
    {
        [SerializeField] private WallWeapon _wallWeaponPrefab;
        [SerializeField] private int _weaponsCount;

        private List<WallWeapon> _weapons = new();

        private void Update()
        {
            foreach (WallWeapon weapon in _weapons)
                weapon.Shoot(_wallDirection);
        }

        public void CreateWeapons()
        {
            int lineLength = _lineRenderer.positionCount;


            if (_weaponsCount % 2 == 1)
            {
                float step = (lineLength - 1f) / _weaponsCount;
                int centerIndex = (_weaponsCount - 1) / 2;
                int center = (lineLength) / 2;

                for (int i = 0; i < _weaponsCount; i++)
                {
                    Vector3 position = _lineRenderer.GetPosition((int)(center + (i - centerIndex) * step));
                    WallWeapon weapon = Instantiate(_wallWeaponPrefab, transform);
                    weapon.transform.localPosition = position;
                    Vector3 lookDirection = transform.position - weapon.transform.position;
                    weapon.transform.right = -lookDirection;
                    _weapons.Add(weapon);
                }
            }
            else
            {
                float step = (lineLength - 1f) / (_weaponsCount);
                float startOffset = step / 2f;

                for (int i = 0; i < _weaponsCount; i++)
                {
                    Vector3 position = _lineRenderer.GetPosition(Mathf.CeilToInt((startOffset + i * step)));
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