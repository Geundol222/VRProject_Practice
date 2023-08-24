using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    [SerializeField] Slider slider;

    float progress;

    private void Awake()
    {
        progress = 0f;
    }

    public void GazeIn()
    {
        StartCoroutine(SliderValueRoutine());
    }

    IEnumerator SliderValueRoutine()
    {
        while (progress < 1f)
        {
            progress += Time.deltaTime * 0.2f;
            slider.value = Mathf.Lerp(slider.value, 1f, progress / 100f);

            yield return null;
        }
    }

    public void GazeOut()
    {
        StopAllCoroutines();
    }
}
