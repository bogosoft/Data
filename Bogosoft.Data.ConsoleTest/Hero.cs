namespace Bogosoft.Data.ConsoleTest
{
    class Hero
    {
        internal int Height;

        internal string MostNotableFeat;

        internal string Name;

        public override string ToString()
        {
            return $"Introducing, '{Name}', standing {Height} feet tall; most notable for {MostNotableFeat}";
        }
    }
}