using System;
using Characters;

namespace ConsoleApp1
{
    class Program
    {
        const int N = 10;
        private static Random rand = new Random();
        static void Main()
        {
            do
            {
                Console.WriteLine("Введите 1, чтобы собрать команду случайно\n" +
                    "Введите 2, чтобы выбрать команду самостоятельно на 10 единиц валюты");
                int choice;
                Console.Write("Введите одну из команд: ");
                choice = ImpN();
                while (choice != 1 && choice != 2)
                {
                    Console.Write("Такого вариант не существует. \nПовторите ввод: ");
                    choice = ImpN();
                }

                string players = "\nВаша команда:\n";
                string playersAI = "Команда AI: \n";
                //Команды компьютера и игрока, соответственно
                Fighter[] teamAI = RandomTeam(ref playersAI);
                Fighter[] teamPlayer;
                if (choice == 1)
                    teamPlayer = RandomTeam(ref players);
                else
                    teamPlayer = DiferentTeam(ref players);

                Console.WriteLine(players + "\n");
                Console.WriteLine(playersAI);
                Console.WriteLine();

                int coin = rand.Next(1, 3); // Опеределение первого хода
                if (coin == 1) Console.WriteLine("Первый ход за вами");
                else Console.WriteLine("Вы ходите вторым");
                Console.Write("Нажмите любую клавишу, чтобы начать\nГотовы начать битву?\n");
                Console.ReadKey();

                //Переменные для подсчета количество живых персонажей
                int playerAlive = teamPlayer.Length, aiAlive = 10;

                while (playerAlive > 0 && aiAlive > 0)
                {
                    if (coin == 1)
                    {
                        Attack(ref teamPlayer, ref teamAI, ref playerAlive, ref aiAlive, "Player");
                        Console.WriteLine($"Player {playerAlive} VS {aiAlive} AI \n");
                    }
                    else
                    {
                        Attack(ref teamAI, ref teamPlayer, ref aiAlive, ref playerAlive, "AI");
                        Console.WriteLine($"Player {playerAlive} VS {aiAlive} AI \n");
                    }
                    if (aiAlive == 0)
                        Console.WriteLine("Вы победили!");
                    else if (playerAlive == 0)
                        Console.WriteLine("Вы проиграли!");
                    if (coin == 1) coin++;
                    else coin--;
                    
                }
                // Повтор решения
                Console.WriteLine("Нажмите esc, чтобы выйти, или другую клавишу, чтобы начать новую игру");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// Метод проводит аттаку одного рандомного персонажа на другого рандомного персонажа
        /// </summary>
        /// <param name="ally">Массив с персонажами атакующего</param>
        /// <param name="enemy">Массив с персонажами обороняющегося</param>
        /// <param name="playerAlive">Количество живых персонажей атакующего</param>
        /// <param name="aiAlive">Количество живых персонажей обороняющегося</param>
        /// <param name="name">Назване команды атакующего</param>
        static void Attack(ref Fighter[] ally, ref Fighter[] enemy, ref int playerAlive, ref int aiAlive, string name)
        {
            
            double damage; // Нанесенный урон
            // Генерация номеров персонажей
            int playerNum = rand.Next(0, ally.Length);
            int enemyNum = rand.Next(0, enemy.Length);
            ally[playerNum].Attack(enemy[enemyNum]);
            damage = ally[playerNum].TotalDamage;
            // Вывод информационного сообщения
            Console.Write("Из команды {0} {1} атаковал {2} и нанес {3} урона. ", name,
            ally[playerNum].ToString(), enemy[enemyNum].ToString(), damage);
            ally[playerNum].BattleCry();
            // "Удаление" мертвого персонажа при необходимости
            if (!ally[playerNum].IsAlive())
            {
                Console.WriteLine("********{0} погиб********", ally[playerNum].ToString());
                playerAlive--;

                if (ally.Length > 0)
                {
                    ally[playerNum] = ally[ally.Length - 1];
                    Array.Resize(ref ally, ally.Length - 1);
                }
            }
            if (!enemy[enemyNum].IsAlive())
            {
                Console.WriteLine("********{0} погиб********", enemy[enemyNum].ToString());
                aiAlive--;

                if (enemy.Length > 1)
                {
                    enemy[enemyNum] = enemy[enemy.Length - 1];
                    Array.Resize(ref enemy, enemy.Length - 1);
                }
            }
        }

        /// <summary>
        /// Метод составляет команду в зависимости от команд пользователя
        /// </summary>
        /// <param name="players">Состав команды</param>
        /// <returns>Заполненный персонажами массив</returns>
        static Fighter[] DiferentTeam(ref string players)
        {
            // Переменные для посчета количества персонажей
            int ninja = 0, samurai = 0, fighter = 0;
            // Вывод информационного сообщения
            Console.WriteLine("\n1. Боец: 1 единица\n2. Ниндзя: 1.5 единицы\n3. Самурай: 2 единицы\n");
            Console.Write("У вас есть 10 единиц валюты\nВведите одну из команд: ");
            double points = 10.0; // Количество едини валюты
            int i = 0; // Общее количество персонажей
            Fighter[] team = new Fighter[10];
            int choice;
            while (points >= 1)
            {
                choice = ImpN(); //Ввод команды игроком
                while (choice != 1 && choice != 2 && choice != 3)
                {
                    Console.Write("Такого вариант нет. \nПовторите ввод: ");
                    choice = ImpN();
                }
                while (points == 1 && choice != 1)
                {
                    Console.Write("У вас не хватает валюты. \nПовторите ввод: ");
                    choice = ImpN();
                }
                while (points == 1.5 && choice == 3)
                {
                    Console.Write("У вас не хватает валюты. \nПовторите ввод: ");
                    choice = ImpN();
                }
                // В зависимости от команды генерируется соответствующий персонаж и уменьшается количество валюты
                switch (choice)
                {
                    case 1:
                        team[i] = new Fighter(rand.Next(50, 71), rand.Next(5, 11), rand.Next(3, 7), 0);
                        i++;
                        fighter++;
                        points -= 1.0;
                        break;
                    case 2:
                        team[i] = new Ninja(rand.Next(60, 76), rand.Next(8, 16), rand.Next(4, 6),
                        rand.NextDouble() * 0.2 + 0.4, rand.NextDouble() * 0.3 + 0.3);
                        i++;
                        ninja++;
                        points -= 1.5;
                        break;
                    case 3:
                        team[i] = new Samurai(rand.Next(70, 86), rand.Next(7, 13), rand.Next(4, 7),
                    rand.NextDouble() * 0.2 + 0.3, rand.NextDouble() * 0.2 + 0.3);
                        i++;
                        samurai++;
                        points -= 2;
                        break;
                    default:
                        break;
                }
                if (points >= 1)
                    Console.Write($"У вас осталось {points} единиц валюты\nВведите одну из команд: ");
            }
            // Составление строки с информацией о команде
            players += $"Бойцы: {fighter}\nНиндзя: {ninja}\nСамураи: {samurai}";
            Array.Resize(ref team, i);
            return team;
        }

        /// <summary>
        /// Метод заполняет массив 10 случайными персонажами по соответствующий вероятностям 
        /// </summary>
        /// <param name="players">Строка с информацией о составе команды</param>
        /// <returns>Заполненный персонажами массив</returns>
        static Fighter[] RandomTeam(ref string players)
        {
            int ninja = 0, samurai = 0, fighter = 0;
            Fighter[] team = new Fighter[N];
            for (int i = 0; i < N; i++)
            {
                double r = rand.NextDouble();
                if (r < 0.45)
                {
                    fighter++;
                    team[i] = new Fighter(rand.Next(50, 71), rand.Next(5, 11), rand.Next(3, 7), 0);
                }
                else
                    if (r < 0.75)
                    {
                        ninja++;
                        team[i] = new Ninja(rand.Next(60, 76), rand.Next(8, 16), rand.Next(4, 6),
                            rand.NextDouble() * 0.2 + 0.4, rand.NextDouble() * 0.3 + 0.3);
                    }
                else
                {
                    samurai++;
                    team[i] = new Samurai(rand.Next(70, 86), rand.Next(7, 13), rand.Next(4, 7),
                      rand.NextDouble() * 0.2 + 0.3, rand.NextDouble() * 0.2 + 0.3);
                }
            }
            players += $"Бойцы: {fighter}\nНиндзи: {ninja}\nСамураи: {samurai}";
            return team;
        }

        static int ImpN()
        {
            int n=0;
            int i = 0;
            while (true)
            {
                try
                {
                    if (i > 0) Console.Write("Повторите ввод: ");
                    else i = 1;
                    n = int.Parse(Console.ReadLine());
                }
                catch (OverflowException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка в формате данных!");
                    continue;
                }
                catch (ArithmeticException ex)
                {
                    Console.WriteLine("ex.Message=" + ex.Message);
                    continue;
                }
                break;
            }
            return n;
        }
    }
}