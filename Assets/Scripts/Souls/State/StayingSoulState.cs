using System.Collections.Generic;

public class StayingSoulState : SoulState
{
    public override Dictionary<SoulStates, int> NextPossibleStatesToPossibilityWeights { get; }
        = new Dictionary<SoulStates, int>
        {
            { SoulStates.Rotating, 2 },
            { SoulStates.Moving, 2 },
            { SoulStates.Staying, 1 },
        };

    public override SoulStates State => SoulStates.Staying;
}
