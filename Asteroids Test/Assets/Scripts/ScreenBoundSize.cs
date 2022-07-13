using UnityEngine;

namespace DefaultNamespace
{
    public static class ScreenBoundSize
    {
        public static Vector2 Size => GetScreenSize();

        public static Vector2 HalfSize => GetHalfScreenSize();
        
        private static Camera _mainCamera => Camera.main;
        
        private static Vector2 GetScreenSize()
        {
            return GetHalfScreenSize() * 2f;
        }

        private static Vector2 GetHalfScreenSize()
        {
            float halfHeightScreenSize = _mainCamera.orthographicSize;
            float halfWidthScreenSize = halfHeightScreenSize * _mainCamera.aspect;

            return new Vector2(halfWidthScreenSize, halfHeightScreenSize);
        }
        
    }
}