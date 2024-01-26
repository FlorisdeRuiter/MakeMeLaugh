using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private SfxManager _soundManager;
    public List<AudioClip> _audioClips;

    private void Start()
    {
        _soundManager = FindObjectOfType<SfxManager>();
    }

    public void PlaySound()
    {
        _soundManager.PlaySound(_audioClips[Random.Range(0, _audioClips.Count)]);
    }
}
