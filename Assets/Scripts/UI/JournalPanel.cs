using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JournalPanel : MonoBehaviour
{
    [SerializeField] private DeedsFinding _deedsFinding;
    [SerializeField] private GameObject _journalPanel;
    [SerializeField] private Transform _sinInitialTransform;
    [SerializeField] private Transform _virtueInitialTransform;
    [SerializeField] private GameObject _sinPrefab;
    [SerializeField] private GameObject _virtuePrefab;
    [SerializeField] private TextMeshProUGUI _factInJournalTMPGUI;

    private readonly Vector3 _deedOffset = new Vector3(0, -140, 0);

    private void Start()
    {
        _journalPanel.SetActive(false);
    }

    public void ShowJournalInfo()
    {
        _journalPanel.SetActive(true);

        FillJournalWithDeeds(
            _deedsFinding.SinsFound,
            _sinPrefab,
            _sinInitialTransform);

        FillJournalWithDeeds(
            _deedsFinding.VirtuesFound,
            _virtuePrefab,
            _virtueInitialTransform);

        _factInJournalTMPGUI.text = _deedsFinding.FoundFact?.Description;
    }

    public void ClearJournalInfo()
    {
        foreach (Transform sin in _sinInitialTransform)
        {
            Destroy(sin.gameObject);
        }

        foreach (Transform virtue in _virtueInitialTransform)
        {
            Destroy(virtue.gameObject);
        }

        _factInJournalTMPGUI.text = string.Empty;
        _journalPanel.SetActive(false);
    }

    private void FillJournalWithDeeds(
        IEnumerable<Deed> deedsFound,
        GameObject deedPrefab,
        Transform deedInitialTransform)
    {
        var deedPosition = 0;
        foreach (var deedFound in deedsFound)
        {
            var deedGameObject = Instantiate(
                deedPrefab,
                deedInitialTransform.position + _deedOffset * deedPosition,
                Quaternion.identity,
                deedInitialTransform);

            var tmpGUI = deedGameObject.GetComponentInChildren<TextMeshProUGUI>();
            tmpGUI.text = deedFound.Description;
            deedPosition++;
        }
    }
}
