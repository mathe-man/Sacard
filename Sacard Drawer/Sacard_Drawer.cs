using Raylib_cs;  // Import de Raylib-cs
using Sacard_Utilities;
using Vector2 = Sacard_Utilities.Vector2;

namespace Sacard_Drawer;

public class SacardDrawer
{
    public List<Circle> Circles = new List<Circle>();
    
    public string Title = "Sacard Drawer";

    public Action FirstLoopAction;
    public Action LastLoopAction;       
    public Action WindowCloseAction;
    

    public SacardDrawer(int width, int height, int fpsRate = 60, string title = "Sacard Drawer")
    {
        Raylib.InitWindow(width, height, title);
        Raylib.SetTargetFPS(fpsRate);
    }

    public void Start()
    {
        while (!Raylib.WindowShouldClose())  // Boucle principale
        {
            if(FirstLoopAction != null){FirstLoopAction();}
            
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);
            string message = "Circle drawed: ";

            foreach (Circle c in Circles)
            {
                Raylib.DrawCircle((int)(Math.Round(c.Position.X * 10) + 720/2), (int)(Math.Round(c.Position.Y * 10) + 520/2), (int)c.Radius, Color.Orange);
                message += c.Position.ToString();
            }
            Console.WriteLine(message);
            Raylib.EndDrawing();
            
            if(LastLoopAction != null){LastLoopAction();}
        }
        
        if(WindowCloseAction != null){WindowCloseAction();}

        Raylib.CloseWindow();
    }
}



public class Circle
{
    public Vector2 Position;
    public double Radius;
    public int Red;
    public int Green;
    public int Blue;

    public Circle(Vector2 position, double radius, int red, int green, int blue)
    {
        Position = position;
        Radius = radius;
        Red = red;
        Green = green;
        Blue = blue;
    }
    
}