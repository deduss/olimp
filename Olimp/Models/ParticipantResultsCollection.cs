using System.Collections;
using System.Collections.Immutable;
using Olimp.Data;
using Olimp.Data.Enums;

namespace Olimp.Models;

public class ParticipantResultsCollection : IReadOnlyList<ParticipantResult>
{
    private IReadOnlyList<ParticipantResult> _participants;

    public IEnumerator<ParticipantResult> GetEnumerator()
    {
        return _participants.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_participants).GetEnumerator();
    }

    public int Count => _participants.Count;

    public ParticipantResult this[int index] => _participants[index];

    public ParticipantResultsCollection(IEnumerable<IGrouping<Participant, Result>> collection)
    {
        _participants = collection.Select(results =>
            {
                var resultsDict = results.ToDictionary(result => result.Step, result => result.Score);
                return new ParticipantResult(results.Key.EduOrg.Logo, results.Key.EduOrg.ShortName,
                    results.Key.Number!.Value, resultsDict);
            })
            .ToImmutableList();

        CalcPoints(StepType.Theory);
        CalcPoints(StepType.Practice);

        _participants = _participants
            .OrderBy(result => result.Points)
            .ToImmutableArray();
    }

    private void CalcPoints(StepType stepType)
    {
        var filteredSteps = _participants
            .Select(result => result.Results.Where(pair => pair.Key.Type == stepType).ToImmutableArray())
            .Where(pairs => pairs.Length > 0)
            .Select(pairs => pairs.Sum(pair => pair.Value))
            .ToImmutableArray();

        if (filteredSteps.Length == 0)
        {
            return;
        }

        var maxResult = stepType == StepType.Theory
            ? filteredSteps.Max()
            : filteredSteps.Min();

        foreach (var participantResult in _participants)
        {
            participantResult.CalcPoints(stepType, maxResult);
        }
    }
}

public record ParticipantResult(string Logo, string ShortName, int Number, IReadOnlyDictionary<Step, decimal> Results)
{
    public decimal Points { get; private set; }

    public void CalcPoints(StepType stepType, decimal maxPoints)
    {
        var sum = Results.Where(pair => pair.Key.Type == stepType).Sum(pair => pair.Value);
        if (sum == 0)
            return;
        var result = stepType == StepType.Theory ? sum / maxPoints * 50 : maxPoints / sum * 50;
        Points += result;
    }
}