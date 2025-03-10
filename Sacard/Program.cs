

using Sacard_Drawer;
using Sacard_Utilities;
using Object = Sacard_PE.Object;

namespace Sacard;


// The Debug/Use entry point
class Program
{/*
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
*/

    public static void Main()
    {
        //LoopSimulation();
        //MesuredSimulation(120_000);
        //MesuredSimulation(30_000);
        //LoopSimulation();
        SacardDrawer drawer1 = new SacardDrawer(400, 400, 60, "1"); drawer1.Start();
        SacardDrawer drawer2 = new SacardDrawer(400, 400, 60, "2"); drawer2.Start();
        SacardDrawer drawer3 = new SacardDrawer(400, 400, 60, "3"); drawer3.Start();
    }
/*
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
    */
}