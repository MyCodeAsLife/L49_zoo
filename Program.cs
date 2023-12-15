using System;
using System.Collections.Generic;

namespace L49_zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            ApplicationZoo myZoo = new ApplicationZoo(random);

            myZoo.Run();
        }
    }

    class Error
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("Вы ввели некорректное значение.");
        }
    }

    class FormaOutput
    {
        public const int DelimeterLenght = 75;
        public const char DelimeterSymbol = '-';
    }

    class ApplicationZoo
    {
        private Random _random = new Random();
        private List<Aviary> _aviaries = new List<Aviary>();
        private int _maxCapacityAviary = 12;

        public ApplicationZoo(Random random)
        {
            _random = random;
            FillZoo();
        }

        public void Run()
        {
            bool isOpen = true;

            while (isOpen)
            {
                Console.Clear();

                Console.WriteLine($"Добро пожаловать в наш зоопарк!\n" + new string(FormaOutput.DelimeterSymbol, FormaOutput.DelimeterLenght) + $"\n" +
                                  $"{(int)Menu.ChooseSavannahAviary + 1} - Подойти к саванному вальеру.\n{(int)Menu.ChooseDesertAviary + 1} - " +
                                  $"Подойти к пустынному вальеру.\n{(int)Menu.ChooseForestAviary + 1} - Подойти к лесному вальеру.\n" +
                                  $"{(int)Menu.ChooseMeadowAviary + 1} - Подойти к луговому вальеру.\n{(int)Menu.ChooseExit + 1} - " +
                                  $"Выйти из программы.\n" + new string(FormaOutput.DelimeterSymbol, FormaOutput.DelimeterLenght));

                Console.Write("Выберите пункт меню: ");

                if (int.TryParse(Console.ReadLine(), out int menuItem))
                {
                    Console.Clear();
                    menuItem--;

                    if (menuItem < (int)Menu.Max || menuItem >= 0)
                    {
                        if ((Menu)menuItem == Menu.ChooseExit)
                            isOpen = false;
                        else
                            ApproachEnclosure(menuItem);
                    }
                    else
                    {
                        Error.Show();
                    }
                }
                else
                {
                    Error.Show();
                }

                Console.WriteLine(new string(FormaOutput.DelimeterSymbol, FormaOutput.DelimeterLenght) + "\nДля продолжения нажмите любую кнопку...");
                Console.ReadKey(true);
            }
        }

        private void ApproachEnclosure(int menuItem)
        {
            _aviaries[menuItem].ShowInfo();
        }

        private void FillZoo()
        {
            _aviaries.Add(new Savannah(_random, _maxCapacityAviary));
            _aviaries.Add(new Desert(_random, _maxCapacityAviary));
            _aviaries.Add(new Forest(_random, _maxCapacityAviary));
            _aviaries.Add(new Meadow(_random, _maxCapacityAviary));
        }
    }

    abstract class Aviary
    {
        protected Random _random;
        private List<Animal> _animals = new List<Animal>();
        protected int _capacity;

        public Aviary(Random random, int capacity)
        {
            _random = random;
            _capacity = capacity;
        }

        public void AddAnimal(Animal animal)
        {
            if (_animals.Count < _capacity)
                _animals.Add(animal);
            else
                Console.WriteLine("Вальер переполнен!");
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"в количестве {_animals.Count} особей.\n" + new string(FormaOutput.DelimeterSymbol, FormaOutput.DelimeterLenght));

            foreach (var animal in _animals)
                animal.ShowInfo();
        }

        protected abstract void FillAviary();
    }

    class Savannah : Aviary
    {
        public Savannah(Random random, int capacity) : base(random, capacity)
        {
            FillAviary();
        }

        public override void ShowInfo()
        {
            Console.Write("Это саванный вальер. Здесь содержатся Львы ");
            base.ShowInfo();
        }

        protected override void FillAviary()
        {
            int countAnimal = _random.Next(_capacity + 1);

            for (int i = 0; i < countAnimal; i++)
                AddAnimal(new Lion((AnimalGender)_random.Next((int)AnimalGender.Max), "РРРРаарррр", (AnimalState)_random.Next((int)AnimalState.Max), _random));
        }
    }

    class Desert : Aviary
    {
        public Desert(Random random, int capacity) : base(random, capacity)
        {
            FillAviary();
        }

        public override void ShowInfo()
        {
            Console.Write("Это пустынный вальер. Здесь содержатся Страусы ");
            base.ShowInfo();
        }

        protected override void FillAviary()
        {
            int countAnimal = _random.Next(_capacity + 1);

            for (int i = 0; i < countAnimal; i++)
                AddAnimal(new Ostrich((AnimalGender)_random.Next((int)AnimalGender.Max), "Чирик чирик", (AnimalState)_random.Next((int)AnimalState.Max), _random));
        }
    }

    class Forest : Aviary
    {
        public Forest(Random random, int capacity) : base(random, capacity)
        {
            FillAviary();
        }

        public override void ShowInfo()
        {
            Console.Write("Это лесной вальер. Здесь содержатся Слоны ");
            base.ShowInfo();
        }

        protected override void FillAviary()
        {
            int countAnimal = _random.Next(_capacity + 1);

            for (int i = 0; i < countAnimal; i++)
                AddAnimal(new Elephant((AnimalGender)_random.Next((int)AnimalGender.Max), "Дуууууу", (AnimalState)_random.Next((int)AnimalState.Max), _random));
        }
    }

    class Meadow : Aviary
    {
        public Meadow(Random random, int capacity) : base(random, capacity)
        {
            FillAviary();
        }

        public override void ShowInfo()
        {
            Console.Write("Это луговой вальер. Здесь содержатся Лошади ");
            base.ShowInfo();
        }

        protected override void FillAviary()
        {
            int countAnimal = _random.Next(_capacity + 1);

            for (int i = 0; i < countAnimal; i++)
                AddAnimal(new Horse((AnimalGender)_random.Next((int)AnimalGender.Max), "Игого", (AnimalState)_random.Next((int)AnimalState.Max), _random));
        }
    }

    abstract class Animal
    {
        private string _call;
        private int _maxAge = 60;
        private int _age;
        private AnimalGender _gender;
        private AnimalState _state;
        Random _random;

        public Animal(AnimalGender gender, string call, AnimalState state, Random random)
        {
            _gender = gender;
            _call = call;
            _state = state;
            _random = random;
            _age = _random.Next(_maxAge + 1);
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"{_state}. Его возраст: {_age}. Пол: {_gender}. Он издает звук \"{_call}\".");
        }
    }

    class Lion : Animal
    {
        public Lion(AnimalGender gender, string call, AnimalState state, Random random) : base(gender, call, state, random) { }

        public override void ShowInfo()
        {
            Console.Write("Этот Лев ");
            base.ShowInfo();
        }
    }

    class Elephant : Animal
    {
        public Elephant(AnimalGender gender, string call, AnimalState state, Random random) : base(gender, call, state, random) { }

        public override void ShowInfo()
        {
            Console.Write("Этот Слон ");
            base.ShowInfo();
        }
    }

    class Ostrich : Animal
    {
        public Ostrich(AnimalGender gender, string call, AnimalState state, Random random) : base(gender, call, state, random) { }

        public override void ShowInfo()
        {
            Console.Write("Этот Страус ");
            base.ShowInfo();
        }
    }

    class Horse : Animal
    {
        public Horse(AnimalGender gender, string call, AnimalState state, Random random) : base(gender, call, state, random) { }

        public override void ShowInfo()
        {
            Console.Write("Эта Лошадь ");
            base.ShowInfo();
        }
    }

    enum AnimalState
    {
        Stay,
        Walk,
        Lies,
        Eating,
        Sleeping,
        Max,
    }

    enum AnimalGender
    {
        Male,
        Female,
        CombatHelicopter,   // Простите не удержался =) Я Честно сопротивлялся, но этот мем огрел меня по затылку стулом и пока я вылазил из под стола, сделал свое грязное дело
        Max,
    }

    enum Menu
    {
        ChooseSavannahAviary,
        ChooseDesertAviary,
        ChooseForestAviary,
        ChooseMeadowAviary,
        ChooseExit,
        Max,
    }
}
