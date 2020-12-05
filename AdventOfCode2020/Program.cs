using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            #region File Paths

            string filePathDay1 = @"C:\Users\Filip\source\repos\AdventOfCode2020\AdventOfCode2020\Inputs\Day1.txt";
            string filePathDay2 = @"C:\Users\Filip\source\repos\AdventOfCode2020\AdventOfCode2020\Inputs\Day2.txt";
            string filePathDay3 = @"C:\Users\Filip\source\repos\AdventOfCode2020\AdventOfCode2020\Inputs\Day3.txt";
            //string filePathDay3E = @"C:\Users\Filip\source\repos\AdventOfCode2020\AdventOfCode2020\Inputs\Day3E.txt";
            string filePathDay4 = @"C:\Users\Filip\source\repos\AdventOfCode2020\AdventOfCode2020\Inputs\Day4.txt";
            //string filePathDay4E = @"C:\Users\Filip\source\repos\AdventOfCode2020\AdventOfCode2020\Inputs\Day4E.txt";
            string filePathDay5 = @"C:\Users\Filip\source\repos\AdventOfCode2020\AdventOfCode2020\Inputs\Day5.txt";

            #endregion

            #region Console Write Lines

            #region Day 1

            Console.WriteLine($" Day 1 Challenge 1: {GetRightNumber(ConvertTextFileToIntList(filePathDay1))}");
            Console.WriteLine($" Day 1 Challenge 2: {ReturnRightNumber(ConvertTextFileToIntList(filePathDay1))}");

            #endregion

            #region Day 2

            Console.WriteLine($" Day 2 Challenge 1: {CalculateNumberOfValidPasswords(filePathDay2)}");
            Console.WriteLine($" Day 2 Challenge 2: {CalculateNumberOfValidPasswordsAgain(filePathDay2)}");

            #endregion

            #region Day 3

            Console.WriteLine($" Day 3 Challenge 1: {GetNumberOfTreesInTheWay(ConvertTextFileToStringList(filePathDay3))}");
            Console.WriteLine($" Day 3 Challenge 2: {GetProductOfTreesInTheWay(ConvertTextFileToStringList(filePathDay3))}");

            #endregion

            #region Day 4

            Console.WriteLine($" Day 4 Challenge 1: {GetNumberOfValidPassports(ConvertTextFileToStringList2(filePathDay4))}");
            Console.WriteLine($" Day 4 Challenge 2: {GetNumberOfTrulyValidPassports(ConvertTextFileToStringList2(filePathDay4))}");

            #endregion

            #region Day 5

            Console.WriteLine($" Day 5 Challenge 1: {GetHighestBoardingID(ConvertTextFileToStringList(filePathDay5))}");
            Console.WriteLine($" Day 5 Challenge 2: {GetMyBoardingID(ConvertTextFileToStringList(filePathDay5))}");

            #endregion

            #endregion
        }

        #region Helper Functions

        static List <int> ConvertTextFileToIntList(string filePath)
        {
            List <int> inputList = new List<int>();
            string [] inputFile = File.ReadAllLines(filePath);
            
            foreach (string rawEntry in inputFile)
            {
                inputList.Add(int.Parse(rawEntry));
            }

            return inputList;
        }

        static List<string> ConvertTextFileToStringList(string filePath)
        {
            string[] inputFile = File.ReadAllLines(filePath);
            List<string> inputList = new List<string>(inputFile);
            return inputList;
        }

        static List<string> ConvertTextFileToStringList2(string filePath)
        {
            string[] inputFile = File.ReadAllLines(filePath);
            List<string> inputList = new List<string>(inputFile);
            List<string> formattedInputList = new List<string>();
            int count = 0;

            for (int i = 0; i < inputList.Count; i++)
            {
                if (inputList[i].Length == 0 || i == inputList.Count - 1)
                {
                    string fullPassportInfo = "";

                    for (int j = count; j < i+1; j++)
                    {
                        fullPassportInfo += $"{inputList[j]} ";
                        count++;
                    }

                    formattedInputList.Add(fullPassportInfo);
                    //count++;
                }
            }
            //foreach (var passport in formattedInputList)
            //{
            //    Console.WriteLine($"{passport} är {passport.Length} långt");
            //}

            return formattedInputList;
        }

        #endregion

        #region Day 1

        static int GetRightNumber(List <int> input) 
        {
            int controlNumber = 0;
            int correctSum = 2020;
            foreach (int entry in input)
            {
                controlNumber = correctSum - entry;
                if (input.Contains(controlNumber))
                {
                    return controlNumber*entry;
                }
            }
            return 0;
        }

        static BigInteger ReturnRightNumber(List <int> input)
        {
            foreach (int entry in input)
            {
                for (int i = input.IndexOf(entry); i < input.Count - (input.IndexOf(entry)); i++)
                {
                    int lastNumber = 2020 - (input[i] + entry);
                    if (input.Contains(lastNumber))
                    {
                        return lastNumber * entry * input[i];
                    }
                }
            }
            return 0;
        }

        #endregion

        #region Day 2

        static int CalculateNumberOfValidPasswords(string filePath) 
        {
            List <string> passwords= ConvertTextFileToStringList(filePath);
            int numberOfValidPasswords = 0;
            foreach (string password in passwords)
            {
                string [] separatedPassword = password.Split("-");
                int minOccurrence = int.Parse(separatedPassword[0]);

                string [] finalseparatedPassword = separatedPassword[1].Split(" ");
                int maxOccurrence = int.Parse(finalseparatedPassword[0]);
                char passwordValidatorCharacter = finalseparatedPassword[1][0];
                string cleanPassword = finalseparatedPassword[2];

                if (IsPasswordValid(minOccurrence, maxOccurrence, passwordValidatorCharacter, cleanPassword)) 
                {
                    numberOfValidPasswords++;
                }

            }

            return numberOfValidPasswords;
        }

        static bool IsPasswordValid(int min, int max, char characterToCheck, string password) 
        {
            int occurrencesOfCharacter = 0;

            foreach (char character in password)
            {
                if (character == characterToCheck)
                {
                    occurrencesOfCharacter++;
                }
            }

            if (occurrencesOfCharacter >= min && occurrencesOfCharacter <= max)
            {
                return true;
            }

            return false;
        }

        static int CalculateNumberOfValidPasswordsAgain(string filePath)
        {
            List<string> passwords = ConvertTextFileToStringList(filePath);
            int numberOfValidPasswords = 0;
            foreach (string password in passwords)
            {
                string[] separatedPassword = password.Split("-");
                int firstPosition = int.Parse(separatedPassword[0]);

                string[] finalseparatedPassword = separatedPassword[1].Split(" ");
                int secondPosition = int.Parse(finalseparatedPassword[0]);
                char passwordValidatorCharacter = finalseparatedPassword[1][0];
                string cleanPassword = finalseparatedPassword[2];

                if (IsPasswordValidAccordingToToboggan(firstPosition-1, secondPosition-1, passwordValidatorCharacter, cleanPassword))
                {
                    numberOfValidPasswords++;
                }

            }

            return numberOfValidPasswords;
        }

        static bool IsPasswordValidAccordingToToboggan(int firstPosition, int secondPosition, char characterToCheck, string password)
        {
            if (password[firstPosition] == characterToCheck && password[secondPosition] != characterToCheck)
            {
                return true;
            }

            if (password[firstPosition] != characterToCheck && password[secondPosition] == characterToCheck)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region Day 3

        static int GetNumberOfTreesInTheWay(List <string> input) 
        {
            int numberOfTreesInTheWay = 0;
            int columnPosition = 0;

            for (int i = 0; i < input.Count; i++)
            {
                char potentialTree = input[i][columnPosition];
                columnPosition += 3;

                if (columnPosition > input[i].Length - 1)
                {
                    columnPosition -= input[i].Length;
                }

                if (potentialTree == "#"[0]) 
                {
                    numberOfTreesInTheWay++;
                }
            }

            return numberOfTreesInTheWay;
        }

        static int GetNumberOfTreesInTheWayInSpecifiedRoute(List<string> input, int stepsToTheRight, int stepsDown)
        {
            int numberOfTreesInTheWay = 0;
            int columnPosition = 0;

            for (int i = 0; i < input.Count; i += stepsDown)
            {
                char potentialTree = input[i][columnPosition];
                columnPosition += stepsToTheRight;

                if (columnPosition > input[i].Length - 1)
                {
                    columnPosition -= input[i].Length;
                }

                if (potentialTree == "#"[0])
                {
                    numberOfTreesInTheWay++;
                }
            }

            return numberOfTreesInTheWay;
        }

        static BigInteger GetProductOfTreesInTheWay(List<string> input)
        {
            int numberOfTreesInRoute1 = GetNumberOfTreesInTheWayInSpecifiedRoute(input, 1, 1);
            int numberOfTreesInRoute2 = GetNumberOfTreesInTheWay(input);
            int numberOfTreesInRoute3 = GetNumberOfTreesInTheWayInSpecifiedRoute(input, 5, 1);
            int numberOfTreesInRoute4 = GetNumberOfTreesInTheWayInSpecifiedRoute(input, 7, 1);
            int numberOfTreesInRoute5 = GetNumberOfTreesInTheWayInSpecifiedRoute(input, 1, 2);

            return numberOfTreesInRoute1 * numberOfTreesInRoute2 * numberOfTreesInRoute3 * numberOfTreesInRoute4 * numberOfTreesInRoute5;
        }

        #endregion

        #region Day 4

        static bool IsPassportValid(string passportInfo)
        {
            string [] passportDetailsRaw = passportInfo.Split(" ");
            List<string> passportDetails = new List<string>();
            foreach (var item in passportDetailsRaw)
            {
                if (item.Length != 0)
                {
                    passportDetails.Add(item);
                }
            }
            if (passportDetails.Count == 8)
            {
                return true;
            }
            else if (passportDetails.Count == 7)
            {
                foreach (string entry in passportDetails)
                {
                    if (entry.StartsWith("c"))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        static int GetNumberOfValidPassports(List <string> input) 
        {
            int numberOfValidPassports = 0;
            foreach (string passport in input)
            {
                if (IsPassportValid(passport) == true)
                {
                    numberOfValidPassports++;
                }
            }
            return numberOfValidPassports;
        }

        static bool IsPassportTrulyValid(string passportInfo)
        {
            string[] passportDetailsRaw = passportInfo.Split(" ");
            List<string> passportDetails = new List<string>();

            foreach (var item in passportDetailsRaw)
            {
                if (item.Length != 0)
                {
                    passportDetails.Add(item);
                }
            }
            foreach (var detail in passportDetails)
            {
                if (detail.StartsWith("byr:"))
                {
                    int specifiedIndex = passportDetails.IndexOf(detail);
                    string[] birthYearRaw = passportDetails[specifiedIndex].Split(":");
                    int birthYear = int.Parse(birthYearRaw[1]);
                    if (birthYear >= 1920 && birthYear <= 2002)
                    {
                        //ok
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (detail.StartsWith("iyr:"))
                {
                    int specifiedIndex = passportDetails.IndexOf(detail);
                    string[] issueYearRaw = passportDetails[specifiedIndex].Split(":");
                    int issueYear = int.Parse(issueYearRaw[1]);
                    if (issueYear >= 2010 && issueYear <= 2020)
                    {
                        //ok
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (detail.StartsWith("eyr:"))
                {
                    int specifiedIndex = passportDetails.IndexOf(detail);
                    string[] expirationYearRaw = passportDetails[specifiedIndex].Split(":");
                    int expirationYear = int.Parse(expirationYearRaw[1]);
                    if (expirationYear >= 2020 && expirationYear <= 2030)
                    {
                        //ok
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (detail.StartsWith("hgt:"))
                {
                    int specifiedIndex = passportDetails.IndexOf(detail);
                    string[] heightRaw = passportDetails[specifiedIndex].Split(":");
                    if (heightRaw[1].EndsWith("cm"))
                    {
                        char[] measurement = { "c"[0], "m"[0] };
                        int height = int.Parse(heightRaw[1].Trim(measurement));
                        if (height >= 150 && height <= 193)
                        {
                            //ok
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        char[] measurement = { "i"[0], "n"[0] };
                        int height = int.Parse(heightRaw[1].Trim(measurement));
                        if (height >= 59 && height <= 76)
                        {
                            //ok
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else if (detail.StartsWith("hcl:"))
                {
                    int specifiedIndex = passportDetails.IndexOf(detail);
                    string[] hairColorRaw = passportDetails[specifiedIndex].Split(":");
                    string hairColor = hairColorRaw[1];
                    if (hairColor.StartsWith("#") && hairColor.Length == 7)
                    {
                        List <char> correctValues = new List<char>();
                        //List<char> correctValues2 = new List<char>();
                        //int numberOfCorrectValues1 = 0;
                        //int numberOfCorrectValues2 = 0;

                        for (int i = 0; i < 10; i++)
                        {
                            correctValues.Add(i.ToString()[0]);
                        }
                        correctValues.Add("a"[0]);
                        correctValues.Add("b"[0]);
                        correctValues.Add("c"[0]);
                        correctValues.Add("d"[0]);
                        correctValues.Add("e"[0]);
                        correctValues.Add("f"[0]);

                        for (int i = 1; i < hairColor.Length-1; i++)
                        {
                            if (!correctValues.Contains(hairColor[i]))
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (detail.StartsWith("ecl:"))
                {
                    int specifiedIndex = passportDetails.IndexOf(detail);
                    string[] eyeColorRaw = passportDetails[specifiedIndex].Split(":");
                    string eyeColor = eyeColorRaw[1];
                    string[] correctValuesRaw = {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
                    List<string> correctValues = new List<string>();
                    foreach (string value in correctValuesRaw)
                    {
                        correctValues.Add(value);
                    }
                    if (!correctValues.Contains(eyeColor))
                    {
                        return false;
                    }
                }
                else if (detail.StartsWith("pid:"))
                {
                    int specifiedIndex = passportDetails.IndexOf(detail);
                    string[] passportIdRaw = passportDetails[specifiedIndex].Split(":");
                    string passportId = passportIdRaw[1];
                    char [] correctValuesRaw = { "0"[0], "1"[0], "2"[0], "3"[0], "4"[0], "5"[0], "6"[0], "7"[0], "8"[0], "9"[0]};
                    List<char> correctValues = new List<char>();
                    foreach (char value in correctValuesRaw)
                    {
                        correctValues.Add(value);
                    }
                    if (passportId.Length == 9)
                    {
                        foreach (var number in passportId)
                        {
                            if (!correctValues.Contains(number))
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static int GetNumberOfTrulyValidPassports(List<string> input)
        {
            int numberOfValidPassports = 0;
            foreach (string passport in input)
            {
                if (IsPassportValid(passport) == true && IsPassportTrulyValid(passport) == true)
                {
                    numberOfValidPassports++;
                    //Console.WriteLine(passport);
                }
            }
            return numberOfValidPassports;
        }

        #endregion

        #region Day 5

        static List <int> CalculateBoardingIDs(List <string> input) 
        {
            List<int> boardingIDs = new List<int>();

            for (int i = 0; i < input.Count; i++)
            {
                int finalRow = 0;
                int minRow = 0;
                int maxRow = 127;

                int finalColumn = 0;
                int minColumn = 0;
                int maxColumn = 7;

                for (int j = 0; j < 6; j++)
                {
                    if (input[i][j] == "F"[0])
                    {
                        maxRow = (int)Math.Floor((double)(maxRow + minRow) / 2);
                    }
                    else
                    {
                        minRow = (int)Math.Ceiling((double)(maxRow + minRow) / 2);
                    }
                }
                if (input[i][6] == "F"[0])
                {
                    finalRow = minRow;
                }
                else
                {
                    finalRow = maxRow;
                }
                for (int j = 7; j < 9; j++)
                {
                    if (input[i][j] == "L"[0])
                    {
                        maxColumn = (int)Math.Floor((double)(maxColumn + minColumn) / 2);
                    }
                    else
                    {
                        minColumn = (int)Math.Ceiling((double)(maxColumn + minColumn) / 2);
                    }
                }
                if (input[i][9] == "L"[0])
                {
                    finalColumn = minColumn;
                }
                else
                {
                    finalColumn = maxColumn;
                }
                int boardingID = (finalRow * 8) + finalColumn;
                boardingIDs.Add(boardingID);
            }
            return boardingIDs;
        } 

        static int GetHighestBoardingID(List<string> input) 
        {
            List<int> boardingIDs = new List<int>(CalculateBoardingIDs(input));
            int highestBoardingID = 0;
            for (int i = 0; i < boardingIDs.Count; i++)
            {
                if (boardingIDs[i] > highestBoardingID)
                {
                    highestBoardingID = boardingIDs[i];
                }
            }
            return highestBoardingID;
        }

        static int GetMyBoardingID(List<string> input) 
        {
            List <int> boardingIDs = new List <int> (CalculateBoardingIDs(input));
            int myBoardingID = 0;

            for (int i = 0; i < boardingIDs.Count; i++)
            {
                if (!boardingIDs.Contains(boardingIDs[i] + 1) && boardingIDs.Contains(boardingIDs[i] + 2))
                {
                    return boardingIDs[i]+1;
                }
                else if (!boardingIDs.Contains(boardingIDs[i] - 1) && boardingIDs.Contains(boardingIDs[i] - 2))
                {
                    return boardingIDs[i]-1;
                }
            }

            return myBoardingID;
        }

        #endregion
    }
}
