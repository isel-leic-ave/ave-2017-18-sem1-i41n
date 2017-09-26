using System;

class App{
   static void Main() {
        Tester.RunTests(typeof(TestUtils));
   }
}

public class TestUtils {
    [Test]
    public void TestIsOddWithOddNumber() {
        bool res = Utils.IsOdd(7);
        Tester.Assert(true, res);
    }
    [Test]
    public void TestIsOddWithOddEven() {
        bool res = Utils.IsOdd(11);
        Tester.Assert(false, res);
    }
}

public class Utils {
    public static bool IsOdd(int nr) {
        return nr % 2 != 0;
    }
}

