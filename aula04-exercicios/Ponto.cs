using System;

public class Ponto {

	public int _x, _y;

	public Ponto(int x, int y) {
        this._x = x;
        this._y = y;
    }
    public double module() {
        int x2 = _x*_x;
        int y2 = _y*_y;
        return Math.Sqrt(x2 + y2);
    }
    
	public void print() {
        Console.WriteLine(
            String.Format("Print V3 super (x = {0}, y = {1})\n", _x, _y)
        );
    }
};