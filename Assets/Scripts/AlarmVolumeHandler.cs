using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AlarmVolumeHandler : MonoBehaviour
{
    [SerializeField] private AudioSource alarmSound;
    [SerializeField] private float fadeSpeed = 0.5f;

    private bool isIntruderInside = false;
    private float targetVolume;
    private float maxtargetVolume = 1f;
    private float mintargetVolume = 0f;

    private void Update()
    {
        alarmSound.volume = Mathf.MoveTowards(alarmSound.volume, targetVolume, fadeSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out _))
        {
            isIntruderInside = true;
            targetVolume = maxtargetVolume;

            if (alarmSound.isPlaying == false)
                alarmSound.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isIntruderInside = false;
        targetVolume = mintargetVolume;
    }
}
