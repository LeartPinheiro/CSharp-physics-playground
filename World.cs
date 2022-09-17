using System;
using System.Collections.Generic;

namespace mg_playground

{
    public class World
    {
        public double gravity{get;}
        public List<Entitily> entities = new List<Entitily>();
        public World(double gravity)
        {
            this.gravity = gravity;
        }
        public void add_entity(Entitily entity)
        {
            entities.Add(entity);
        }
        public void update(double dt)
        {
            update_velocities(dt);
            update_positions();

        }
        private void update_velocities(double dt)
        {
            foreach (Entitily entity1 in entities)
            {
               foreach (Entitily entity2 in entities)
               {
                     if (entity1 != entity2)
                     {
                        double dx = entity2.x - entity1.x;
                        double dy = entity2.y - entity1.y;
                        double dist = Math.Sqrt((dx * dx + dy * dy));
                        double force = (gravity * entity1.mass * entity2.mass) / (dist * dist);
                        double theta = Math.Atan2(dy,dx);
                        entity1.x_vel += force * Math.Cos(theta) * dt;
                        entity1.y_vel += force * Math.Sin(theta) * dt;
                     }
               }
            }
        }
        private void update_positions()
        {
            foreach (Entitily ent1 in entities)
            {
                ent1.x += ent1.x_vel;
                ent1.y += ent1.y_vel;
                foreach(Entitily ent2 in entities){
                    if (ent1 == ent2)
                    {
                        continue;
                    }
                    check_collisions(ent1, ent2);
                }
            }
        }
        private void check_collisions(Entitily ent1, Entitily ent2)
        {
            double dx = ent2.x - ent1.x;
            double dy = ent2.y - ent1.y;
            double dist = Math.Sqrt((dx * dx + dy * dy));
            if( dist < (ent1.radius + ent2.radius)){
                double phi = Math.Atan2(dy,dx);
                ent1.x -= Math.Cos(phi) * (ent1.radius + ent2.radius - dist);
                ent1.y -= Math.Sin(phi) * (ent1.radius + ent2.radius - dist);
                resolve_collision(ent1, ent2);
            }
        }

        private void resolve_collision(Entitily ent1, Entitily ent2)
        {
            double t1 = ent1.theta;
            double t2 = ent2.theta;
            double m1 = ent1.mass;
            double m2 = ent2.mass;
            double v1 = ent1.velocity;
            double v2 = ent2.velocity;
            double phi = Math.Atan2(ent2.y - ent1.y, ent2.x - ent1.x);
            double vx1 = (v1*Math.Cos(t1-phi)*(m1-m2)+2*m2*v2*Math.Cos(t2-phi))/(m1+m2)*Math.Cos(phi)+v1*Math.Sin(t1-phi)*Math.Cos(phi+Math.PI/2);
            double vy1 = (v1*Math.Cos(t1-phi)*(m1-m2)+2*m2*v2*Math.Cos(t2-phi))/(m1+m2)*Math.Sin(phi)+v1*Math.Sin(t1-phi)*Math.Sin(phi+Math.PI/2);
            double vx2 = (v2*Math.Cos(t2-phi)*(m2-m1)+2*m1*v1*Math.Cos(t1-phi))/(m1+m2)*Math.Cos(phi)+v2*Math.Sin(t2-phi)*Math.Cos(phi+Math.PI/2);
            double vy2 = (v2*Math.Cos(t2-phi)*(m2-m1)+2*m1*v1*Math.Cos(t1-phi))/(m1+m2)*Math.Sin(phi)+v2*Math.Sin(t2-phi)*Math.Sin(phi+Math.PI/2);
            ent1.x_vel = vx1;
            ent1.y_vel = vy1;
            ent2.x_vel = vx2;
            ent2.y_vel = vy2;
        }

    }
}