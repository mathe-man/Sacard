

using Sacard_Drawer;
using Sacard_Utilities;
using Sacard_PE;
using Object = Sacard_PE.Object;

namespace Sacard;


// The Debug/Use entry point
class Program
{
    public static List<Object> XList =
    [
        new(new(3, 4), 1, 100, Vector2.Zero, Vector2.Zero),
        new(new(-3, 4), 1, 100, Vector2.Zero, Vector2.Zero),
        new(new(0, -4), 1, 100, Vector2.Zero, Vector2.Zero)
    ];

    public static List<Object> YList =
    [
        new(new(5, 6), 1, 100, Vector2.Zero, Vector2.Zero),
        new(new(-5, 6), 1, 100, Vector2.Zero, Vector2.Zero),
        new(new(0, -6), 1, 100, Vector2.Zero, Vector2.Zero)
    ];

    public static List<Object> ZList =
    [
        new(new(5, 0), 1, 10, Vector2.Zero, Vector2.Zero),
        new(new(-5, 0), 1, 10, Vector2.Zero, Vector2.Zero),
    ];

    public static Env simEnv = new Env("aa", 6.67E-11, 0, ZList);
    public static SacardDrawer drawer = new SacardDrawer(720, 520, 60, "Drawer");

    public static void Main()
    {
        DebugGravitation();
        //Sacard_Interface();
    }


    public static void DebugGravitation()
    {
        foreach (Object obj in simEnv.Update())
        {
            Console.WriteLine(obj.ToString());
            Console.WriteLine();
        }
    }
    
    public static void Sacard_Interface()
    {
        drawer.FirstLoopAction = LoopAction;
        drawer.Start();
        Console.WriteLine("--Closed--");
    }
    public static void LoopAction()
    {
        List<Object> objects = simEnv.Update();

        drawer.Circles.Clear();
        foreach (Object obj in objects)
        {
            drawer.Circles.Add(new(obj.Position, obj.Radius, 150, 150, 150));
        }
        drawer.Circles.Add(new (Vector2.Zero, 150, 150, 150, 150));
    }
}