namespace negativePositiveSorting {
    enum SaveChoiceControls {
        save = 1,
        cancel,
        exit
    }
    enum MainMenuControls {
        manual = 1,
        file,
        test,
        exit
    }
    class Interface {

        public static void DivideLine() {
            Console.WriteLine("==========================================================================================================");
        }
        public static void GiveWelcomeMessage() {
            DivideLine();
            Console.WriteLine("Добро пожаловать!");
            Console.WriteLine("Вариант №4 работы №2 был выполнен студентом группы 423 Ореховым Даниилом.");
            Console.WriteLine("Задание: Написать программу сортировки элементов массива так, чтобы отрицательные элементы чередовались с положительными.");
            Console.WriteLine("(В рамках данной работы '0' считается положиельным числом)");
            DivideLine();
        }
        private static void GiveMainMenu() {
            Console.WriteLine("Выберите метод ввода данных:");
            Console.WriteLine("1. Ручной ввод");
            Console.WriteLine("2. Ввод из файла");
            Console.WriteLine("3. Тестирование");
            Console.WriteLine("4. Выход");
            DivideLine();
        }
        public static List<double> FillArray() {
            bool exitFlag = false;
            List<double> array = [];

            while (!exitFlag) {
                GiveMainMenu();

                MainMenuControls selection = (MainMenuControls)GetIntInput();

                switch (selection) {
                    case MainMenuControls.manual:
                        GiveInputInstructions();
                        string? input;
                        do {
                            Console.WriteLine($"Введите переменную: ");
                            input = Console.ReadLine();
                            if (!string.IsNullOrEmpty(input)) { 
                                input = GetDoubleInput(input).Replace(',', '.');
                                if (input == "~") {
                                    break;
                                }

                                array.Add(double.Parse(input));
                            }
                        } while (true);
                        exitFlag = true;

                        break;
                    case MainMenuControls.file:
                        //Console.WriteLine("You selected Option 2.");
                        string fileName = Files.FileDownloadValidation();
                        if(fileName == "~") {
                            exitFlag = false;
                            continue;
                        }
                        array = Files.FileDownload(fileName);
                        exitFlag = true;
                        break;
                    case MainMenuControls.test:
                        Test.TestSorting();
                        exitFlag = false;
                        continue;
                    case MainMenuControls.exit:
                        Console.WriteLine("Выход...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Ошибка ввода! Попробуйте снова.");
                        exitFlag = false;
                        continue;
                }
            }
            return array;
        }
        private static void GiveInputInstructions() {
            Console.WriteLine("Введите элементы массива. Для выхода введите '~':");
        }
        public static int GetIntInput() {
            bool errFlag;
            int number;
            do {
                string? userInput = Console.ReadLine();
                errFlag = !int.TryParse(userInput, out number);

                if (errFlag) {
                    Console.WriteLine("Ошибка ввода! Попробуйте снова.");
                }

            } while (errFlag);

            return number;
        }
        private static string GetDoubleInput(string? input) {
            bool errFlag = true;
            double number = 0.0;
            while (errFlag) {
                if (input == "~") {
                    return input;
                }
                if (string.IsNullOrEmpty(input) || !double.TryParse(input.Replace(',', '.'), out number)) {
                    Console.WriteLine("Ошибка ввода! Попробуйте снова.");
                    input = Console.ReadLine();
                    errFlag = true;
                }
                else {
                    errFlag = false;
                }
            }
            return input;
        }
        public static void SaveChoice(List<double> sortedArray, List<double> unsortedArray) {
            bool errFlag = false;
            Console.WriteLine("Сохранить результат?");
            Console.WriteLine("1. Да");
            Console.WriteLine("2. Нет");
            Console.WriteLine("3. Выход из программы");
            do {
                SaveChoiceControls selection = (SaveChoiceControls)GetIntInput();
                switch (selection) {
                    case SaveChoiceControls.save:
                        (bool isNameValid, string filePath) = Files.FileUploadValidation();
                        if (isNameValid) {
                            Files.FileUpload(filePath, sortedArray, unsortedArray);
                        }
                        break;
                    case SaveChoiceControls.cancel:

                        break;
                    case SaveChoiceControls.exit:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Ошибка ввода! Попробуйте снова.");
                        errFlag = true;
                        break;
                }
            } while (errFlag);
        }
        public static void GiveArray(List<double> array) {
                Console.WriteLine(string.Join(", ", array));
        }
    }
}