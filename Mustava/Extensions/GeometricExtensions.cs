using System.Drawing;

namespace Mustava.Extensions
{
    public static class GeometricExtensions
    {
        public static bool IsPointTouching(this Rectangle rectangle, Point point)
        {
            return point.IsTouching(rectangle.Left, rectangle.Right, rectangle.Top, rectangle.Bottom);
        }

        public static bool IsTouching(this Point point, int left, int right, int top, int bottom)
        {
            return IsTouchingF(point, left, right, top, bottom);
        }

        public static bool IsTouchingF(this Point point, float left, float right, float top, float bottom)
        {
            return point.X >= left & point.X <= right & point.Y >= top & point.Y <= bottom;
        }
    }
}