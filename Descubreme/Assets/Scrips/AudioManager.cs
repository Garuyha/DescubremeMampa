using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource correctSource, failSource;

    public void PlayCorrectSound()
    {
        correctSource.Play();
    }
}
