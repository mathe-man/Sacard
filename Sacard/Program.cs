using System;
using Raylib_cs;

namespace Sacard;

// The Debug/Use entry point
class Program
{
    //first Triangle shape points list
    public static List<Object> ListX =
    [
        new(new(150, 200), 15, 100000, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(-150, 200), 15, 100000, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(0, -200), 15, 100000, Vector2.Zero, Vector2.Zero, Color.Blue)
    ];
    
    //second
    public static List<Object> ListY =
    [
        new(new(250, 300), 1, 10000, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(-250, 300), 1, 10000, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(0, -300), 1, 100000, Vector2.Zero, Vector2.Zero, Color.Blue)
    ];

    //Line
    public static List<Object> ListZ =
    [
        new(new(200, 0), 10, 1E8, new (0, 0.1), Vector2.Zero, Color.Green),
        new(new(-100, 0), 20, 1E12, Vector2.Zero, Vector2.Zero, Color.Red),
    ];
    //Dual Orbital
    public static List<Object> ListA =
    [
        new(new(100, 5), 18, 1E12, new (0, 0.4), Vector2.Zero, Color.Violet),
        new(new(-100, 5), 18, 1E12, new(0, -0.4), Vector2.Zero, Color.Magenta),
    ];
    //Orbital Earth/Moon
    public static List<Object> Earth =
    [
        //Earth - 1km = X * 1000000 / 2000
        new(new(0, 0), 30, 5.9724E24, new(0, 0), new(0, 0), Color.Blue),
        new(new (0.4055E6, 0), 10, 0.07346E24, new(0, 1.082), new(0, 0), Color.DarkGray)
    ];

    public static Env simEnv;
    public static void Main()
    {
        JsonFiles.SaveObjectsToFile(ListX, "ListX.objs");
        JsonFiles.SaveObjectsToFile(ListY, "ListY.objs");
        JsonFiles.SaveObjectsToFile(ListZ, "ListZ.objs");
        JsonFiles.SaveObjectsToFile(ListA, "ListA.objs");
        JsonFiles.SaveObjectsToFile(Earth, "Earth.objs");
        
        Console.Clear();
        Console.WriteLine("Sacard Physic Engine\n");

        simEnv = JsonFiles.LoadEnvFromFile("Default.json");
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
        
        SacardDrawer.FirstLoopAction.Add(LoopAction);
        SacardDrawer.WindowCloseAction.Add(() => { Console.WriteLine("---Closed---"); });
        
        SacardDrawer.Start();
    }
    public static void LoopAction()
    {
        //Console.WriteLine("First action" + SacardDrawer.FirstLoopActio);
        SacardDrawer.objects.Clear();

        foreach (Object obj in simEnv.Update())
        {
            SacardDrawer.objects.Add(obj);
            //SacardDrawer.objects.Add(new (obj.Position / 1000000, obj.Radius, obj.Mass, obj.Velocity, obj.Force, obj.Color));
        }
        

        //Thread.Sleep(10);
    }


}

