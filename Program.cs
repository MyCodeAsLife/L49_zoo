using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L49_zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
        }
    }

    class Zoo
    {
        private List<Aviary> _aviaries = new List<Aviary>();
    }

    abstract class Aviary
    {
        protected Random _random;
        protected int _capacity;

        private List<Animal> _animals = new List<Animal>();
        private int _delimeterLenght = 75;
        private char _delimeter = '-';

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

        public virtual void ShowInfoAboutAviary()
        {
            Console.WriteLine($"в количестве {_animals.Count} особей.\n" + new string(_delimeter, _delimeterLenght));     // Форматирование

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

        public override void ShowInfoAboutAviary()
        {
            Console.Write("Это саванный вальер. Здесь содержатся Львы ");
            base.ShowInfoAboutAviary();
        }

        protected override void FillAviary()
        {
            int countAnimal = _random.Next(_capacity + 1);

            for (int i = 0; i < countAnimal; i++)
                AddAnimal(new Lion((AnimalGender)_random.Next((int)AnimalGender.Max), "РРРРаарррр", _random.Next(60), (AnimalState)_random.Next((int)AnimalState.Max)));    // Маг цифра
        }
    }

    class Desert : Aviary
    {
        public Desert(Random random, int capacity) : base(random, capacity)
        {
            FillAviary();
        }

        public override void ShowInfoAboutAviary()
        {
            Console.Write("Это пустынный вальер. Здесь содержатся Страусы ");
            base.ShowInfoAboutAviary();
        }

        protected override void FillAviary()
        {
            int countAnimal = _random.Next(_capacity + 1);

            for (int i = 0; i < countAnimal; i++)
                AddAnimal(new Ostrich((AnimalGender)_random.Next((int)AnimalGender.Max), "Чирик чирик", _random.Next(60), (AnimalState)_random.Next((int)AnimalState.Max)));    // Маг цифра
        }
    }

    class Forest : Aviary
    {
        public Forest(Random random, int capacity) : base(random, capacity)
        {
            FillAviary();
        }

        public override void ShowInfoAboutAviary()
        {
            Console.Write("Это лесной вальер. Здесь содержатся Слоны ");
            base.ShowInfoAboutAviary();
        }

        protected override void FillAviary()
        {
            int countAnimal = _random.Next(_capacity + 1);

            for (int i = 0; i < countAnimal; i++)
                AddAnimal(new Elephant((AnimalGender)_random.Next((int)AnimalGender.Max), "Дуууууу", _random.Next(60), (AnimalState)_random.Next((int)AnimalState.Max)));    // Маг цифра
        }
    }

    class Meadow : Aviary
    {
        public Meadow(Random random, int capacity) : base(random, capacity)
        {
            FillAviary();
        }

        public override void ShowInfoAboutAviary()
        {
            Console.Write("Это луговой вальер. Здесь содержатся Лошади ");
            base.ShowInfoAboutAviary();
        }

        protected override void FillAviary()
        {
            int countAnimal = _random.Next(_capacity + 1);

            for (int i = 0; i < countAnimal; i++)
                AddAnimal(new Horse((AnimalGender)_random.Next((int)AnimalGender.Max), "Игого", _random.Next(60), (AnimalState)_random.Next((int)AnimalState.Max)));    // Маг цифра
        }
    }

    abstract class Animal
    {
        private string _call;
        private int _age;
        private AnimalGender _gender;
        private AnimalState _state;

        public Animal(AnimalGender gender, string call, int age, AnimalState state)
        {
            _gender = gender;
            _call = call;
            _age = age;
            _state = state;
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"{_state}. Его возраст: {_age}. Пол: {_gender}. Он издает звук \"{_call}\".");
        }
    }

    class Lion : Animal
    {
        public Lion(AnimalGender gender, string call, int age, AnimalState state) : base(gender, call, age, state) { }

        public override void ShowInfo()
        {
            Console.Write("Этот Лев ");
            base.ShowInfo();
        }
    }

    class Elephant : Animal
    {
        public Elephant(AnimalGender gender, string call, int age, AnimalState state) : base(gender, call, age, state) { }

        public override void ShowInfo()
        {
            Console.Write("Этот Слон ");
            base.ShowInfo();
        }
    }

    class Ostrich : Animal
    {
        public Ostrich(AnimalGender gender, string call, int age, AnimalState state) : base(gender, call, age, state) { }

        public override void ShowInfo()
        {
            Console.Write("Этот Страус ");
            base.ShowInfo();
        }
    }

    class Horse : Animal
    {
        public Horse(AnimalGender gender, string call, int age, AnimalState state) : base(gender, call, age, state) { }

        public override void ShowInfo()
        {
            Console.Write("Эта Лошадь ");
            base.ShowInfo();
        }
    }

    enum AnimalState
    {
        Stay,       // Стоит
        Walk,       // Идет
        Lies,       // Лежит
        Eating,     // Ест
        Sleeping,   // Спит
        Max,
    }

    enum AnimalGender
    {
        Male,
        Female,
        CombatHelicopter,   // Простите не удержался =)
        Max,
    }
}
