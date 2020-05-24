using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.IO;
namespace Lab7_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Key.PKEY();
        }
    }
    class Notebook : IComparable
    {
        public string Lastname;
        public string Name;
        public string Surname;
        public DateTime Birthday;
        public int Phone;
        public string Mail;
        public Notebook(string lastname, string name, string surname, DateTime birthday, int phone, string mail)
        {
            Lastname = lastname;
            Name = name;
            Surname = surname;
            Birthday = birthday;
            Phone = phone;
            Mail = mail;
        }
        public int CompareTo(object notebooks)
        {
            Notebook p = (Notebook)notebooks;
            if (this.Birthday > p.Birthday) return 1;
            if (this.Birthday < p.Birthday) return -1;
            return 0;
        }
        public void HappyB(Notebook[] a)
        {
            Console.WriteLine("\nСортування за Датою Народження:\nПрiзвище\t Дата Народження");
            Array.Sort(a);
            foreach (Notebook elem in a) elem.Inf();
        }
        public void Inf()
        {
            Console.WriteLine("{0,-20} {1, -10} ", Lastname, Birthday);
        }

        public class SortByBirthday : IComparer
        {
            int IComparer.Compare(object ob1, object ob2)
            {
                Notebook p1 = (Notebook)ob1;
                Notebook p2 = (Notebook)ob2;
                if (p1.Birthday > p2.Birthday) return 1;
                if (p1.Birthday < p2.Birthday) return -1;
                return 0;
            }
        }
        public void First(Notebook[] a)
        {
            Console.WriteLine("\nСортування за Датою Народження:\nПрiзвище\t Дата Народження");
            Array.Sort(a, new Notebook.SortByBirthday());
            foreach (Notebook elem in a) elem.Info();
        }
        public void Info()
        {
            Console.WriteLine("{0,-20} {1, -10} ", Lastname, Birthday);
        }
        public class SortByPhone : IComparer
        {
            int IComparer.Compare(object ob1, object ob2)
            {
                Notebook p1 = (Notebook)ob1;
                Notebook p2 = (Notebook)ob2;
                if (p1.Phone > p2.Phone) return 1;
                if (p1.Phone < p2.Phone) return -1;
                return 0;
            }
        }
        public void Second(Notebook[] a)
        {
            Console.WriteLine("\nСортування за номером телефону:\nПрiзвище\t Номер телефону:");
            Array.Sort(a, new Notebook.SortByPhone());
            foreach (Notebook elem in a) elem.Info1();
        }
        public void Info1()
        {
            Console.WriteLine("{0,-20} {1, -10} ", Lastname, Phone);
        }
        public void Add()
        {
            Console.WriteLine("Запишiть:");

            string str = Console.ReadLine();

            string[] elements = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        }
    }
    class Key
    {
        public static void PKEY()
        {
            FileStream file1 = File.OpenRead("note.txt");
            byte[] array = new byte[file1.Length];
            file1.Read(array, 0, array.Length);
            string textfromfile = System.Text.Encoding.Default.GetString(array);
            string[] s = textfromfile.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            file1.Close();
            Notebook[] a = new Notebook[s.Length / 6];
            int c = 0;
            while (a[c] != null)
            {
                ++c;
            }
            for (int i = 0; i < s.Length; i += 6)
            {
                a[c + i / 6] = new Notebook(s[i], s[i + 1], s[i + 2], DateTime.Parse(s[i + 3]), int.Parse(s[i + 4]), s[i + 5]);
            }
            bool[] delete = new bool[100];
            Console.WriteLine("Додавання записiв: A");
            Console.WriteLine("Редагування записiви: E");
            Console.WriteLine("Знищення записiв: R");
            Console.WriteLine("Виведення записiв: Enter");
            Console.WriteLine("Сортування за Датою Народження: N");
            Console.WriteLine("Сортування за номером телефону: D");
            Console.WriteLine("Вихiд: Esc");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.E:
                    Key.Edit(a);
                    break;

                case ConsoleKey.N:
                    a[0].First(a);
                    Key.PKEY();
                    break;

                case ConsoleKey.D:
                    a[0].Second(a);
                    Key.PKEY();
                    break;

                case ConsoleKey.S:
                    a[0].HappyB(a);
                    Key.PKEY();
                    break;

                case ConsoleKey.Enter:
                    Key.Show(a);
                    break;

                case ConsoleKey.A:
                    Key.Add(a, c);
                    break;

                case ConsoleKey.R:
                    Key.Remove(a, delete);
                    break;

                case ConsoleKey.Escape:
                    break;
            }

        }
        public static void Show(Notebook[] a)
        {
            Console.WriteLine("{0,-15} {1, -15} {2, -20} {3, -25} {4,-15} {5,-15}", "Прiзвище", "Iм'я", "По батьковi", "Дата Народження", "Телефон", "Електронна пошта");

            for (int i = 0; i < a.Length; ++i)
            {
                if (a[i] != null)
                {
                    Console.WriteLine("{0,-15} {1, -15} {2, -20} {3, -25} {4,-15} {5,-15}", a[i].Lastname, a[i].Name, a[i].Surname, a[i].Birthday, a[i].Phone, a[i].Mail);
                }
            }
            Key.PKEY();
        }
        public static void Add(Notebook[] a, int c)
        {
            Console.WriteLine("\nЗапишiть:");

            string str = Console.ReadLine();

            string[] elements = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            Key.Parse(elements, true, a, c);
            Key.PKEY();
        }

        private static void Save(Notebook m)
        {
            StreamWriter save = new StreamWriter("note.txt", true);

            save.WriteLine(m.Lastname);
            save.WriteLine(m.Name);
            save.WriteLine(m.Surname);
            save.WriteLine(m.Birthday);
            save.WriteLine(m.Phone);
            save.WriteLine(m.Mail);
            save.Close();
        }

        public static void Parse(string[] elements, bool save, Notebook[] a, int counter)
        {
            for (int i = 0; i < elements.Length; i += 6)
            {
                a[counter + i / 4] = new Notebook(elements[i], elements[i + 1], elements[i + 2], DateTime.Parse(elements[i + 3]), int.Parse(elements[i + 4]), elements[i + 5]);
                if (save)
                {
                    Save(a[counter + i / 4]);
                }
            }
        }
        public static void Remove(Notebook[] a, bool[] delete)
        {
            Console.Write("\nПрiзвище: ");

            string name = Console.ReadLine();

            bool[] write = new bool[a.Length];
            for (int i = 0; i < a.Length; ++i)
            {
                if (a[i] != null)
                {
                    if (a[i].Lastname == name)
                    {
                        Console.WriteLine("{0,-15} {1, -15} {2, -20} {3, -25} {4,-15} {5,-15}", a[i].Lastname, a[i].Name, a[i].Surname, a[i].Birthday, a[i].Phone, a[i].Mail);

                        Console.WriteLine("\nDELETE? (Y/N)\n");

                        var key = Console.ReadKey().Key;

                        if (key == ConsoleKey.Y)
                        {
                            a[i] = null;
                            delete[i] = true;
                            Key.Show(a);
                        }
                        else
                        {
                            delete[i] = false;
                        }
                    }
                }
            }
            Key.PKEY();
        }
        public static void Edit(Notebook[] a)
        {
            Console.WriteLine("\nЗапишiть?(Прiзвище, Iм'я, По батьковi, Дата народження, Телефон, Електронна пошта)");
            string what = Console.ReadLine();
            switch (what)
            {
                case "Прiзвище":
                    Console.WriteLine("Введiть прiзвище: ");
                    string name1 = Console.ReadLine();
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Surname == name1)
                            {
                                Console.WriteLine("{0,-15} {1, -15} {2, -20} {3, -25} {4,-15} {5,-15}", a[i].Lastname, a[i].Name, a[i].Surname, a[i].Birthday, a[i].Phone, a[i].Mail);

                                Console.WriteLine("Нове прiзвище: ");

                                string str = Console.ReadLine();

                                a[i].Surname = str;

                                Key.Show(a);
                            }
                        }
                    }
                    break;

                case "Iм'я":
                    Console.WriteLine("Введiть iм'я: ");
                    string name2 = Console.ReadLine();
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Name == name2)
                            {
                                Console.WriteLine("{0,-15} {1, -15} {2, -20} {3, -25} {4,-15} {5,-15}", a[i].Lastname, a[i].Name, a[i].Surname, a[i].Birthday, a[i].Phone, a[i].Mail);

                                Console.WriteLine("нове iм'я: ");
                                string str = Console.ReadLine();
                                a[i].Name = str;
                                Key.Show(a);
                            }
                        }
                    }
                    break;
                case "По батьковi":
                    Console.WriteLine("Введiть по батьковi: ");
                    string name3 = Console.ReadLine();
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Surname == name3)
                            {
                                Console.WriteLine("{0,-15} {1, -15} {2, -20} {3, -25} {4,-15} {5,-15}", a[i].Lastname, a[i].Name, a[i].Surname, a[i].Birthday, a[i].Phone, a[i].Mail);
                                Console.WriteLine("нове по батькові: ");
                                string str = Console.ReadLine();
                                a[i].Surname = str;
                                Key.Show(a);
                            }
                        }

                    }
                    break;
                case "Дата Народження":
                    Console.WriteLine("Введiть Дату Народження: ");
                    DateTime name4 = DateTime.Parse(Console.ReadLine());
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Birthday == name4)
                            {
                                Console.WriteLine("{0,-15} {1, -15} {2, -20} {3, -25} {4,-15} {5,-15}", a[i].Lastname, a[i].Name, a[i].Surname, a[i].Birthday, a[i].Phone, a[i].Mail);
                                DateTime str = DateTime.Parse(Console.ReadLine());
                                a[i].Birthday = str;
                                Key.Show(a);
                            }
                        }
                    }
                    break;
                case "Телефон":
                    Console.WriteLine("Введiть телефон: ");
                    int name5 = int.Parse(Console.ReadLine());
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Phone == name5)
                            {
                                Console.WriteLine("{0,-15} {1, -15} {2, -20} {3, -25} {4,-15} {5,-15}", a[i].Lastname, a[i].Name, a[i].Surname, a[i].Birthday, a[i].Phone, a[i].Mail);
                                int str = int.Parse(Console.ReadLine());
                                a[i].Phone = str;
                                Key.Show(a);
                            }
                        }
                    }
                    break;
                case "Електронна пошта":
                    Console.WriteLine("Введiть електронну пошту: ");
                    string name6 = Console.ReadLine();
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Mail == name6)
                            {
                                Console.WriteLine("{0,-15} {1, -15} {2, -20} {3, -25} {4,-15} {5,-15}", a[i].Lastname, a[i].Name, a[i].Surname, a[i].Birthday, a[i].Phone, a[i].Mail);

                                Console.WriteLine("нова електронна пошта: ");
                                string str = Console.ReadLine();
                                a[i].Mail = str;
                                Key.Show(a);
                            }
                        }

                    }
                    break;
            }
            Key.PKEY();
        }
    }
}
