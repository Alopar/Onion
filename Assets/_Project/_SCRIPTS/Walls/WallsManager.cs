using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class WallsManager : MonoBehaviour
    {
        [SerializeField, Required] private ProtectWall _protectWallPrefab;
        [SerializeField, Required] private AttackWall _attackWallPrefab;
        [SerializeField, Required] private ProduceWall _produceWallPrefab;
        [SerializeField, Required] private WallPreview _wallPreviewPrefab;
        [SerializeField, Required] private Transform _wallsContainer;

        [SerializeField] private float _offsetFromCenter;
        [SerializeField] private float _offsetBetweenWalls;
        [SerializeField] private float _defaultWallThickness;
        [SerializeField] private int _wallSegments;
        [SerializeField] private float _wallAngle;
        [SerializeField] private int _colliderSegments;

        private Dictionary<WallDirection, List<Wall>> _walls = new();
        private Dictionary<WallDirection, WallPreview> _previews = new();

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
            float nextWallDistance = GetNextWallDistance(direction);
            wall.transform.eulerAngles = WallsSettings.Instance.GetWallAngle(direction);
            wall.SetDirection(direction);
            wall.Draw(_wallSegments, _wallAngle, nextWallDistance);
            wall.UpdateCollider(_colliderSegments);

            _walls[direction].Add(wall);
            UpdateCreationPreviews();
            return (T)wall;
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
            
            float nextWallDistance = GetNextWallDistance(direction);
            preview.transform.eulerAngles = WallsSettings.Instance.GetWallAngle(direction);
            preview.SetDirection(direction);
            preview.Draw(_wallSegments, _wallAngle, nextWallDistance);
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

        private float GetNextWallDistance(WallDirection direction)
        {
            bool isCorner = direction.IsCorner();
            float offsetsBetween;
            float wallsThickness;
            if (_walls[direction].Count == 0 && isCorner)
            {
                offsetsBetween = _offsetBetweenWalls;
                wallsThickness = _defaultWallThickness;
            }
            else
            {
                offsetsBetween = _walls[direction].Count * _offsetBetweenWalls * 2;
                wallsThickness = _defaultWallThickness * _walls[direction].Count * 2;
                if (isCorner)
                {
                    offsetsBetween += _offsetBetweenWalls;
                    wallsThickness += _defaultWallThickness;
                }
            }
            return _offsetFromCenter + offsetsBetween + wallsThickness;
        }

        private bool CanPlace(WallDirection direction)
        {
            switch (direction)
            {
                case WallDirection.Top:
                    if (_walls[direction].Count == 0)
                        return true;

                    return _walls[WallDirection.TopLeft].Count == _walls[WallDirection.TopRight].Count 
                        && _walls[direction].Count == _walls[WallDirection.TopRight].Count;

                case WallDirection.Right:
                    if (_walls[direction].Count == 0)
                        return true;

                    return _walls[WallDirection.TopRight].Count == _walls[WallDirection.BottomRight].Count
                        && _walls[direction].Count == _walls[WallDirection.BottomRight].Count;

                case WallDirection.Bottom:
                    if (_walls[direction].Count == 0)
                        return true;

                    return _walls[WallDirection.BottomRight].Count == _walls[WallDirection.BottomLeft].Count
                        && _walls[direction].Count == _walls[WallDirection.BottomLeft].Count;

                case WallDirection.Left:
                    if (_walls[direction].Count == 0)
                        return true;

                    return _walls[WallDirection.BottomLeft].Count == _walls[WallDirection.TopLeft].Count
                        && _walls[direction].Count == _walls[WallDirection.TopLeft].Count;

                case WallDirection.TopLeft:
                    return _walls[WallDirection.Left].Count == _walls[WallDirection.Top].Count
                        && _walls[direction].Count < _walls[WallDirection.Top].Count;

                case WallDirection.TopRight:
                    return _walls[WallDirection.Top].Count == _walls[WallDirection.Right].Count
                        && _walls[direction].Count < _walls[WallDirection.Right].Count;

                case WallDirection.BottomRight:
                    return _walls[WallDirection.Right].Count == _walls[WallDirection.Bottom].Count
                        && _walls[direction].Count < _walls[WallDirection.Bottom].Count;

                case WallDirection.BottomLeft:
                    return _walls[WallDirection.Bottom].Count == _walls[WallDirection.Left].Count
                        && _walls[direction].Count < _walls[WallDirection.Left].Count;
            }

            return false;
        }
    }
}
