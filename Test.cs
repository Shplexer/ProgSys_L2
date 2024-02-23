using System;

namespace negativePositiveSorting {
    class Test {
        public static void TestSorting() {

            // Arrange
            List<double> testArray = [-1.1, 2.2, -3.3, 4.4, -5.5];
            List<double> expected = [2.2, -1.1, 4.4, -3.3, -5.5];

           
            List<double> result = Calculatons.Sort(testArray);
            if (result.SequenceEqual(expected)) {
                Console.WriteLine("ТЕСТ ПРОЙДЕН");
                Interface.DivideLine();
            }
            else {
                Console.WriteLine("ТЕСТ НЕ ПРОЙДЕН");
                Console.WriteLine();
                Console.WriteLine($"РЕЗУЛЬТАТ РАБОТЫ ПРОГРАММЫ: {string.Join(", ", result)}");
                Console.WriteLine($"ОЖИДАЕМЫЙ РЕЗУЛЬТАТ: {string.Join(", ", expected)}");
                Interface.DivideLine();
            }
        }
    }
}
