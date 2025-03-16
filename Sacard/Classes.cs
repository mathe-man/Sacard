
using System.Data;
using Raylib_cs;
using Newtonsoft.Json;

namespace Sacard;



public class Vector2
{
    public double X { get; set; }
    public double Y { get; set; }
    
    
    
    public static Vector2 Zero = new Vector2(0, 0);
    public static Vector2 One = new Vector2(1, 1);
    public static Vector2 NegativeOne = new Vector2(-1, 1);
    public static Vector2 Up = new Vector2(0, 1);
    public static Vector2 Down = new Vector2(0, -1);
    public static Vector2 Right = new Vector2(1, 0);
    public static Vector2 Left = new Vector2(-1, 0);

    public Vector2(double x, double y)
    {
        X = x;
        Y = y;
    }

    /// <summary> Vector2(double all) instantiate a new Vector2 with X and Y equal to parameters 'all'.</summary>
    public Vector2(double all)
    {
        X = all;
        Y = all;
    }

    public Vector2(System.Numerics.Vector2 systemVector2)
    {
        X = systemVector2.X;
        Y = systemVector2.Y;
    }

    

    public static Vector2 DistanceVectorBetween(Vector2 a, Vector2 b)
    {
        //Calcul the x and y distance and return the vector2 result
        double x = b.X - a.X;
        double y = b.Y - a.Y;
        return new Vector2(x, y);
    }

    public static double DistanceDoubleBetween(Vector2 a, Vector2 b)
    {
        //Calcul the x and y distance and there hypothenus to return the distance in double
        double x = b.X - a.X;
        double y = b.Y - a.Y;
        
        double distance = Math.Sqrt(x * x + y * y);
        
        return distance;
    }

    public Vector2 DistanceVectorTo(Vector2 point)
    {
        double x = point.X - X;
        double y = point.Y - Y;
        return new Vector2(x, y);
    }

    public double DistanceDoubleTo(Vector2 point)
    {
        double x = point.X - X; 
        double y = point.Y - Y;
        
        double distance = Math.Sqrt(x * x + y * y);
        return distance;
    }

    
    //Operators and every math definitions
    public static Vector2 operator +(Vector2 a, Vector2 b) => new (a.X + b.X, a.Y + b.Y);
    public static Vector2 operator +(Vector2 a, double b) => new (a.X + b, a.Y + b);
    public static Vector2 Add(Vector2 a, Vector2 b) => a + b;
    public static Vector2 Add(Vector2 a, double b) => a + b;
    
    public static Vector2 operator -(Vector2 a, Vector2 b) => new (a.X - b.X, a.Y - b.Y);
    public static Vector2 operator -(Vector2 a, double b) => new (a.X - b, a.Y - b);
    public static Vector2 Subtract(Vector2 a, Vector2 b) => a + b;
    public static Vector2 Subtract(Vector2 a, double b) => a + b;

    
    public static Vector2 operator *(Vector2 a, Vector2 b) => new (a.X * b.X, a.Y * b.Y);
    public static Vector2 operator *(Vector2 a, double b) => new (a.X * b, a.Y * b);
    public static Vector2 Multiply(Vector2 a, Vector2 b) => a * b;
    public static Vector2 Multiply(Vector2 a, double b) => a * b;
    
    public static Vector2 operator /(Vector2 a, Vector2 b) => new (a.X / b.X, a.Y / b.Y);
    public static Vector2 operator /(Vector2 a, double b) => new (a.X / b, a.Y / b);
    public static Vector2 Divide(Vector2 a, Vector2 b) => a / b;
    public static Vector2 Divide(Vector2 a, double b) => a / b;

    
    public static bool operator ==(Vector2 a, Vector2 b) => System.Object.Equals(a, b);
    public static bool operator !=(Vector2 a, Vector2 b) => !System.Object.Equals(a, b);
    
    public static bool operator <(Vector2 a, Vector2 b) => a.Average() < b.Average();
    public static bool operator >(Vector2 a, Vector2 b) => a.Average() > b.Average();
    
    public static bool operator <=(Vector2 a, Vector2 b) => a.Average() <= b.Average();
    public static bool operator >=(Vector2 a, Vector2 b) => a.Average() >= b.Average();
    

    public bool Equal(Vector2 o) => X.Equals(o.Y) && Y.Equals(o.X);
    public static bool Equals(Vector2 a, Vector2 b) => a.Equal(b);

    // The hash code is thr hash code of the average of the vector.
    public override int GetHashCode() => this.Average().GetHashCode();
    public static int GetHashCode(Vector2 vector) => vector.GetHashCode();
    
    public double Average() => (X + Y) / 2;
    public static double Average(Vector2 a) => a.Average();
    public static Vector2 Average(Vector2 a, Vector2 b) => (a + b) / 2;

    public double Length() => this.DistanceDoubleTo(Vector2.Zero);
    public static double Length(Vector2 vector2) => vector2.Length();
    public Vector2 Opposite() => new Vector2(X, Y) * -1;
    public static Vector2 Opposite(Vector2 a) => a.Opposite();
    
    public Vector2 Abs() => new Vector2(Math.Abs(X), Math.Abs(Y));
    public static Vector2 Abs(Vector2 a) => a.Abs();

    //Return the smallest of X and Y
    public double Min() => Math.Min(X, Y);
    public static double Min(Vector2 a) => a.Min();
    //Return the largest of X and Y
    public double Max() => Math.Max(X, Y);
    public static double Max(Vector2 a) => a.Max();
    
    
    //Return the vector with the smallest average
    public static Vector2 Min(Vector2 a, Vector2 b) => a < b  ? a : b;
    //Return the vector with the largest average
    public static Vector2 Max(Vector2 a, Vector2 b) => a > b  ? a : b;

    //Clamp X, Y, and finally X and Y
        // X
    public Vector2 ClampXmin(double min) => new (X < min ? min : X, Y);
    public Vector2 ClampXmax(double max) => new (X > max ? max : X, Y);
    public Vector2 ClampX(double min, double max) => new Vector2(X, Y).ClampXmin(min).ClampXmax(max);
    
        // Y
    public Vector2 ClampYmin(double min) => new (X, Y < min ? min : Y);
    public Vector2 ClampYmax(double max) => new (X, Y > max ? max : Y);
    public Vector2 ClampY(double min, double max) => new Vector2(X, Y).ClampYmin(min).ClampYmax(max);
        
        // X and Y
    public Vector2 ClampMin(double min) => new Vector2(X, Y).ClampXmin(min).ClampYmin(min);
    public Vector2 ClampMax(double max) => new Vector2(X, Y).ClampXmax(max).ClampYmax(max);
    public Vector2 Clamp(double min, double max) => new Vector2(X, Y).ClampMin(min).ClampMax(max);

    public Vector2 Normalized(double max) => this / max;
    public Vector2 Normalized(Vector2 vector2, double max) => this / max;
    
    
    //Conversion methods
    public string ToString(string separator = ";") => $"({X}{separator}{Y})";
    public static string ToString(Vector2 vector2, string separator = ";") => vector2.ToString(separator);
    
    public System.Numerics.Vector2 ToSystemVector2()  => new System.Numerics.Vector2(Convert.ToSingle(X), Convert.ToSingle(Y));
    public static System.Numerics.Vector2 ToSystemVector2(Vector2 sacardVector2)  => sacardVector2.ToSystemVector2();
    
    public static Vector2 FromSystemVector2(System.Numerics.Vector2 systemVector2) => new (systemVector2);
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



public class JsonFiles
{
    //Basic json de/serialisation and load/save
    public static T LoadFromFile<T>(string fileName)
    {
        // Read the file content to a variable
        string jsonString = File.ReadAllText("Sacard Files/" + fileName);
        // Deserialize the content
        T? value = JsonConvert.DeserializeObject<T>(jsonString);
        if (Equals(value, null))
        {
            throw new NoNullAllowedException($"{fileName} deserialization result is null (result:{value})");
        }

        return value;
    }
    public static void SaveToFile<T>(T content, string filePath)
    {
        try
        {
            if (File.Exists("Sacard Files/" + filePath))  //Only appen when the file is already existent
            {
                // Serialize the content into Json format
                string jsonString = JsonConvert.SerializeObject(content, Formatting.Indented);
                // Write the json formated content in the specified file
                File.WriteAllText("Sacard Files/" + filePath, jsonString);
            }
            else                        // if the file doesn't exist, it will be created and the function are recalled
            {
                Console.WriteLine($"File {filePath} don't exist");
                FileStream fs = File.Create("Sacard Files/" + filePath);
                fs.Close();
                SaveToFile(content, filePath);
            }
        }
        catch (DirectoryNotFoundException)
        {
            if (!Directory.Exists("Sacard Files"))
            {
                Directory.CreateDirectory("Sacard Files");
                SaveToFile(content, filePath);
            }
            else
            {
                Directory.CreateDirectory("Sacard Files/" + filePath.Remove(filePath.LastIndexOf("/")));
                SaveToFile(content, filePath);
            }

        }
    }
    
    
    public static List<Object> LoadObjectsFromFile(string? filePath = null)
    {
        if (Equals(filePath, null))   //Ask the file path if it is not gived in parameters
        {Console.WriteLine("Enter the json file contening your objects:\n"); filePath = Console.ReadLine();}

        if (Equals(filePath, null))     //throw a new exception if it still null
        { throw new FileNotFoundException($"{filePath} is null(got: {filePath}.");}
        
        List<Dictionary<string, object>>? construtors =  LoadFromFile<List<Dictionary<string, object>>>(filePath);

        if (Equals(construtors, null))
        { throw new NullReferenceException($"Constructors in {filePath} are nulls");}
        
        List<Object> objects = new List<Object>();
        foreach (var dic in construtors)
        {
            objects.Add(new Object(dic));
        }
        
        return objects;
    }

    public static void SaveObjectsToFile(List<Object> objectList, string? filePath = null)
    {
        if (filePath == null)   //Ask the file path if it is not gived in parameters
        {Console.WriteLine("Enter the file directory to save objects:\n"); filePath = Console.ReadLine();}

        if (filePath == null)
        { throw new NullReferenceException("Invalid file path");}
        
        List<Dictionary<string, object>> construtors = new();

        foreach (var obj in objectList)
        {
            construtors.Add(obj.ToConstructorDictionary());
        }
        
        SaveToFile(construtors, filePath);
    }

    public static void SaveEnvToFile(Env env, string? filePath = null)
    {
        if (filePath == null)   //Ask the file path if it is not gived in parameters
        {Console.WriteLine("Enter the save file directory to save environment:\n"); filePath = Console.ReadLine();}

        if (filePath == null)
        { throw new NullReferenceException("Invalid file path");}
        
        Dictionary<string, object> envConstructor = new ();
        envConstructor["name"] = env.Name;
        envConstructor["GravitationalConstant"] = env.GravitationalConstant;
        envConstructor["AirResistance"] = env.AirResistance;
        
        
        SaveToFile(envConstructor, filePath);
    }

    public static Env LoadEnvFromFile(string? filePath = null)
    {
        if (filePath == null)   //Ask the file path if it is not gived in parameters
        {Console.WriteLine("Enter the json file contening your environment:\n"); filePath = Console.ReadLine();}

        if (filePath == null)
        { throw new NullReferenceException("Invalid file path");}
        
        Dictionary<string, object>? envConstructor = LoadFromFile<Dictionary<string, object>>(filePath);
        
        
        if (Equals(envConstructor, null))
        { throw new NullReferenceException($"Constructors in {filePath} are nulls");}
        
        Console.WriteLine("Environment information's");
        foreach (var keyPair in envConstructor)
        {
            Console.WriteLine(keyPair.Key + " = " + keyPair.Value);
        }
        
        Env env = new((string)envConstructor["name"], (double)envConstructor["GravitationalConstant"],
            (double)envConstructor["AirResistance"], new ());
        return env;
    }
    
}
