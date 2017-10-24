using System;

class Student {
    int nr;
    string name; 
    public Student(int nr,  string name) {
        this.nr = nr;
        this.name = name;
    }
}
static class App {
    static void Main() {
        Student s = new Student(1725, "Ze Manel");
        Object o = s; // Up Cast => ldloc.0 stloc.1
        Student s2 = (Student) o; // Down Cast => ldloc.1 + castclass Student + stloc.2
        Object o2 = "isel";
        // Student s3 = (Student) o2; // => InvalidCastException
        
        int n = 11;
        object o3 = n; // ldloc.4 + box Int32 + stloc.5
        int n2 = (int) o3; // ldloc.5 + unbox.any Int32 + stloc.6
    }
}