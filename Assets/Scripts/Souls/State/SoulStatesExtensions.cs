using System;

public static class SoulStatesExtensions
{
    public static SoulState GetSoulStateByEnumState(this SoulStates state)
    {
        switch (state)
        {
            case SoulStates.Moving:
                {
                    return new MovingSoulState();
                }

            case SoulStates.Rotating:
                {
                    return new RotatingSoulState();
                }

            case SoulStates.Staying:
                {
                    return new StayingSoulState();
                }
        }

        throw new InvalidOperationException($"State {state} is not supported!");
    }
}
