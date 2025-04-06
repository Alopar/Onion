using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class WallsSettings
    {
        private static WallsSettings _instance;

        private Dictionary<WallDirection, Vector3> _directions;
        private Dictionary<WallDirection, Vector3> _angles;

        public static WallsSettings Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = new WallsSettings();
                return _instance;
            }
        }

        public WallsSettings() =>
            Initialize();

        public Vector3 GetDirectionVector(WallDirection wallPosition) =>
            _directions[wallPosition];

        public Vector3 GetWallAngle(WallDirection wallPosition) =>
            _angles[wallPosition];

        private void Initialize()
        {
            InitDirections();
            InitAngles();
        }

        private void InitDirections()
        {
            _directions = new Dictionary<WallDirection, Vector3>();
            _directions.Add(WallDirection.TopLeft, new Vector3(-1, 1));
            _directions.Add(WallDirection.Top, new Vector3(0, 1));
            _directions.Add(WallDirection.TopRight, new Vector3(1, 1));
            _directions.Add(WallDirection.Right, new Vector3(1, 0));
            _directions.Add(WallDirection.BottomRight, new Vector3(1, -1));
            _directions.Add(WallDirection.Bottom, new Vector3(0, -1));
            _directions.Add(WallDirection.BottomLeft, new Vector3(-1, -1));
            _directions.Add(WallDirection.Left, new Vector3(-1, 0));
        }

        private void InitAngles()
        {
            _angles = new Dictionary<WallDirection, Vector3>();
            _angles.Add(WallDirection.TopLeft, new Vector3(0, 0, 90));
            _angles.Add(WallDirection.Top, new Vector3(0, 0, 45));
            _angles.Add(WallDirection.TopRight, new Vector3(0, 0, 0));
            _angles.Add(WallDirection.Right, new Vector3(0, 0, -45));
            _angles.Add(WallDirection.BottomRight, new Vector3(0, 0, -90));
            _angles.Add(WallDirection.Bottom, new Vector3(0, 0, -135));
            _angles.Add(WallDirection.BottomLeft, new Vector3(0, 0, -180));
            _angles.Add(WallDirection.Left, new Vector3(0, 0, 135));
        }
    }
}
