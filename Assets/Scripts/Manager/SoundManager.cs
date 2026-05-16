using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource _sfxSource;

    public static SoundManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        string path = "Audio/Stage";

        ResourceManager.Instance.LoadAsset<AudioClip>(path, (clip) =>
        {
            _bgmSource.clip = clip;
            _bgmSource.Play();
        });
    }

    public void SetDialogueBGM(string bgm)
    {
        string path = $"Audio/{bgm}";

        ResourceManager.Instance.LoadAsset<AudioClip>(path, (clip) =>
        {
            _bgmSource.clip = clip;
            _bgmSource.Play();
        });
    }

    public void SetOnClickSFX(string sfx)
    {
        string path = $"Audio/{sfx}";

        ResourceManager.Instance.LoadAsset<AudioClip>(path, (clip) =>
        {
            _sfxSource.PlayOneShot(clip);
        });
    }
}
