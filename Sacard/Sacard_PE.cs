
namespace Sacard;

//  Class Env to manage large amount of object and make the calculs
public class Env
{
    public string Name;                 //Name of the env
    public double GravitationalConstant;//G
    public double AirResistance;        //AirResistance is subtracted of the velocity at each update

    
    public List<Object> Objects;
    

    public Env(string name, double gravitationalConstant, double airResistance, List<Object> objects, bool collision = false)
    {
        
        Name = name;
        
        GravitationalConstant = gravitationalConstant;
        AirResistance = airResistance;
        Objects = objects == null ? new List<Object>() : objects;
        
        
        Console.WriteLine($"Environment '{Name}' initialized with G={GravitationalConstant}, AirResistance={AirResistance}, and {Objects.Count} objects");
    }
    
    
    //Update every object in the env and return the actual list 
    public List<Object> Update(Action<Object> debugAction = null)
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

