using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumneChanger : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider backgroundSlider;
    [SerializeField] Slider VFXSlider;

    const string MIXER_BACKGR = "BackVol";
    const string MIXER_VFX = "VFXVol";
    float value;
    void Start()
    {
        float value;
        float value2;
        bool isBackground = mixer.GetFloat(MIXER_BACKGR, out value);
        backgroundSlider.value = Mathf.Pow(10, value / 20);
        Debug.Log(value);
        bool isVFX = mixer.GetFloat(MIXER_VFX, out value2);
        VFXSlider.value = Mathf.Pow(10, value2 / 20);
        Debug.Log(value2);

    }
    void Awake()
    {
        backgroundSlider.onValueChanged.AddListener(SetBackgroundVolumne);
        VFXSlider.onValueChanged.AddListener(SetVFXVolumne);
    }
    void SetBackgroundVolumne(float value)
    {
        mixer.SetFloat(MIXER_BACKGR, Mathf.Log10(value) * 20);
    }
    void SetVFXVolumne(float value)
    {
        mixer.SetFloat(MIXER_VFX, Mathf.Log10(value) * 20);
    }
}
