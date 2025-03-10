using Raylib_cs;  // Import de Raylib-cs
using Sacard_Utilities;
using Vector2 = Sacard_Utilities.Vector2;

namespace Sacard_Drawer;

public class SacardDrawer
{
    public List<Object> Objects = new List<Object>();
    
    public string Title = "Sacard Drawer";
    

    public SacardDrawer(int width, int height, int fpsRate = 60, string title = "Sacard Drawer")
    {
        Raylib.InitWindow(width, height, title);
        Raylib.SetTargetFPS(fpsRate);
    }

    public void Start()
    {
        while (!Raylib.WindowShouldClose())  // Boucle principale
        {
            // Récupération de la position de la souris
            Vector2 mousePos = Vector2.FromSystemVector2(Raylib.GetMousePosition());

            // Début du rendu
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);

            // Dessiner un cercle qui suit la souris
            Raylib.DrawCircleV(mousePos.ToSystemVector2(), 20, Color.Red);

            // Affichage des infos
            Raylib.DrawText("Déplace la souris !", 10, 10, 20, Color.DarkGray);

            // Fin du rendu
            Raylib.EndDrawing();
        }
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