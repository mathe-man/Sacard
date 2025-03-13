using System.Diagnostics;
using Raylib_cs;  // Import de Raylib-cs for 2d rendering
namespace Sacard;

public static class SacardDrawer
{
    public static List<Object> objects = new List<Object>();
    
    public static string Title = "Sacard Drawer";
    public static bool DisplayInfo = false;

    public static List<Action> FirstLoopAction = new ();
    public static List<Action> LastLoopAction = new ();       
    public static List<Action> WindowCloseAction = new ();

    

    public static void Init(int width, int height, int fpsRate = 60, string title = "Sacard Drawer", bool displayInfo = false)
    {
        Raylib.InitWindow(width, height, title);
        Raylib.SetTargetFPS(fpsRate);
        
        
        DisplayInfo = displayInfo;
    }

    public static void Start()
    {
        Camera2D camera = new ();
        camera.Target = new  System.Numerics.Vector2(0, 0);
        camera.Offset = new System.Numerics.Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2); // Décale l'origine
        camera.Rotation = 0;
        camera.Zoom = 1;
        
        
        while (!Raylib.WindowShouldClose())  // Main loop
        {
            if(FirstLoopAction.Count > 0){
                foreach (var action in FirstLoopAction)
                {
                    action();
                }
            }

            
            //Bodies drawing
            Raylib.BeginDrawing();
            Raylib.BeginMode2D(camera);
            Raylib.ClearBackground(Color.RayWhite);
            
            
            Stopwatch  stopwatch = new Stopwatch(); // Debugging/optimizing drawing
            stopwatch.Start();
            
            foreach (var obj in objects)
            {
                Parallel.Invoke(() => {Raylib.DrawCircleLines((int)obj.Position.X, (int)obj.Position.Y, (int)obj.Radius, obj.Color); });
                Parallel.Invoke(() => {Raylib.DrawLine((int)obj.Position.X, (int)obj.Position.Y, (int)(obj.Position.X + obj.Velocity.X * 100), (int)(obj.Position.Y + obj.Velocity.Y * 100), obj.Color);});
                
                if (DisplayInfo)
                {
                    int textpositionX = (int)obj.Position.X + (int)obj.Radius;
                    int textpositionY = (int)obj.Position.Y + (int)obj.Radius;
                    string info = $"P:({Math.Round(obj.Position.X, 3)} ; {Math.Round(obj.Position.Y, 3)}) \n" +
                                  $"V:({Math.Round(obj.Velocity.X, 3)} ; {Math.Round(obj.Velocity.Y, 3)}) \n" +
                                  $"R:{Math.Round(obj.Radius, 3)} ; M:{Math.Round(obj.Mass, 3)}; I:{objects.IndexOf(obj)}";
                    Raylib.DrawText(info, textpositionX, textpositionY, (int)Math.Floor(obj.Radius / 3), obj.Color);
                }
            }
            Raylib.DrawCircleLines(0, 0, 3, Color.Black);
            
            stopwatch.Stop();   //Debug end
            Console.Clear();
            Console.WriteLine(stopwatch.Elapsed);   //Give the result
            
            Raylib.EndMode2D();
            Raylib.EndDrawing();
            if(LastLoopAction.Count > 0){
                foreach (var action in LastLoopAction)
                {
                    action();
                }
            }
        }
        
        if(WindowCloseAction.Count > 0){
            foreach (var action in WindowCloseAction)
            {
                action();
            }
        }
        Raylib.CloseWindow();
    }
}

