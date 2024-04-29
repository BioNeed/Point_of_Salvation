using System.Collections.Generic;

public class MovingSoulState : SoulState
{
    public override Dictionary<SoulStates, int> NextPossibleStatesToPossibilityWeights { get; }
        = new Dictionary<SoulStates, int>
        {
            { SoulStates.Rotating, 1 },
            { SoulStates.Staying, 1 },
        };

    public override SoulStates State => SoulStates.Moving;
}
