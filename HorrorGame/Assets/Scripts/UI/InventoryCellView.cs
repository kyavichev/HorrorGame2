using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCellView : MonoBehaviour
{
    public Text nameText;
    public Image iconImage;
    public GameObject selectedGameObject;
    public Button button;

    public Action useAction;


    public void SetName(string newName)
    {
        nameText.text = newName;
    }


    public void SetIconTexture(Texture2D texture)
    {
        iconImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    public void SetSelected(bool isSelected)
    {
        selectedGameObject.SetActive(isSelected);
    }


    public void SetButtonActive(bool isActive)
    {
        button.interactable = isActive;
    }


    public void OnButtonPressed()
    {
        if(useAction != null)
        {
            useAction.Invoke();
        }
    }
}
