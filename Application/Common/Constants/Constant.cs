namespace Application.Common.Constants;

public class Constant
{
    public static string[] TypeMoneySpendings = new[]
        { "Одежда", "Обувь", "Лекарства", "Электронная техника", "Подарки", "Абонемент на занятие спортом","Путишествие", "Прочее" };

    public static string[] TypeMoneySpendingsForAuto = new[] { "ТО", "Резина", "Заправка" };

    public static Dictionary<int, string> Months = new Dictionary<int, string>()
    {
        { 1, "Январь" },
        { 2, "Февраль" },
        { 3, "Март" },
        { 4, "Апрель" },
        { 5, "Май" },
        { 6, "Июнь" },
        { 7, "Июль" },
        { 8, "Август" },
        { 9, "Сентябрь" },
        { 10, "Октябрь" },
        { 11, "Ноябрь" },
        { 12, "Декабрь" }
    };
}