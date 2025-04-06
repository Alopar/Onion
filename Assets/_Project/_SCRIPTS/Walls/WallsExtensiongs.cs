namespace Gameplay
{
    public static class WallsExtensiongs
    {
        public static bool IsCorner(this WallDirection direction) =>
            direction == WallDirection.TopLeft
            || direction == WallDirection.TopRight
            || direction == WallDirection.BottomRight
            || direction == WallDirection.BottomLeft;
    }
}
