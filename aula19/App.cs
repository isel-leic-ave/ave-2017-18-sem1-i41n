using System;

class Point {
    private readonly int id;
    public int X {get; set; }
    public int Y {get; set; }
    public double GetModule() {
        return Math.Sqrt(X*X + Y*Y);
    }
    public Point(int id, int x, int y) {
        this.id = id;
        X = x; Y = y;
    }
}

static class App {
    static void Main() {
        IEqualityComparer cmpId = Comparators.Comparer(typeof(Point), "id");
        IEqualityComparer cmpCoords = Comparators.Comparer(typeof(Point), "X", "Y");
        IEqualityComparer cmpModule = Comparators.Comparer(typeof(Point), "GetModule");
        Point p1 = new Point(54132, 5, 7);
        Point p2 = new Point(980, 5, 7);
        Point p3 = new Point(65465, 7, 5);
        
        Console.WriteLine(cmpId.Equals(p1, p2)); // false 
        Console.WriteLine(cmpCoords.Equals(p1, p2)); // true
        Console.WriteLine(cmpCoords.Equals(p2, p3)); // false
        Console.WriteLine(cmpModule.Equals(p2, p3)); // true
    }
}