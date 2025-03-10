using Raylib_cs;  // Import de Raylib-cs
using System.Numerics;

class Program
{
    static void Main()
    {
        // Initialisation de la fenêtre
        Raylib.InitWindow(800, 600, "Raylib-cs Example");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())  // Boucle principale
        {
            // Récupération de la position de la souris
            Vector2 mousePos = Raylib.GetMousePosition();

            // Début du rendu
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);

            // Dessiner un cercle qui suit la souris
            Raylib.DrawCircleV(mousePos, 50, Color.Red);

            // Affichage des infos
            Raylib.DrawText("Déplace la souris !", 10, 10, 20, Color.DarkGray);

            // Fin du rendu
            Raylib.EndDrawing();
        }
        // Fermeture de la fenêtre
        Raylib.CloseWindow();
    }
}