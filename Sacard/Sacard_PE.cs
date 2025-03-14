
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
        
        Console.WriteLine($"Environment '{Name}' initialized with g={GravitationalConstant}, AirResistance={AirResistance} and {Objects.Count} objects");
    }
    
    
    //Update every object in the env and return the actual list 
    public List<Object> Update(Action<Object> debugAction = null)
    {
        //Calculate the force and the velocity of each objects
        
        List<int> toRemove = new List<int>();
        foreach (Object objx in Objects)
        {

            if (!toRemove.Contains(Objects.IndexOf(objx)))
            {
                objx.Force = Vector2.Zero;
                Vector2 acceleration = Vector2.Zero;
                
                
                foreach (Object objy in Objects)
                {
                    if (!objx.Equals(objy))
                    {
                        if (objx.IsCollide(objy))
                        {
                            Console.WriteLine("Collision");
                            if (objx.Mass > objy.Mass)
                            {
                                objx.Mass += objy.Mass;
                                acceleration = (objy.Velocity - objx.Velocity) / objx.Mass;
                                objx.Radius += Math.Sqrt(objy.Radius);
                                //objx.Position += objx.Position.DistanceVectorTo(objy.Position)/2; 
                                toRemove.Add(Objects.IndexOf(objy));
                            }
                        }
                        else
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
                }
            
            
            objx.Velocity += acceleration;
            }
            
        }
    
        foreach (int index in toRemove)
        {Objects.RemoveAt(index);}
        
        //Update the position of each object
        //It needs to be made after the force calculation to avoid error in synchronisation
        foreach (Object obj in Objects)
        {
            obj.Position += obj.Velocity;
        }
        
        return Objects;
    }
}

