using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace negativePositiveSorting {
    class Calculatons {
        public static List<double> Sort(List<double> originalArray) {

            List<double> positives = originalArray.Where(x => x >= 0).ToList();
            List<double> negatives = originalArray.Where(x => x < 0).ToList();
            List<double> sortedArray = [];
            int i = 0, j = 0;

            while (i < positives.Count && j < negatives.Count) {
                sortedArray.Add(positives[i++]);
                sortedArray.Add(negatives[j++]);
            }

            // Append the remaining elements from the longer list
            while (i < positives.Count) {
                sortedArray.Add(positives[i++]);
            }
            while (j < negatives.Count) {
                sortedArray.Add(negatives[j++]);
            }

            return sortedArray;
        }
    }
}
