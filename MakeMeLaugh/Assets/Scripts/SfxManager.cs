using UnityEngine;

public class SfxManager : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audio)
    {
        if (_audioSource == null)
        {
            gameObject.AddComponent<AudioSource>();
            _audioSource = GetComponent<AudioSource>();
        }

        _audioSource.PlayOneShot(audio);
    }
}
