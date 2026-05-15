using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private Slider _loadingBar;

    private void Start()
    {
        LoadingRoutine(0.7f).Forget();
    }

    public async UniTaskVoid LoadingRoutine(float duration)
    {
        float elapsed = 0f;
        _loadingBar.value = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float progress = Mathf.Clamp01(elapsed / duration);
            _loadingBar.value = progress;

            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        _loadingBar.value = 1.0f;
        UIExtension.CloseLoading();
    }
}
