using System.Collections.Generic;

public class RotatingSoulState : SoulState
{
    public override Dictionary<SoulStates, int> NextPossibleStatesToPossibilityWeights { get; }
        = new Dictionary<SoulStates, int>
        {
            { SoulStates.Rotating, 1 },
            { SoulStates.Moving, 2 },
            { SoulStates.Staying, 2 },
        };

    public override SoulStates State => SoulStates.Rotating;
}
