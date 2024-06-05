using UnityEngine;

namespace Assets.Scripts.Gate
{
    public class GateEntering : MonoBehaviour
    {
        [SerializeField] private GateHint _gateHint;

        private bool _hasKey = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GateHint gateHint) == true)
            {
                gateHint.TryShowHint(_hasKey);
            }
        }
    }
}
