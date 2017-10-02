using System;

class App{
   static void Main() {
        Tester.RunTests(typeof(TestUtils));
   }
}

public class TestUtils {

    [Test]
    [Inline(7)]
    public void TestWrongNumberOfArguments(int val, string b) {
        
    }

    [Test]
    [Inline(7)]
    public void TestWrongTypeOfArguments(string val) {
        
    }

    [Test]
    [Inline(7)]
    [Inline(10)]
    [Inline(13)]
    public void TestIsOdd(int val) {
        bool res = Utils.IsOdd(val);
        Tester.Assert(true, res);
    }

    [Test]
    public void TestIsOddFor7() {
        bool res = Utils.IsOdd(7);
        Tester.Assert(true, res);
    }
    [Test]
    public void TestIsOddWith11() {
        bool res = Utils.IsOdd(11);
        Tester.Assert(false, res);
    }
}

public class Utils {
    public static bool IsOdd(int nr) {
        return nr % 2 != 0;
    }
}

