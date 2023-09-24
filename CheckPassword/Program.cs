using System.Text.RegularExpressions;

var flagLength = false;
var flagUpperLetter = false;
var flagLowerLetter = false;
var flagDigit = false;
var flagSymbol = false;

Console.Write("Введите пароль: ");
var password = Console.ReadLine();

Check(
    predicate: p => p.Length is > 8 and < 20,
    password: password,
    actionTrue: () => {
        flagLength = true;
        PrintInfo("Символов от 8 до 20");
    },
    actionFalse: () =>
    {
        PrintError("Меньше 8 или больше 20 символов");
    });

Check(
    predicate: p => Regex.IsMatch(p, "[A-Z]+"),
    password: password,
    actionTrue: () => {
        flagUpperLetter = true;
        PrintInfo("Есть заглавная буква");
    },
    actionFalse: () =>
    {
        PrintError("Нет заглавной буквы");
    });

Check(
    predicate: p => Regex.IsMatch(p, "[a-z]+"),
    password: password,
    actionTrue: () => {
        flagLowerLetter = true;
        PrintInfo("Есть строчная буква");
    },
    actionFalse: () =>
    {
        PrintError("Нет строчной буквы");
    });

Check(
    predicate: p => Regex.IsMatch(p, "[0-9]+"),
    password: password,
    actionTrue: () => {
        flagDigit = true;
        PrintInfo("Есть цифра");
    },
    actionFalse: () =>
    {
        PrintError("Нет цифры");
    });

Check(
    predicate: p => Regex.IsMatch(p, @"[!?.,:;\-+=_*%$#@&^]+"),
    password: password,
    actionTrue: () => {
        flagSymbol = true;
        PrintInfo("Есть спец. символ");
    },
    actionFalse: () =>
    {
        PrintError("Нет спец. символа");
    });

if (flagLength && flagUpperLetter && flagLowerLetter && flagDigit && flagSymbol)
{
    PrintInfo("Пароль соответствует всем требованиям");
}
else
{
    PrintError("Пароль не соответствует всем требованиям");
}

return;

void PrintInfo(string message)
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine(message);
    Console.ResetColor();
}

void PrintError(string message)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(message);
    Console.ResetColor();
}

void Check(Predicate<string> predicate, string password, Action actionTrue, Action actionFalse)
{
    if (predicate(password))
    {
        actionTrue();
    }
    else
    {
        actionFalse();
    }
}