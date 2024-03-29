﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace negativePositiveSorting {
    class Files {
        public static (bool isValid, string fileName) FileUploadValidation() {
            bool saveFlag = true;
            bool errFlag = false;
            string pattern = @"^[a-zA-Z0-9_.-]+$";
            string[] reservedNames = { "CON", "PRN", "AUX", "NUL",
                "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
                "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };
            string? fileName;
            do {
                saveFlag = true;
                errFlag = false;
                do {
                    Console.WriteLine("Введите имя файла: ");
                    fileName = Console.ReadLine();
                } while (string.IsNullOrEmpty(fileName) || !Regex.IsMatch(fileName, pattern) || reservedNames.Contains(fileName.ToUpper()));


                if (!fileName.EndsWith(".txt")) {
                    fileName += ".txt";
                }
                fileName = $"../../../{fileName}";

                //if (!File.Exists(fileName)) {
                //    Console.WriteLine("Ошибка! Файл с таким именем не найден.");
                //    errFlag = true;
                //}
                if (File.Exists(fileName)) {
                    Console.WriteLine("Файл с таким именем уже существует. Перезаписать?");
                    Console.WriteLine("1. Да");
                    Console.WriteLine("2. Нет");
                    SaveChoiceControls selection = (SaveChoiceControls)Interface.GetIntInput();
                    switch (selection) {
                        case SaveChoiceControls.save:
                            saveFlag = true;
                            break;
                        case SaveChoiceControls.cancel:
                            saveFlag = false;
                            break;
                        default:
                            Console.WriteLine("Ошибка ввода! Попробуйте снова.");
                            errFlag = true;
                            break;
                    }
                }
                else {
                    saveFlag = true;
                    errFlag = false;
                }


            } while (errFlag);
            return (saveFlag, fileName);
        }
        public static void FileUpload(string fileName, List<double> result, List<double> array) {
            using StreamWriter writer = new(fileName);
            foreach (double element in array) {
                writer.WriteLine($"{element}");
            }
            writer.WriteLine("//");
            writer.WriteLine($"Отсортированный массив: {string.Join(", ", result)}");
            writer.Close();
        }
        public static string FileDownloadValidation() {
            string? fileName;
            bool errFlag;
            do {
                errFlag = false;
                Console.WriteLine("Введите имя файла или напишите '~' для выхода: ");

                do {
                    fileName = Console.ReadLine();
                } while (string.IsNullOrEmpty(fileName));
                if (fileName == "~") {
                    return fileName;
                }

                if (!fileName.EndsWith(".txt")) {
                    fileName += ".txt";
                }
                fileName = $"../../../{fileName}";

                if (!File.Exists(fileName)) {
                    Console.WriteLine("Ошибка! Файл с таким именем не найден.");
                    errFlag = true;
                }
                else {
                    StreamReader reader = new StreamReader(fileName);
                    string? line;
                    string endSymbol = "//";
                    List<double> values = [];
                    while (!reader.EndOfStream) {
                        line = reader.ReadLine();
                        if (!string.IsNullOrEmpty(line)) {

                            if (line.Trim().StartsWith(endSymbol)) {
                                break; // Stop reading if the symbol is encountered.
                            }

                            if (double.TryParse(line, out double value)) {
                                Console.WriteLine($"{value}");
                            }
                            else {
                                errFlag = true;
                            }
                        }
                        else {
                            errFlag = true;
                        }
                    }
                    reader.Close();
                }
            } while (errFlag);
            return fileName;
        }
        public static List<double> FileDownload(string fileName) {
            List<double> array = [];

            using StreamReader reader = new(fileName);
            string? line;
            string endSymbol = "//";

            while (!reader.EndOfStream) {
                line = reader.ReadLine();
                if (!string.IsNullOrEmpty(line)) {
                    if (line.Trim().StartsWith(endSymbol)) {
                        break;
                    }
                    string valueString = line.Replace(',', '.');

                    double value = double.Parse(valueString);

                    array.Add(value);
                    Console.WriteLine($"{value}");
                }
            }
            reader.Close();
            return array;
        }
    }
}
