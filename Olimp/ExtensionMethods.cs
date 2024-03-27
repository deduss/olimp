using Olimp.Data.Enums;

namespace Olimp;

public static class ExtensionMethods
{
    public static string ToDisplayName(this StepType stepType)
    {
        return stepType switch
        {
            StepType.Theory => "Теория",
            StepType.Practice => "Практика",
            _ => throw new ArgumentOutOfRangeException(nameof(stepType), stepType, null)
        };
    } 
}