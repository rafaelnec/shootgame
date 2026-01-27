using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeController : MonoBehaviour
{
    [SerializeField] private List<Sprite> volumeSprites;
    [SerializeField] private Image volumeIconImage;
    [SerializeField] private Slider volumeSlider;

    private static VolumeController _instance;
    private float _previousVolumeSliderValue;   
    private bool _volumeActive = false;
    private bool _currentVolumeActive = false;

    void SetSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _instance = this;
    }

    private void Awake()
    {
        SetSingleton();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            _volumeActive = true;
            TogleVolumePanel();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            _volumeActive = true;
            TogleVolumePanel();
            volumeSlider.value = AudioListener.volume > 0 ? 0 : _previousVolumeSliderValue;
        }
    }

    public void OnVolumeChange(float volume)
    {
        _previousVolumeSliderValue = AudioListener.volume;
        AudioListener.volume = volume;
        if (volume == 0) volumeIconImage.sprite = volumeSprites[0];
        else if (volume <= 0.5) volumeIconImage.sprite = volumeSprites[1];
        else volumeIconImage.sprite = volumeSprites[2];
        UpdateAutoHiddienVolumePanel();
    }

    public void OnVolumeIconClick()
    {
        if (AudioListener.volume == 0) volumeSlider.value = _previousVolumeSliderValue;  
        else volumeSlider.value = 0;
        UpdateAutoHiddienVolumePanel();
    }

    public void TogleVolumePanel()
    {
        if (_volumeActive != _currentVolumeActive)
        {
            Transform childTransform = this.transform.Find("VolumePanel");
            if (childTransform != null) childTransform.gameObject.SetActive(_volumeActive);
            _currentVolumeActive = _volumeActive;
        }
        UpdateAutoHiddienVolumePanel();
    }

    public void UpdateAutoHiddienVolumePanel()
    {
        CancelInvoke(nameof(HiddenVolumePanel));
        Invoke("HiddenVolumePanel", 5f);
    }

    public void HiddenVolumePanel()
    {
        _volumeActive = false;
        TogleVolumePanel();
    }

}
