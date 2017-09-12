using System;

public class Ponto {

	public int _x, _y;

	public Ponto(int x, int y) {
        this._x = x;
        this._y = y;
    }

	public void print() {
        Console.WriteLine(
            String.Format("Print V3 super (x = {0}, y = {1})\n", _x, _y)
        );
    }
};