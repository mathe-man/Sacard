using Raylib_cs; //TODO remove this using and use Vec2 and Vec3(for color)

namespace Sacard;

//  Class Env to manage large amount of object and make the calculs
public class Env
{
    public string Name;                 //Name of the env
    public double GravitationalConstant;//G
    public double AirResistance;        //AirResistance is subtracted of the velocity at each update

    
    public List<Object> Objects;
    

    public Env(string name, double gravitationalConstant, double airResistance, List<Object> objects)
    {
        
        Name = name;
        
        GravitationalConstant = gravitationalConstant;
        AirResistance = airResistance;
        Objects = objects == null ? new List<Object>() : objects;
        
        
        //Console.WriteLine($"Environment '{Name}' initialized with G={GravitationalConstant}, AirResistance={AirResistance}, and {Objects.Count} objects");
    }
    
    
    //Update every object in the env and return the actual list 
    public List<Object> Update()
    {
        //Calculate the force and the velocity of each objects
        
        foreach (Object objx in Objects)
        {
            
            objx.Force = Vector2.Zero;
            Vector2 acceleration = Vector2.Zero;
            
            
            foreach (Object objy in Objects)
            {
                if (!objx.Equals(objy))
                {
                    
                    //Local variables
                    double d = Vector2.DistanceDoubleBetween(objx.Position, objy.Position); 
                    double dx = Vector2.DistanceVectorBetween(objx.Position, objy.Position).X;  //X distance
                    double dy = Vector2.DistanceVectorBetween(objx.Position, objy.Position).Y;  //y distance
                    d = !double.IsRealNumber(d) ? 0 : d;
                    dx = !double.IsRealNumber(dx) ? 0 : dx;
                    dy = !double.IsRealNumber(dy) ? 0 : dy;
            
                    //Calculate the force and the force vector
                    double force = GravitationalConstant * objx.Mass * objy.Mass / (d*d);
                
                    Vector2 forceVec = new Vector2(
                        force * (dx / d),
                        force * (dy / d)
                    );
                
                    forceVec.X = !double.IsRealNumber(forceVec.X) ? 0 : forceVec.X;
                    forceVec.Y = !double.IsRealNumber(forceVec.Y) ? 0 : forceVec.Y;
            
                    objx.Force += forceVec;
                    acceleration = objx.Force / objx.Mass;
                    
                    
                }
            }
        
        
            objx.Velocity += acceleration;
            
        }
    
        
        //Update the position of each object
        //It needs to be made after the force calculation to avoid error in synchronisation
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
    
    public Color Color { get; set; }

    //Constructors:
        //Main constructor
    public Object(Vector2 position, double radius,double mass, Vector2 velocity, Vector2 force, Color color)
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
        Color = color;
    }

        //Constructor without Vector2 in parameters
    public Object(double x, double y, double radius, double mass, Vector2 velocity, Vector2 force, Color color)
    {
        if (radius == 0 || mass == 0)
        {
            throw new ArgumentException(
                "Radius and Mass can't be 0, it will cause critical error in Velocity and Force calculations.\nYou can use 1 instead");
        }
        
        Position = new (x, y);
        Radius   = radius;
        Velocity = velocity;
        Force    = force;
        Mass     = mass;
        Color = color;
    }

        //Constructor with a ConstructorDictionary : each parameter needed for the construction is in the dictionnay
    public Object(Dictionary<string, object> constructorDictionary)
    {
        Dictionary<string, object> cd = constructorDictionary; //Give a shorter name
        
        Position = new((double)cd["x"],          (double)cd["y"]);
        Velocity = new((double)cd["velocity_x"], (double)cd["velocity_y"]);
        Force    = new((double)cd["force_x"],    (double)cd["force_y"]);
        
        
        Radius   = (double)cd["radius"];
        Mass     = (double)cd["mass"];
  
        Color    = new Color((int)Convert.ToInt64(cd["color_r"]), (int)Convert.ToInt64(cd["color_g"]), (int)Convert.ToInt64(cd["color_b"]));
        
    }
    
    
    public bool IsCollide(Object other) =>
        Position.DistanceDoubleTo(other.Position) <= other.Radius + Radius;

    public string ToString(string separator = ";")
    {
        string text = $"[position={Position.ToString()}{separator}velocity={Velocity.ToString()}{separator}force={Force.ToString()}{separator}{Mass}]";
        return text;
    }
    public static string ToString(Object a) => a.ToString();

    public Dictionary<string, object> ToConstructorDictionary()
    {
        Dictionary<string, object> constructorDictionary = new Dictionary<string, object>();
        
        constructorDictionary["x"] = Position.X;
        constructorDictionary["y"] = Position.Y;
        constructorDictionary["velocity_x"] =  Velocity.X;
        constructorDictionary["velocity_y"] =  Velocity.Y;
        constructorDictionary["force_x"] = Force.X;
        constructorDictionary["force_y"] = Force.Y;
        
            
        constructorDictionary["radius"] = Radius;
        constructorDictionary["mass"] = Mass;
        constructorDictionary["color_r"] = Convert.ToInt64(Color.R);
        constructorDictionary["color_g"] = Convert.ToInt64(Color.G);
        constructorDictionary["color_b"] = Convert.ToInt64(Color.B);
        return constructorDictionary;
    }
}
