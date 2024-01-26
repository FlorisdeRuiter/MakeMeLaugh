using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public void PlaySound(AudioClip audio)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(audio);
    }
}
