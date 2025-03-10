
using Gtk;
using Cairo;


namespace Sacard_Drawer;

//Circle class to handle needed information
public class Circle
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Radius { get; set; }
    public double Red { get; set; }
    public double Green { get; set; }
    public double Blue { get; set; }

    public Circle(double x, double y, double radius, double red, double green, double blue)
    {
        X = x;
        Y = y;
        Radius = radius;
        Red = red;
        Green = green;
        Blue = blue;
    }
}

public class DrawerWindow : Window
{
    private DrawingArea drawingArea;
    public List<Circle> circles;
    
    public bool clearList;

    public DrawerWindow(bool clearListOnUpdate = false, string title = "Drawer Window") : base(title)
    {
        //Init window
        SetDefaultSize(720, 420);
        SetPosition(WindowPosition.Center);

        //Set properties
        circles = new List<Circle>();
        clearList = clearListOnUpdate;
        
        //Create a new drawing area
        drawingArea = new DrawingArea();
        //Use Drawn event of drawingArea
        drawingArea.Drawn += OnDrawingAreaDrawn;

        DeleteEvent += (o, args) =>
        {
            Application.Quit();
            args.RetVal = true;
        };

        ShowAll();
    }
    
    //Drawing method, automaticly called to re-draw the area
    private void OnDrawingAreaDrawn(object sender, DrawnArgs args)
    {
        Context cr = args.Cr;
        
        //Remove white background
        cr.SetSourceRGB(1, 1, 1);
        cr.Paint();

        foreach (Circle circle in circles)
        {
            cr.SetSourceRGB(circle.Red, circle.Green, circle.Blue);
            cr.Arc(circle.X, circle.Y, circle.Radius, 0, 2 * Math.PI);
            
            cr.Fill();
        }
    }

    
    public void Start()
    {
        Console.WriteLine("Start() called");
    }

    //Add a new circle in the circles list and return the new list
    public List<Circle> SetCircle(double x, double y, double radius, double red, double green, double blue)
    {
        Circle circle = new Circle(x, y, radius, red, green, blue);
        circles.Add(circle);
        return circles;
    }

    public void UpdateWindow()
    {
        
        drawingArea.QueueDraw();
        if(clearList)
        {circles.Clear();}
    }
}



