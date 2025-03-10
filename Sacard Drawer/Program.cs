using System;
using Gtk;
using Cairo;

class Program
{
    static void Main()
    {
        Application.Init();

        Window window = new Window("Dessiner des formes avec GTK#");
        window.SetDefaultSize(400, 300);
        window.DeleteEvent += delegate { Application.Quit(); };

        DrawingArea drawingArea = new DrawingArea();
        drawingArea.Drawn += OnDrawn;
        window.Add(drawingArea);

        window.ShowAll();
        Application.Run();
    }

    static void OnDrawn(object sender, DrawnArgs args)
    {
        DrawingArea area = (DrawingArea)sender;
        using (Context cr = args.Cr)
        {
            cr.SetSourceRGB(0.2, 0.3, 0.8);
            cr.Arc(200, 150, 100, 0, 2 * Math.PI);
            cr.Fill();
        }
    }
}