using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HotbarItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image ItemImage;
    public TextMeshProUGUI ItemName;
    public Button ItemButton;
    public Action<HotbarItem> OnHover;
    public Action<HotbarItem> OnHoverExit;

    public void OnPointerEnter(PointerEventData eventData) => OnHover?.Invoke(this);
    public void OnPointerExit(PointerEventData eventData) => OnHoverExit?.Invoke(this);

    
    public void SetItem(HotbarItem item)
    {
        ItemImage.sprite = item.ItemImage.sprite;
        ItemName.text = item.ItemName.text;
    }
}