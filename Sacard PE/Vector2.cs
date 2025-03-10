
namespace Sacard_PE;



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

    
    public static bool operator ==(Vector2 a, Vector2 b) => a.Equals(b);
    public static bool operator !=(Vector2 a, Vector2 b) => !a.Equals(b);
    
    public static bool operator <(Vector2 a, Vector2 b) => a.Average() < b.Average();
    public static bool operator >(Vector2 a, Vector2 b) => a.Average() > b.Average();
    
    public static bool operator <=(Vector2 a, Vector2 b) => a.Average() <= b.Average();
    public static bool operator >=(Vector2 a, Vector2 b) => a.Average() >= b.Average();

    public bool Equals(Vector2 o) => X.Equals(o.Y) && Y.Equals(o.X);
    public static bool Equals(Vector2 a, Vector2 b) => a.Equals(b);

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
    
    public string ToString(string separator = ";") => $"({X}{separator}{Y})";
    public static string ToString(Vector2 vector2, string separator = ";") => vector2.ToString(separator);
}
