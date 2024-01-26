using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public SfxManager _soundManager;
    public AudioClip _audioClip;

    private void Start()
    {
        _soundManager = FindObjectOfType<SfxManager>();
    }

    public void PlaySound()
    {
        _soundManager.PlaySound(_audioClip);
    }
}
