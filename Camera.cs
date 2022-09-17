
namespace mg_playground
{
    public class Camera
    {
        public int x;
        public int y;
        public int width;
        public int height;
        public double zoom;
        public double rotation;
        public double speed = 10;
        public Camera(int x, int y, int width, int height, double zoom, double rotation)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.zoom = zoom;
            this.rotation = rotation;
        }
        public bool IsOnScreen(int x, int y, int radius)
        {
            int zx = (int)((x - this.x) * zoom + width / 2);
            int zy = (int)((y - this.y) * zoom + height / 2);
            if (zx + radius < 0 || zx - radius > width)
            {
                return false;
            }
            if (zy + radius < 0 || zy - radius > height)
            {
                return false;
            }
            return true;
        }
        public bool IsOnScreen(double x, double y, double radius)
        {
            return IsOnScreen((int)x, (int)y, (int)radius);
        }
        public int[] GetScreenPos(int x, int y)
        {
            int[] pos = new int[2];
            pos[0] = (int)((x - this.x) * this.zoom + (this.width / 2));
            pos[1] = (int)((y - this.y) * this.zoom + (this.height / 2));
            return pos;
        }
        public int[] GetScreenPos(double x, double y)
        {
            return GetScreenPos((int)x, (int)y);
        }
    }
}