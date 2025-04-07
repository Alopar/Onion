using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class WallsManager : MonoBehaviour
    {
        [SerializeField, Required] private ProtectWall _protectWallPrefab;
        [SerializeField, Required] private AttackWall _attackWallPrefab;
        [SerializeField, Required] private ProduceWall _produceWallPrefab;
        [SerializeField, Required] private WallPreview _wallPreviewPrefab;
        [SerializeField, Required] private WallsProgress _wallsProgress;
        [SerializeField, Required] private Transform _wallsContainer;

        [SerializeField] private float _offsetFromCenter;
        [SerializeField] private float _offsetBetweenWalls;
        [SerializeField] private float _defaultWallThickness;
        [SerializeField] private int _wallSegments;
        [SerializeField] private float _wallAngle;
        [SerializeField] private int _colliderSegments;

        [SerializeField] private AudioClip _buildSoundEffect;
        [SerializeField] private AudioClip _destroySoundEffect;

        private Dictionary<WallDirection, List<Wall>> _walls = new();
        private Dictionary<WallDirection, WallPreview> _previews = new();

        public WallsProgress WallsProgress => _wallsProgress;

        public static WallsManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            InitWallsDictionary();
        }

        private void Start()
        {
            UpdateCreationPreviews();
        }

        public T CreateWall<T>(WallDirection direction) where T : Wall
        {
            Wall wall;
            if (typeof(T) == typeof(ProtectWall))
                wall = Instantiate(_protectWallPrefab);
            else if (typeof(T) == typeof(AttackWall))
                wall = Instantiate(_attackWallPrefab);
            else
                wall = Instantiate(_produceWallPrefab);

            wall.transform.SetParent(_wallsContainer);

            int ring = GetFreeRing(direction);
            float wallRadius = GetNextWallDistance(ring);
            wall.transform.eulerAngles = WallsSettings.Instance.GetWallAngle(direction);
            wall.Init(direction, ring);
            wall.Draw(_wallSegments, _wallAngle, wallRadius);
            wall.UpdateCollider(_colliderSegments);
            if (wall is AttackWall attackWall) attackWall.CreateWeapons(_wallAngle, wallRadius);
            _walls[direction].Add(wall);
            UpdateCreationPreviews();
            SoundManager.Instance.PlaySound(_buildSoundEffect);
            return (T)wall;
        }

        public void DestroyWall(Wall wall)
        {
            _walls[wall.WallDirection].Remove(wall);
            Destroy(wall.gameObject);
            UpdateCreationPreviews();
            SoundManager.Instance.PlaySound(_destroySoundEffect);
        }

        private void InitWallsDictionary()
        {
            _walls.Add(WallDirection.TopLeft, new List<Wall>());
            _walls.Add(WallDirection.Top, new List<Wall>());
            _walls.Add(WallDirection.TopRight, new List<Wall>());
            _walls.Add(WallDirection.Right, new List<Wall>());
            _walls.Add(WallDirection.BottomRight, new List<Wall>());
            _walls.Add(WallDirection.Bottom, new List<Wall>());
            _walls.Add(WallDirection.BottomLeft, new List<Wall>());
            _walls.Add(WallDirection.Left, new List<Wall>());
        }

        private void UpdateCreationPreviews()
        {
            foreach (WallDirection direction in _walls.Keys)
                if (CanPlace(direction))
                    CreatePreviewView(direction);
                else
                    RemovePreviewView(direction);
        }

        private void RemovePreviewView(WallDirection direction)
        {
            if (_previews.TryGetValue(direction, out WallPreview preview) && preview != null)
                Destroy(preview.gameObject);
        }

        private void CreatePreviewView(WallDirection direction)
        {
            WallPreview preview = Instantiate(_wallPreviewPrefab);
            preview.transform.SetParent(_wallsContainer);
            int ring = GetFreeRing(direction);
            float wallRadius = GetNextWallDistance(ring);
            preview.transform.eulerAngles = WallsSettings.Instance.GetWallAngle(direction);
            preview.Init(direction, ring);
            preview.Draw(_wallSegments, _wallAngle, wallRadius);
            preview.UpdateCollider(_colliderSegments);

            if (_previews.TryGetValue(direction, out WallPreview lastPreview))
            {
                if (lastPreview != null)
                    Destroy(lastPreview.gameObject);
                _previews[direction] = preview;
            }
            else
                _previews.Add(direction, preview);
        }

        private int GetFreeRing(WallDirection direction)
        {
            int ring = direction.IsCorner() ? 1 : 0;
            while (_walls[direction].FirstOrDefault(x => x.Ring == ring) != default)
                ring += 2;

            return ring;
        }


        private float GetNextWallDistance(int ring)
        {
            float offsetsBetween = ring * _offsetBetweenWalls;
            float wallsThickness = ring * _defaultWallThickness;
            return _offsetFromCenter + offsetsBetween + wallsThickness;
        }

        private bool CanPlace(WallDirection direction)
        {
            int ring = GetFreeRing(direction);

            switch (direction)
            {
                case WallDirection.Top:
                    if (ring == 0)
                        return true;

                    return _walls[WallDirection.TopLeft].FirstOrDefault(x => x.Ring == ring - 1) != default
                        && _walls[WallDirection.TopRight].FirstOrDefault(x => x.Ring == ring - 1) != default;

                case WallDirection.Right:
                    if (ring == 0)
                        return true;

                    return _walls[WallDirection.TopRight].FirstOrDefault(x => x.Ring == ring - 1) != default
                        && _walls[WallDirection.BottomRight].FirstOrDefault(x => x.Ring == ring - 1) != default;

                case WallDirection.Bottom:
                    if (ring == 0)
                        return true;

                    return _walls[WallDirection.BottomRight].FirstOrDefault(x => x.Ring == ring - 1) != default
                        && _walls[WallDirection.BottomLeft].FirstOrDefault(x => x.Ring == ring - 1) != default;

                case WallDirection.Left:
                    if (ring == 0)
                        return true;

                    return _walls[WallDirection.BottomLeft].FirstOrDefault(x => x.Ring == ring - 1) != default
                        && _walls[WallDirection.TopLeft].FirstOrDefault(x => x.Ring == ring - 1) != default;

                case WallDirection.TopLeft:
                    return _walls[WallDirection.Left].FirstOrDefault(x => x.Ring == ring - 1) != default
                        && _walls[WallDirection.Top].FirstOrDefault(x => x.Ring == ring - 1) != default;

                case WallDirection.TopRight:
                    return _walls[WallDirection.Top].FirstOrDefault(x => x.Ring == ring - 1) != default
                        && _walls[WallDirection.Right].FirstOrDefault(x => x.Ring == ring - 1) != default;

                case WallDirection.BottomRight:
                    return _walls[WallDirection.Right].FirstOrDefault(x => x.Ring == ring - 1) != default
                        && _walls[WallDirection.Bottom].FirstOrDefault(x => x.Ring == ring - 1) != default;

                case WallDirection.BottomLeft:
                    return _walls[WallDirection.Bottom].FirstOrDefault(x => x.Ring == ring - 1) != default
                        && _walls[WallDirection.Left].FirstOrDefault(x => x.Ring == ring - 1) != default;
            }

            return false;
        }
    }
}
