class Student {
    int nr;
    String name;

    public Student(int nr, String name) {
        this.nr = nr;
        this.name = name;
    }
}

class App {

    static void inspect(Object obj) {
        Class t = obj.getClass();
        System.out.println("obj Class = " + t);
    }

    static void sameClass(Object o1, Object o2) {
      
        boolean res = (o1.getClass() == o2.getClass()); // Comparação de Identidade
        if(res)
            System.out.println("o1 and o2 SAME Class");
        else
            System.out.println("o1 and o2 DIFERENT Class");
    }

    static void isInstanceOf(Object obj, Class t) {
        if(obj.getClass() == t)
            System.out.println("obj is instance of " + t);
        else 
            System.out.println("obj is NOT instance of " + t);

    }

    public static void main(String[] args) {
        Student s1 = new Student(8233, "Maria");
        Student s2 = new Student(8233, "Jose");
        String str = "super isel";
        inspect(s1);
        inspect(s2);
        inspect(str);
        sameClass(s1, s2);
        sameClass(s1, str);
        isInstanceOf(s1, Student.class);
        isInstanceOf(str, Student.class);
    }
}