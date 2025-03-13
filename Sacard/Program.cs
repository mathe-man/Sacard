using System;
using Raylib_cs;

namespace Sacard;

// The Debug/Use entry point
class Program
{
    //first Triangle shape points list
    public static List<Object> XList =
    [
        new(new(150, 200), 15, 10000, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(-150, 200), 15, 10000, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(0, -200), 15, 10000, Vector2.Zero, Vector2.Zero, Color.Blue)
    ];
    
    //second
    public static List<Object> YList =
    [
        new(new(250, 300), 1, 10000, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(-250, 300), 1, 10000, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(0, -300), 1, 100000, Vector2.Zero, Vector2.Zero, Color.Blue)
    ];

    //Line
    public static List<Object> ZList =
    [
        new(new(200, 0), 10, 10000, new (0, 0.1), Vector2.Zero, Color.Green),
        new(new(-100, 0), 20, 100000, Vector2.Zero, Vector2.Zero, Color.Red),
    ];
    //Orbital
    public static List<Object> AList =
    [
        new(new(100, 5), 18, 100000, new (0, 0.1), Vector2.Zero, Color.Violet),
        new(new(-100, 5), 18, 100000, new(0, -0.1), Vector2.Zero, Color.Magenta),
    ];

    public static Env simEnv = new Env("Env", 6.67E-5, 0, GetObjectsFromFile());

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
        }
        

        //Thread.Sleep(10);
    }


    public static List<Object> GetObjectsFromFile()
    {
        Console.Clear();
        Console.WriteLine("Sacard Physic Engine\nEnter the json file contening your objects:\n");
        return JsonFiles.LoadFromFile<List<Object>>(Console.ReadLine());
    }
}

