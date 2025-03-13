using System;
using Raylib_cs;

namespace Sacard;

// The Debug/Use entry point
class Program
{
    //first Triangle shape points list
    public static List<Object> XList =
    [
        new(new(3, 4), 1, 100, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(-3, 4), 1, 100, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(0, -4), 1, 100, Vector2.Zero, Vector2.Zero, Color.Blue)
    ];
    
    //second
    public static List<Object> YList =
    [
        new(new(5, 6), 1, 100, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(-5, 6), 1, 100, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(0, -6), 1, 100, Vector2.Zero, Vector2.Zero, Color.Blue)
    ];

    //Line
    public static List<Object> ZList =
    [
        new(new(200, 0), 50, 100000000, Vector2.Zero, Vector2.Zero, Color.Red),
        new(new(-100, 0), 40, 10000, Vector2.Zero, Vector2.Zero, Color.Blue),
    ];
    //Orbital try
    public static List<Object> AList =
    [
        new(new(7, 5), 2, 10, new (0, 0.05), Vector2.Zero, Color.Blue),
        new(new(-7, 5), 2, 10, new(0, -0.05), Vector2.Zero, Color.Blue),
    ];

    public static Env simEnv = new Env("aa", 6.67E-5, 0, ZList);

    public static void Main()
    {
        //DebugGravitation();
        Sacard_Interface();
    }


    public static void DebugGravitation()
    {
        
        foreach (Object obj in simEnv.Update())
        {
            Console.WriteLine(obj.ToString());
            Console.WriteLine(simEnv.Objects.IndexOf(obj));
        }
        //Console.ReadKey();
        
    }
    
    public static void Sacard_Interface()
    {
        SacardDrawer.Init(720, 500, 60, "Drawer", true);
        SacardDrawer.FirstLoopAction = LoopAction;
        SacardDrawer.Start();
        
        Console.WriteLine("--Closed--");
    }
    public static void LoopAction()
    {

        SacardDrawer.objects.Clear();
        SacardDrawer.objects = simEnv.Update();
        
        SacardDrawer.objects.Add(new (Vector2.Zero, 2, 1, Vector2.Zero, Vector2.Zero, Color.Black));
        

        //Thread.Sleep(10);
    }
}