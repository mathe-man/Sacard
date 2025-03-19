
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.InteropServices;

using Raylib_cs;


namespace Sacard;

// The Debug/Use entry point
internal static class Program
{
    //first Triangle shape points list
    private static readonly List<Object> ListX =
    [
        new(new(150, 200), 15, 1e12, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(-150, 200), 15, 1e12, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(0, -200), 15, 1e12, Vector2.Zero, Vector2.Zero, Color.Blue)
    ];
    //second
    private static readonly List<Object> ListY =
    [
        new(new(250, 100), 10, 1e11, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(-250, 100), 10, 1e11, Vector2.Zero, Vector2.Zero, Color.Blue),
        new(new(0, -100), 10, 1e11, Vector2.Zero, Vector2.Zero, Color.Blue)
    ];

    //Line
    private static readonly List<Object> ListZ =
    [
        new(new(200, 0), 10, 1e8, new (0, 0.1), Vector2.Zero, Color.Green),
        new(new(-100, 0), 20, 1e12, Vector2.Zero, Vector2.Zero, Color.Red),
    ];
    //Dual Orbital
    private static readonly List<Object> ListA =
    [
        new(new(100, 5), 18, 1e12, new (0, 0.4), Vector2.Zero, Color.Violet),
        new(new(-100, 5), 18, 1e12, new(0, -0.4), Vector2.Zero, Color.Magenta),
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
        if (!File.Exists("SacardGline Files/config.json"))
        {
            Console.WriteLine("config.json: not found");
            Init();
        }
        else if (JsonFiles.LoadFromFile<Dictionary<string, string>>("config.json")["asInitied"] != "true")
        {
            Console.WriteLine("config.json: not initied");
            Init();
        }
        else if (args.Contains("-init"))
        {Init();}
        
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
            environement = JsonFiles.LoadEnvFromFile(args[args.ToList().IndexOf("-env") + 1]);
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

            objs = JsonFiles.LoadObjectsFromFile(args[args.ToList().IndexOf("-objs") + 1]);
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

    private static void Init()
    {
        JsonFiles.SaveEnvToFile(new ("Default Environment", 6.67e-11, 0, new()), "space.env");
        JsonFiles.SaveObjectsToFile(ListX, "ListX.objs");
        JsonFiles.SaveObjectsToFile(ListY, "ListY.objs");
        JsonFiles.SaveObjectsToFile(ListZ, "ListZ.objs");
        JsonFiles.SaveObjectsToFile(ListA, "ListA.objs");
        
        if(!Directory.Exists("SacardGline Files/objects"))
        {Directory.CreateDirectory("SacardGline Files/objects");}
        if(!Directory.Exists("SacardGline Files/environments"))
        {Directory.CreateDirectory("SacardGline Files/environment");}
        
        Dictionary<string, string> defaultConfig = new();
        defaultConfig["asInitied"] = "true";
        defaultConfig["useDefaultEnvironment"] = "true";
        defaultConfig["useDefaultObjects"] = "true";
        defaultConfig["defaultEnvironment"] = "space.env";
        defaultConfig["defaultObjects"] = "ListA.objs";
        
        JsonFiles.SaveToFile(defaultConfig, "config.json");
        
        Console.WriteLine("---SacardGline as been initied correctly---\n\n");
        
    }

    private static void Sacard_Interface()
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
        }
        

        //Thread.Sleep(10);
    }


}



public class JsonFiles
{
    //Basic json de/serialisation and load/save
    public static T LoadFromFile<T>(string fileName)
    {
        // Read the file content to a variable
        string jsonString = File.ReadAllText("SacardGline Files/" + fileName);
        // Deserialize the content
        T? value = JsonConvert.DeserializeObject<T>(jsonString);
        if (Equals(value, null))
        {
            throw new NullReferenceException($"{fileName} deserialization result is null (result:{value})");
        }

        return value;
    }
    public static void SaveToFile<T>(T content, string filePath)
    {
        try
        {
            if (File.Exists("SacardGline Files/" + filePath))  //Only append when the file is already existent
            {
                // Serialize the content into Json format
                string jsonString = JsonConvert.SerializeObject(content, Formatting.Indented);
                // Write the json formated content in the specified file
                File.WriteAllText("SacardGline Files/" + filePath, jsonString);
            }
            else                        // if the file doesn't exist, it will be created and the function are recalled
            {
                Console.WriteLine($"File {filePath} don't exist");
                FileStream fs = File.Create("SacardGline Files/" + filePath);
                fs.Close();
                SaveToFile(content, filePath);
            }
        }
        catch (DirectoryNotFoundException)
        {
            if (!Directory.Exists("SacardGline Files"))
            {
                Directory.CreateDirectory("SacardGline Files");
                SaveToFile(content, filePath);
            }
            else
            {
                Directory.CreateDirectory("SacardGline Files/" + filePath.Remove(filePath.LastIndexOf("/")));
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
