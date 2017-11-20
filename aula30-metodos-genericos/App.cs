
class A<T> {
    public void g<R>(R x) {
    }
}

class Movie {}
class TvShow: Movie {}
class Actor {}

public class App {

    public static void Main() {
        A<Actor> a = new A<Actor>();
        
        a.g<Movie>(new Movie());
        /*
         * Parametro de tipo R é INFERIDO a partir do parametro actual x
         */
        a.g(new Movie()); 
        a.g(new TvShow()); 
        
        // Using the generic type 'A<T>' requires 1 type argument !!!!!!!!!
        // A a = new A();
    }
}