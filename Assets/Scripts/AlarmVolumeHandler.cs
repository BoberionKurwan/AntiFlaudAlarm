using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class AlarmVolumeHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _fadeSpeed = 0.5f;
    [SerializeField] private AlarmTrigger _trigger;

    private Coroutine _volumeCoroutine;
    private bool _isIntruderInside = false;
    private float _targetVolume;
    private float _maxTargetVolume = 1f;
    private float _mintargetVolume = 0f;

    private void OnEnable()
    {
        if (_trigger != null)
        {
            _trigger.IntruderEntered += OnIntruderEntered;
            _trigger.IntruderExited += OnIntruderExited;
        }
    }

    private void OnDisable()
    {
        if (_trigger != null)
        {
            _trigger.IntruderEntered -= OnIntruderEntered;
            _trigger.IntruderExited -= OnIntruderExited;
        }
    }

    private void OnIntruderEntered()
    {
        HandleVolume(_maxTargetVolume, _isIntruderInside: true);
    }

    private void OnIntruderExited()
    {
        HandleVolume(_mintargetVolume, _isIntruderInside: false);
    }

    private void HandleVolume(float targetVolume, bool _isIntruderInside)
    {
        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
        }

        if (_isIntruderInside && !_alarmSound.isPlaying)
        {
            _alarmSound.Play();
        }

        _volumeCoroutine = StartCoroutine(FadeVolumeCoroutine(targetVolume, _isIntruderInside));
    }


    private IEnumerator FadeVolumeCoroutine(float targetVolume, bool shouldKeepPlaying)
    {
        while (Mathf.Approximately(_alarmSound.volume, targetVolume) == false)
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, targetVolume, _fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
