using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider _volumeSlider;
    public Slider _sfxSlider;

    public void ChangeVolume()
    {
        MainManager.Instance.musicVolume = _volumeSlider.value;
    }

    public void ChangeSFX()
    {
        MainManager.Instance.sfxVolume = _sfxSlider.value;
    }


}
