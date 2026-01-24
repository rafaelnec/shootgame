using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject nukeBar;
    [SerializeField] private Sprite nukeActiveSprite;
    [SerializeField] private Sprite nukeInactiveSprite;
    
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
}
