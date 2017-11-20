
class A<T> {
    public <R> void g(R x) {
    }
}

class Movie {}
class TvShow extends Movie {}
class Actor {}

public class App {

    public static void main(String[] args) {
        A<Actor> a = new A<Actor>();
        
        a.<Movie>g(new Movie());
        /*
         * Parametro de tipo R é INFERIDO a partir do parametro actual x
         */
        a.g(new Movie()); 
        a.g(new TvShow()); 
        
        A a2 = new A();
    }
}