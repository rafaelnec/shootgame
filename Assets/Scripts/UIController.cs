using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject nukeBar;
    [SerializeField] private GameObject gunPowerUpBar;
    [SerializeField] private Sprite nukeActiveSprite;
    [SerializeField] private Sprite nukeInactiveSprite;
    [SerializeField] private Sprite gunPowerUpActiveSprite;
    [SerializeField] private Sprite gunPowerUpInactiveSprite;

    public void OnNukeCollected(int idx)
    {
        Image uiImage = nukeBar.transform.Find("Nuke0" + idx).GetComponent<Image>();
        uiImage.sprite = nukeActiveSprite;
    }

    public void OnNukeUsed(int idx)
    {
        Image uiImage = nukeBar.transform.Find("Nuke0" + idx).GetComponent<Image>();
        uiImage.sprite = nukeInactiveSprite;
    }

    public void OnGunPowerUP(int idx)
    {
        Image uiImage = gunPowerUpBar.transform.Find("GunPowerUp0" + idx).GetComponent<Image>();
        uiImage.sprite = gunPowerUpActiveSprite;
    }

    public void OnGunPowerUPUsed(int idx)
    {
        Image uiImage = gunPowerUpBar.transform.Find("GunPowerUp0" + idx).GetComponent<Image>();
        uiImage.sprite = gunPowerUpInactiveSprite;
    }
}
