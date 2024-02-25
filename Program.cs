using System;
using System.Globalization;

namespace negativePositiveSorting {
    class Program {
        static void Main() {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

            while (true) {
                Interface.GiveWelcomeMessage();

                List<double> array = Interface.FillArray();
                List<double> sortedArray = Calculatons.Sort(array);

                Interface.DivideLine();
                Console.WriteLine($"Результат:");
                Interface.GiveArray(sortedArray);
                Interface.DivideLine();

                Interface.SaveChoice(sortedArray, array);
            }
        }
    }
}