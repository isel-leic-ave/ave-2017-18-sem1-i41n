
delegate int Func(string msg);

public class App {
    static int ToInt(string str) { return str.GetHashCode(); }

    public static void Main(string [] args) {
        Func f1 = App.ToInt;
        /**
         * ldnull             // 1º argumento do construtor
         * ldftn App::ToInt   // 2º argumento do construtor
         * newobj Func::.ctor // malloc + init a zeros + call .ctor + ret ponteiro objecto
         */
        Func f2 = new Func(App.ToInt); 

    }
}