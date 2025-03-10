using System.Security.AccessControl;
using Vector2 = Sacard_PE.Vector2;

namespace Sacard_PE;

//  Class Env to manage large amount of object and make the calculs
public class Env
{
    public string Name;                 //Name of the env
    public double GravitationalConstant;//G
    public double AirResistance;        //AirResistance is subtracted of the velocity at each update
    
    public List<Object> Objects;

    public Env(string name, double G, double airResistance, List<Object> objects)
    {
        
        Name = name;
        
        GravitationalConstant = G;
        AirResistance = airResistance;
        Objects = objects == null ? new List<Object>() : objects;
        
        Console.WriteLine($"Environment '{Name}' initialized with g={GravitationalConstant}, AirResistance={AirResistance} and {Objects.Count} objects");
    }
    
    
    //Update every object in the env and return the actual list 
    public List<Object> Update(Action<Object> debugAction = null)
    {
        //Calculate the force and the velocity of each objects
        foreach (Object objx in Objects)
        {
            double Force;   //Force in N
            Vector2 ForceVec = Vector2.Zero;
            
            
            objx.Force = Vector2.Zero;
            foreach (Object objy in Objects)
            {
                if (objy != objx)
                {
                    double r = Vector2.DistanceDoubleBetween(objx.Position, objy.Position);
                    // Using the universal law of gravitation
                    Force = GravitationalConstant * (objx.Mass * objy.Mass) / r*r;
                    ForceVec.X = Force * (objx.Position.X / objy.Position.X) / r;
                    ForceVec.Y = Force * (objx.Position.Y / objy.Position.Y) / r;
                    
                    objx.Force += ForceVec;
                }

            }
            objx.Force /= Objects.Count - 1;
            
            
            objx.Velocity.X = ForceVec.X/objx.Mass;
            objx.Velocity.Y = ForceVec.Y/objx.Mass;
            
            if(debugAction != null){debugAction.Invoke(objx);}
        }

        //Update the position of each object
        //It need to be make after the force calculation to avoid error in synchronisation
        foreach (Object obj in Objects)
        {
            obj.Position += obj.Velocity;
        }
        
        return Objects;
    }
}

public class Object
{
    // This use metrics system and I highly recommend to use meters(m) and kg or other proportionnal unit (like cm with g)
    public Vector2 Position { get; set; }
    public double Radius { get; set; }
    public double Mass { get; set; }
    public Vector2 Velocity { get; set; }
    //The force applied to the object in N
    public Vector2 Force { get; set; }

    public Object(Vector2 position, double radius,double mass, Vector2 velocity, Vector2 force)
    {
        if (radius == 0 || mass == 0)
        {
            throw new ArgumentException(
                $"Radius and Mass can't be 0, it will cause critical error in Velocity and Force calculations.\nYou can use 1 instead");
        }
        
        Position = position;
        Radius   = radius;
        Velocity = velocity;
        Force    = force;
        Mass     = mass;
    }

    public bool IsCollide(Vector2 otherPosition, double otherRadius) =>
        Position.DistanceDoubleTo(otherPosition) <= otherRadius + Radius;

    public string ToString(string separator = ";")
    {
        string text = $"[position={Position.ToString()}{separator}velocity={Velocity.ToString()}{separator}force={Force.ToString()}{separator}{Mass}]";
        return text;
    }
    public static string ToString(Object a) => a.ToString();
}
