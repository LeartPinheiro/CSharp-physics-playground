using System;

namespace mg_playground
{
    public class Entitily
    {
        public double x;
        public double y;
        public double mass;

        public double density = 1;
        public double x_vel = 0;
        public double y_vel = 0;
        public double velocity
        {
            get
            {
                return Math.Sqrt((x_vel * x_vel + y_vel * y_vel));
            }
        }
        public double theta
        {
            get
            {
                return Math.Atan2(y_vel, x_vel);
            }
        }
        public double radius
        {
            get
            {
                return Math.Sqrt((mass / density)) * 15;
                //return Math.Cbrt(3 * (mass / density) / (4 * Math.PI));
            }
        }
        public Entitily(double x, double y, double mass)
        {
            this.x = x;
            this.y = y;
            this.mass = mass;
        }
    }
}