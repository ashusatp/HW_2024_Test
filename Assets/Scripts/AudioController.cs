using UnityEngine;

public class AudioController : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip checkPoint;
    public AudioClip death;

    void Start()
    {
        audioSource.clip = background;
        audioSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void stopBackgroundMusic()
    {
       audioSource.Stop();
    }
}
