using UnityEngine;

public class FateChoosing : MonoBehaviour
{
    [SerializeField] private FateJournal _fateJournal;

    public void ChooseFateBurnInHell() => ChooseFate(Fate.BurnInHell);

    public void ChooseFateNoPurification() => ChooseFate(Fate.NoPurification);

    public void ChooseFateSlightSinner() => ChooseFate(Fate.SlightSinner);

    public void ChooseFateDeservePurification() => ChooseFate(Fate.DeservePurification);

    public void ChooseFateGoodFellow() => ChooseFate(Fate.GoodFellow);

    public void ChooseFateRighteous() => ChooseFate(Fate.Righteous);

    private void ChooseFate(Fate fate)
    {
        _fateJournal.CheckResultOnChosenFate(fate);
    }
}
