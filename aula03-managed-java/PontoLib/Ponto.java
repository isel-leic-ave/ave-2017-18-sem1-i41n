class Ponto {

	public int _x, _y;

	Ponto(int x, int y) {
        this._x = x;
        this._y = y;
    }

	public void print() {
        System.out.println(
            String.format("Print V3 super (x = %d, y = %d)\n", _x, _y)
        );
    }
};