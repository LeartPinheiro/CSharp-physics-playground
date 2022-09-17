using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mg_playground
{
    public class Entitily
    {
        public double x;
        public double y;
        public double mass;
        public double x_vel = 0;
        public double y_vel = 0;
        public double velocity{
            get{
                return Math.Sqrt((x_vel * x_vel + y_vel * y_vel));
            }
        }
        public double theta{
            get{
                return Math.Atan2(y_vel,x_vel);
            }
        }
        public double radius{
            get{
                return mass * 10;
                //return Math.Sqrt((mass / Math.PI));
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