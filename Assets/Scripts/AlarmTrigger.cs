using System;
using UnityEngine;

public class AlarmTrigger : MonoBehaviour
{
    public event Action IntruderEntered;
    public event Action IntruderExited;

    private void OnTriggerEnter(Collider other)
    {
        TryInvokeOnTrigger(IntruderEntered, other);
    }

    private void OnTriggerExit(Collider other)
    {
        TryInvokeOnTrigger(IntruderExited, other);
    }

    private void TryInvokeOnTrigger(Action action, Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out _))
        {
            action?.Invoke();
        }
    }
}