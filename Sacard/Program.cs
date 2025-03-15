
using System.Diagnostics;
using System.Runtime.InteropServices;

using Raylib_cs;


namespace Sacard;

// The Debug/Use entry point
class Program
{
    //first Triangle shape points list
    private static readonly List<Object> ListX =
    [
        new(new(150, 200), 15, 1E12, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(-150, 200), 15, 1E12, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(0, -200), 15, 1E12, Vector2.Zero, Vector2.Zero, Color.Blue)
    ];
    //second
    private static readonly List<Object> ListY =
    [
        new(new(250, 300), 1, 10000, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(-250, 300), 1, 10000, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(0, -300), 1, 100000, Vector2.Zero, Vector2.Zero, Color.Blue)
    ];

    //Line
    private static readonly List<Object> ListZ =
    [
        new(new(200, 0), 10, 1E8, new (0, 0.1), Vector2.Zero, Color.Green),
        new(new(-100, 0), 20, 1E12, Vector2.Zero, Vector2.Zero, Color.Red),
    ];
    //Dual Orbital
    private static readonly List<Object> ListA =
    [
        new(new(100, 5), 18, 1E12, new (0, 0.4), Vector2.Zero, Color.Violet),
        new(new(-100, 5), 18, 1E12, new(0, -0.4), Vector2.Zero, Color.Magenta),
    ];
    //Orbital Earth/Moon    -- not finished
    public static List<Object> Earth =
    [
        //Earth - 1km = X * 1000000 / 2000
        new(new(0, 0), 30, 5.9724E24, new(0, 0), new(0, 0), Color.Blue),
        new(new (0.4055E6, 0), 10, 0.07346E24, new(0, 1.082), new(0, 0), Color.DarkGray)
    ];

    private static Env environement = new ("Null env", 0, 0, new ());
    public static void Main(string[] args)
    {
        Console.WriteLine();
        //Config verification
        if (!File.Exists("config.json"))
        {
            Console.WriteLine("config.json: not found");
            Init();
        }
        else if (JsonFiles.LoadFromFile<Dictionary<string, string>>("config.json")["asInitied"] != "true")
        {
            Console.WriteLine("config.json: not initied");
            Init();
        }

        
        //Start the program with the specified configuration
        Dictionary<string, string> config = JsonFiles.LoadFromFile<Dictionary<string, string>>("config.json");
        
        
        if (args.Contains("config"))
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start("start", "config.json");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", "config.json");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", "config.json");
            }
            else
            {
                Console.WriteLine("Unrecognized OS");
                
            }
            Console.WriteLine("Configuration file path:");
            Console.WriteLine(Directory.GetCurrentDirectory() + "/config.json");
            
            return;
        }

        if (args.Contains("--i-all"))
        {
            config["useDefaultEnvironment"] = config["useDefaultEnvironment"] == "true" ? "false" : "true";
            config["useDefaultObjects"] = config["useDefaultObjects"] == "true" ? "false" : "true";
        }
        else if (args.Contains("--i-env"))
        {
            config["useDefaultEnvironment"] = config["useDefaultEnvironment"] == "true" ? "false" : "true";
        }
        else if (args.Contains("--i-objs"))
        {
            config["useDefaultObjects"] = config["useDefaultObjects"] == "true" ? "false" : "true";
        }
        
            //Env configuration
        if (args.Contains("-env"))
        {
            if (args.Length <= args.ToList().IndexOf("-env") +1)
            {
                throw new ArgumentException("Argument -env must be followed by the environment file path.");
            }
            environement = JsonFiles.LoadEnvFromFile(args[args.ToList().IndexOf("-e") + 1]);
        }
        else if (config["useDefaultEnvironment"] == "true")
        {
            environement = JsonFiles.LoadEnvFromFile(config["defaultEnvironment"]);
        }
        else
        {
            Console.WriteLine("\nEnter a environment file path:");
            environement = JsonFiles.LoadEnvFromFile(Console.ReadLine());
        }
        
            //Objects configuration
        List<Object> objs;
        if (args.Contains("-objs"))
        {
            if (args.Length <= args.ToList().IndexOf("-objs") + 1)
            {
                throw new ArgumentException("Argument -objs must be followed by the objects file path.");
            }

            objs = JsonFiles.LoadObjectsFromFile(args[args.ToList().IndexOf("-o") + 1]);
        }
        else if (config["useDefaultObjects"] == "true")
        {
            objs = JsonFiles.LoadObjectsFromFile(config["defaultObjects"]);
        }
        else
        {
            Console.WriteLine("\nEnter a objects file path:");
            objs = JsonFiles.LoadObjectsFromFile(Console.ReadLine());
        }

        environement.Objects = objs;
        Sacard_Interface();
    }

    public static void Init()
    {
        JsonFiles.SaveEnvToFile(new ("Default Environment", 6.67E-11, 0, new()), "init/space.env");
        JsonFiles.SaveObjectsToFile(ListX, "init/ListX.objs");
        JsonFiles.SaveObjectsToFile(ListY, "init/ListY.objs");
        JsonFiles.SaveObjectsToFile(ListZ, "init/ListZ.objs");
        JsonFiles.SaveObjectsToFile(ListA, "init/ListA.objs");
        
        if(!Directory.Exists("objects"))
        {Directory.CreateDirectory("objects");}
        if(!Directory.Exists("environments"))
        {Directory.CreateDirectory("environment");}
        
        Dictionary<string, string> defaultConfig = new();
        defaultConfig["asInitied"] = "true";
        defaultConfig["useDefaultEnvironment"] = "true";
        defaultConfig["useDefaultObjects"] = "true";
        defaultConfig["defaultEnvironment"] = "init/space.env";
        defaultConfig["defaultObjects"] = "init/ListA.objs";
        
        JsonFiles.SaveToFile(defaultConfig, "config.json");
        
        Console.WriteLine("---Sacard as been initied correctly---\n\n");
        
    }


    public static void DebugGravitation()
    {
        
        foreach (Object obj in environement.Update())
        {
            Console.WriteLine(obj.ToString());
            Console.WriteLine(environement.Objects.IndexOf(obj));
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

        foreach (Object obj in environement.Update())
        {
            SacardDrawer.objects.Add(obj);
            //SacardDrawer.objects.Add(new (obj.Position / 1000000, obj.Radius, obj.Mass, obj.Velocity, obj.Force, obj.Color));
        }
        

        //Thread.Sleep(10);
    }


}

