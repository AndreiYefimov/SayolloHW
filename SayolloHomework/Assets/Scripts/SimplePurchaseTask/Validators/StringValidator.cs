using System.Linq;

namespace AndriiYefimov.SayolloHW2.Validators
{
    public static class StringValidator
    {
        public static bool IsNotEmpty(params string[] values)
        {
            return values.All(item => item != string.Empty);
        }
    }
}