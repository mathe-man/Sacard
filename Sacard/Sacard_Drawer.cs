using Raylib_cs;  // Import de Raylib-cs for 2d rendering
namespace Sacard;

public static class SacardDrawer
{
    public static List<Object> objects = new List<Object>();
    
    public static string Title = "Sacard Drawer";
    public static bool DisplayInfo = false;

    public static Action? FirstLoopAction = null;
    public static Action? LastLoopAction = null;       
    public static Action? WindowCloseAction = null;

    private static int? Width = null;
    private static int? Heigth = null;
    

    public static void Init(int width, int height, int fpsRate = 60, string title = "Sacard Drawer", bool displayInfo = false)
    {
        Raylib.InitWindow(width, height, title);
        Raylib.SetTargetFPS(fpsRate);
        
        Width = width;
        Heigth = height;
        
        DisplayInfo = displayInfo;
    }

    public static void Start()
    {
        Camera2D camera = new Camera2D();
        camera.Target = new  System.Numerics.Vector2(0, 0);
        camera.Rotation = 0;
        camera.Zoom = 1;
        
        while (!Raylib.WindowShouldClose())  // Boucle principale
        {
            if(FirstLoopAction != null){FirstLoopAction();}
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);
            Raylib.BeginMode2D(camera);


            foreach (var obj in objects)
            {
                Raylib.DrawCircleLines((int)obj.Position.X, (int)obj.Position.Y, (int)obj.Radius, obj.Color);
                if (DisplayInfo)
                {
                    int textpositionX = (int)obj.Position.X + (int)obj.Radius;
                    int textpositionY = (int)obj.Position.Y + (int)obj.Radius;
                    string info = $"X:{Math.Round(obj.Position.X, 3)} ; Y:{Math.Round(obj.Position.Y, 3)} \n" +
                                  $"Vx:{Math.Round(obj.Velocity.X, 3)} ; Vy:{Math.Round(obj.Velocity.Y, 3)} \n" +
                                  $"R:{Math.Round(obj.Radius, 3)} ; M:{Math.Round(obj.Mass, 3)}; I:{objects.IndexOf(obj)}";
                    Raylib.DrawText(info, textpositionX, textpositionY, (int)Math.Floor(obj.Radius / 3), obj.Color);
                    Console.WriteLine(info);
                }
            }
            
            
            
            Raylib.EndMode2D();
            Raylib.EndDrawing();
            if(LastLoopAction != null){LastLoopAction();}
        }
        
        if(WindowCloseAction != null){WindowCloseAction();}
        Raylib.CloseWindow();
    }
}

