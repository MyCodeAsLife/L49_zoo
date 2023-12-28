using System;
using System.Collections.Generic;

namespace L49_zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationZoo myZoo = new ApplicationZoo();

            myZoo.Run();
        }
    }

    class ApplicationZoo
    {
        private const int DelimeterLenght = 75;
        private const char DelimeterSymbol = '-';

        private List<Aviary> _aviaries = new List<Aviary>();
        private int _maxCapacityAviary = 12;

        public ApplicationZoo()
        {
            Fill();
        }

        private enum Menu
        {
            ChooseSavannahAviary = 1,
            ChooseDesertAviary = 2,
            ChooseForestAviary = 3,
            ChooseMeadowAviary = 4,
            ChooseExit = 5,
        }

        public void Run()
        {
            bool isOpen = true;

            while (isOpen)
            {
                Console.Clear();

                Console.WriteLine($"Добро пожаловать в наш зоопарк!\n" + new string(DelimeterSymbol, DelimeterLenght) + $"\n" +
                                  $"{(int)Menu.ChooseSavannahAviary} - Подойти к саванному вальеру.\n{(int)Menu.ChooseDesertAviary} - " +
                                  $"Подойти к пустынному вальеру.\n{(int)Menu.ChooseForestAviary} - Подойти к лесному вальеру.\n" +
                                  $"{(int)Menu.ChooseMeadowAviary} - Подойти к луговому вальеру.\n{(int)Menu.ChooseExit} - " +
                                  $"Выйти из программы.\n" + new string(DelimeterSymbol, DelimeterLenght));

                Console.Write("Выберите пункт меню: ");
                int menuLenght = Enum.GetNames(typeof(Menu)).Length;

                if (int.TryParse(Console.ReadLine(), out int menuNumber))
                {
                    Console.Clear();

                    if (menuNumber < menuLenght || menuNumber >= 0)
                    {
                        if ((Menu)menuNumber == Menu.ChooseExit)
                            isOpen = false;
                        else
                            ApproachEnclosure(menuNumber);
                    }
                    else
                    {
                        ShowError();
                    }
                }
                else
                {
                    ShowError();
                }

                Console.WriteLine(new string(DelimeterSymbol, DelimeterLenght) + "\nДля продолжения нажмите любую кнопку...");
                Console.ReadKey(true);
            }
        }

        private void ApproachEnclosure(int menuNumber)
        {
            _aviaries[menuNumber].ShowInfo();
        }

        private void Fill()
        {
            _aviaries.Add(new Savannah(_maxCapacityAviary));
            _aviaries.Add(new Desert(_maxCapacityAviary));
            _aviaries.Add(new Forest(_maxCapacityAviary));
            _aviaries.Add(new Meadow(_maxCapacityAviary));
        }

        private void ShowError()
        {
            Console.Clear();
            Console.WriteLine("Вы ввели некорректное значение.");
        }
    }

    abstract class Aviary
    {
        private List<AnimalGenerator> _animalList;
        private List<Animal> _animals = new List<Animal>();
        protected int _capacity;

        public Aviary(int capacity)
        {
            _animalList = new List<AnimalGenerator> { new LionAnimalGenerator(),
                                                      new ElephantAnimalGenerator(),
                                                      new OstrichAnimalGenerator(),
                                                      new HorseAnimalGenerator() };
            _capacity = capacity;
        }

        public void AddAnimal(string animalType)
        {
            if (_animals.Count < _capacity)
            {
                foreach (var animal in _animalList)
                {
                    if (animal.GetAnimalType().ToLower() == animalType.ToLower())
                        _animals.Add(animal.Create());
                    else
                        Console.WriteLine("Нет такого животного.");
                }
            }
            else
            {
                Console.WriteLine("Вальер переполнен!");
            }
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"в количестве {_animals.Count} особей.\n\n");

            foreach (var animal in _animals)
                animal.ShowInfo();
        }

        protected abstract void FillAviary();
    }

    class Savannah : Aviary
    {
        public Savannah(int capacity) : base(capacity)
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
            int countAnimal = RandomGenerator.GetRandomNumber(_capacity + 1);

            for (int i = 0; i < countAnimal; i++)
                AddAnimal("Lion");
        }
    }

    class Forest : Aviary
    {
        public Forest(int capacity) : base(capacity)
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
            int countAnimal = RandomGenerator.GetRandomNumber(_capacity + 1);

            for (int i = 0; i < countAnimal; i++)
                AddAnimal("Elephant");
        }
    }

    class Desert : Aviary
    {
        public Desert(int capacity) : base(capacity)
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
            int countAnimal = RandomGenerator.GetRandomNumber(_capacity + 1);

            for (int i = 0; i < countAnimal; i++)
                AddAnimal("Ostrich");
        }
    }

    class Meadow : Aviary
    {
        public Meadow(int capacity) : base(capacity)
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
            int countAnimal = RandomGenerator.GetRandomNumber(_capacity + 1);

            for (int i = 0; i < countAnimal; i++)
                AddAnimal("Horse");
        }
    }

    abstract class Animal
    {
        private string _call;
        private int _maxAge = 60;
        private int _age;
        private AnimalGender _gender;
        private AnimalState _state;

        public Animal(AnimalGender gender, string call, AnimalState state)
        {
            _gender = gender;
            _call = call;
            _state = state;
            _age = RandomGenerator.GetRandomNumber(_maxAge + 1);
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"{_state}. Возраст особи: {_age}.\tПол: {_gender}.\tОн издает звук \"{_call}\".");
        }
    }

    class Lion : Animal
    {
        public Lion(AnimalGender gender, string call, AnimalState state) : base(gender, call, state) { }

        public override void ShowInfo()
        {
            Console.Write("Этот Лев ");
            base.ShowInfo();
        }
    }

    class Elephant : Animal
    {
        public Elephant(AnimalGender gender, string call, AnimalState state) : base(gender, call, state) { }

        public override void ShowInfo()
        {
            Console.Write("Этот Слон ");
            base.ShowInfo();
        }
    }

    class Ostrich : Animal
    {
        public Ostrich(AnimalGender gender, string call, AnimalState state) : base(gender, call, state) { }

        public override void ShowInfo()
        {
            Console.Write("Этот Страус ");
            base.ShowInfo();
        }
    }

    class Horse : Animal
    {
        public Horse(AnimalGender gender, string call, AnimalState state) : base(gender, call, state) { }

        public override void ShowInfo()
        {
            Console.Write("Эта Лошадь ");
            base.ShowInfo();
        }
    }

    abstract class AnimalGenerator
    {
        protected List<string> Gender = new List<string>(Enum.GetNames(typeof(AnimalGender)));

        public abstract Animal Create();

        public abstract string GetAnimalType();
    }

    class LionAnimalGenerator : AnimalGenerator
    {
        public override Animal Create() => new Lion((AnimalGender)RandomGenerator.GetRandomNumber(Gender.Count), "РРРРаарррр", (AnimalState)RandomGenerator.GetRandomNumber(Gender.Count));

        public override string GetAnimalType() => "Lion";
    }

    class ElephantAnimalGenerator : AnimalGenerator
    {
        public override Animal Create() => new Elephant((AnimalGender)RandomGenerator.GetRandomNumber(Gender.Count), "Дуууууу", (AnimalState)RandomGenerator.GetRandomNumber(Gender.Count));

        public override string GetAnimalType() => "Elephant";
    }

    class OstrichAnimalGenerator : AnimalGenerator
    {
        public override Animal Create() => new Ostrich((AnimalGender)RandomGenerator.GetRandomNumber(Gender.Count), "Чирик чирик", (AnimalState)RandomGenerator.GetRandomNumber(Gender.Count));

        public override string GetAnimalType() => "Ostrich";
    }

    class HorseAnimalGenerator : AnimalGenerator
    {
        public override Animal Create() => new Horse((AnimalGender)RandomGenerator.GetRandomNumber(Gender.Count), "Игого", (AnimalState)RandomGenerator.GetRandomNumber(Gender.Count));

        public override string GetAnimalType() => "Horse";
    }

    static class RandomGenerator
    {
        private static Random s_random = new Random();

        public static int GetRandomNumber(int minValue, int maxValue) => s_random.Next(minValue, maxValue);

        public static int GetRandomNumber(int maxValue) => s_random.Next(maxValue);
    }

    enum AnimalState
    {
        Stay,
        Walk,
        Lies,
        Eating,
        Sleeping,
    }

    enum AnimalGender
    {
        Male,
        Female,
        CombatHelicopter,
    }
}
