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
        new(new(200, 0), 10, 10000, new (0, 0.1), Vector2.Zero, Color.Green),
        new(new(-100, 0), 20, 100000, Vector2.Zero, Vector2.Zero, Color.Red),
    ];
    //Orbital
    public static List<Object> ListA =
    [
        new(new(100, 5), 18, 1E13, new (0, 0.1), Vector2.Zero, Color.Violet),
        new(new(-100, 5), 18, 1E13, new(0, -0.1), Vector2.Zero, Color.Magenta),
    ];

    public static Env simEnv = new Env("Env", 6.67E-11, 0, ListA);//LoadObjectsFromFile("ListA.json")

    public static void Main()
    {
        SaveObjectsToFile(ListX, "ListX.json");
        SaveObjectsToFile(ListY, "ListY.json");
        SaveObjectsToFile(ListZ, "ListZ.json");
        SaveObjectsToFile(ListA, "ListA.json");
        
        
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


    public static List<Object> LoadObjectsFromFile(string? path = null)
    {
        Console.Clear();
        Console.WriteLine("Sacard Physic Engine\n");
        
        if (path == null)   //Ask the file path if it is not gived in parameters
        {Console.WriteLine("Enter the json file contening your objects:\n"); path = Console.ReadLine();}
        
        List<Dictionary<string, object>> construtors = JsonFiles.LoadFromFile<List<Dictionary<string, object>>>(path);

        List<Object> objects = new List<Object>();
        foreach (var dic in construtors)
        {
            objects.Add(new Object(dic));
        }
        
        return objects;
    }

    public static void SaveObjectsToFile(List<Object> objectList, string path)
    {
        List<Dictionary<string, object>> construtors = new();

        foreach (var obj in objectList)
        {
            construtors.Add(obj.ToConstructorDictionary());
        }
        
        JsonFiles.SaveToFile(construtors, path);
    }
}

