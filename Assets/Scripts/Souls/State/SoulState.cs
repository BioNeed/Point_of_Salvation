using System.Collections.Generic;

public abstract class SoulState
{
    public abstract SoulStates State { get; }

    public abstract Dictionary<SoulStates, int> NextPossibleStatesToPossibilityWeights { get; }
}
