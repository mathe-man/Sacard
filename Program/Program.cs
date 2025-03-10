
using Gtk;
using Sacard_PE;
using Sacard_Drawer;

using Object = Sacard_PE.Object;

// The Debug/Use entry point
class Program
{
    private Window window;
    public static List<Object> XList =
    [
        new(new (3, 4),  1, 100, Vector2.Zero, Vector2.Zero),
        new(new (-3, 4), 1, 100, Vector2.Zero, Vector2.Zero),
        new(new (0, -4), 1, 100, Vector2.Zero, Vector2.Zero)
    ];
    public static List<Object> YList =
    [
        new(new (5, 6),  1, 100, Vector2.Zero, Vector2.Zero),
        new(new (-5, 6), 1, 100, Vector2.Zero, Vector2.Zero),
        new(new (0, -6), 1, 100, Vector2.Zero, Vector2.Zero)
    ];

    public static List<Object> ZList =
    [
        new(new(5, 0), 1, 10, Vector2.Zero, Vector2.Zero),
        new(new(-5, 0), 1, 10, Vector2.Zero, Vector2.Zero),
    ];
    public static Env simEnv = new Env("aa", 6.67E-11, 0, ZList);


    public static void Main()
    {
        //LoopSimulation();
        //MesuredSimulation(120_000);
        //MesuredSimulation(30_000);
        //LoopSimulation();
        Application.Init();

        // Création de la fenêtre
        DrawerWindow fenetre = new DrawerWindow();
        

        // Ajout de quelques cercles avec des propriétés (x, y, rayon, r, g, b)
        fenetre.SetCircle(100, 100, 50, 1.0, 0.0, 0.0);   // Cercle rouge
        fenetre.SetCircle(200, 150, 30, 0.0, 1.0, 0.0);   // Cercle vert
        fenetre.SetCircle(150, 200, 40, 0.0, 0.0, 1.0);   // Cercle bleu

        // Rafraîchit la fenêtre pour afficher les nouveaux cercles
        fenetre.UpdateWindow();

        // Lance la boucle principale GTK
        Application.Run();
        Console.WriteLine("End");
    }

    public static void LoopSimulation()
    {
        bool end = false;
        int loop = 0;
        while (!end)
        {
            simEnv.Update(ref end);
            loop++;
        }
        
        Console.WriteLine($"In {loop} loop ({loop/60}s at 60fps) :");
        foreach (Object obj in simEnv.Objects)
        {
            Console.WriteLine(
                $"Velocity:{obj.Velocity.ToString()}  " +
                $"Position:{obj.Position.ToString()}  " +
                $"Force:   {obj.Force.ToString()}"
            );
        }
    }

    public static void  MesuredSimulation(int loopNumber)
    {
        bool end = false;
        for (int i = 0; i < loopNumber; i++)
        {
            simEnv.Update(ref end);

            if (i % 1000 == 0 || i > loopNumber - 3 || end)
            {
                foreach (Object obj in simEnv.Objects)
                {
                    Console.WriteLine(
                        $"Velocity:{obj.Velocity.ToString()}  " +
                        $"Position:{obj.Position.ToString()}  " +
                        $"Force:   {obj.Force.ToString()}"
                    );
                }

                
                Console.WriteLine($"Loop {i+1} - - {end} - - \n");
                end = false;
            }
            
            
        }
    }

    
    
    
    public static List<Object> simObjs;
    public static bool collided = false;
    
    public static void SacardPEInterface()
    {
        Timer refresh = new Timer(refreshDrawer, "Yep", 400, 100);

        while (true)
        {
            simObjs = simEnv.Update(ref  collided, null);
        }
        
    }

    public static void refreshDrawer(object? o)
    {
        Console.Clear();
        Console.WriteLine("Sacard PE Interface");
        Console.WriteLine($"-----------{collided}-----------");
        Console.WriteLine(Vector2.DistanceDoubleBetween(simObjs[0].Position, simObjs[1].Position));
        Console.WriteLine("----------------------------------");
        Console.WriteLine(simObjs[0].Position.ToString());
        Console.WriteLine(simObjs[1].Position.ToString());
        
        
    }
}