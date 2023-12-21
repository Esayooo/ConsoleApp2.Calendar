using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2.Calendar
{
    class Program
    {
        // Класс событий для представления событий
        class Event
        {
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public string Description { get; set; }
        }

        // Коллекция для хранения событий
        private static List<Event> events = new List<Event>();

        //Функция добавления событий для тестирования
        private static void AddTestData()
        {
            events.Add(new Event 
            { Name = "Meeting", Date = new DateTime(2023,1,7), Description = "Team meeting at 2 PM" });
            events.Add(new Event 
            { Name = "Birthday", Date = new DateTime(2023,2,25), Description = "John's birthday celebration" });
            
        }
        //Определить, какой день недели является первым днем ​​определенного месяца определенного года.
        private static int GetWeekByDay(int year, int month, int day)
        {
            DateTime dt = new DateTime(year, month, day);//Создает объект DateTime, представляющий указанную дату.
            return (int)dt.DayOfWeek;//Возвращает день недели в виде целого числа.
        }
        //Определите, является ли определенный год високосным.Високосный год: он делится на 4, но не делится на 100 и не делится на 400.
        private static bool IsLeapYear(int year)
        {
            if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
                return true;
            else
                return false;
        }
        //Получить количество дней в каждом месяце
        private static int GetDaysByMonth(int year, int month)
        {
            if (month < 1 || month > 12) return 0;

            switch (month)
            {
                //Если год високосный, то в феврале 29 дней.
                case 2:
                    if (IsLeapYear(year))
                        return 29;
                    else
                        return 28;
                //В апреле, июне, сентябре и ноябре тридцать дней.
                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
                //В остальных месяцах 31 день.
                default:
                    return 31;
            }
        }
        //Распечатать ежемесячный календарь
        private static void PrintMonthCalendar(int year, int month)
        {
            Console.WriteLine("{0}year {1}month", year, month);
            Console.WriteLine("Sun\tMon\tTues\tWed\tThur\tFri\tSat");
            //Какой день недели является первым днем ​​определенного месяца определенного года?
            int week = GetWeekByDay(year, month, 1);
            //Добавляйте пробелы перед первым днем ​​каждого месяца, пока оно не совпадет с соответствующим количеством недель.
            for (int i = 0; i < week; i++)
                Console.Write("\t");

            int days = GetDaysByMonth(year, month);
            //Определив день недели, на который приходится первый день, добавляйте пробел через день, начиная с первого дня.
            for (int i = 1; i <= days; i++)
            {
                //Существует разрыв между каждым днем
                Console.Write(i + "\t");

                // Распечатать события дня
                PrintEvents(year, month, i);
                //Меняйте очередь каждую субботу
                if (GetWeekByDay(year, month, i) == 6)
                    Console.WriteLine();
            }
        }
        //Объявляет частный статический метод с именем PrintEvents для отображения событий за определенный день.
        private static void PrintEvents(int year, int month, int day)
        {
            //Создает объект DateTime для текущего дня.
            DateTime currentDate = new DateTime(year, month, day);
            //Фильтрует события на текущий день.
            var dayEvents = events.Where(e => e.Date.Date == currentDate.Date);

            //Перебирает события дня и печатает их подробную информацию.
            foreach (var ev in dayEvents)
            {
                Console.WriteLine($"{ ev.Date.ToShortDateString()} -{ev.Name}: {ev.Description}");
            }
        }
        //Вызовите ежемесячный календарь 12 раз.
        private static void PrintYearCaledar(int year)
        {
            for (int i = 1; i <= 12; i++)
            {
                PrintMonthCalendar(year, i);
                Console.WriteLine();
            }
        }
        
        //основная функция
        static void Main()
        {
            Console.WriteLine("Please enter the year:");
            int year = int.Parse(Console.ReadLine());

            // Добавьте тестовые данные (события) перед печатью календаря
            AddTestData();
            //Разрешить пользователям вводить события
            Console.WriteLine("Do you want to add an event? (yes/no)");
            string addEventResponse = Console.ReadLine().ToLower();
            while (addEventResponse == "yes")
            {
                Console.WriteLine("Enter event name:");
                string eventName = Console.ReadLine();

                Console.WriteLine("Enter event date (yyyy-MM-dd):");
                DateTime eventDate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter event description:");
                string eventDescription = Console.ReadLine();

                events.Add(new Event { Name = eventName, Date = eventDate, Description = eventDescription });

                Console.WriteLine("Event added successfully. Do you want to add another event? (yes/no)");
                addEventResponse = Console.ReadLine().ToLower();
            }
            

            PrintYearCaledar(year);
        }
    }
}

