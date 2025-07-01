using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    public Sprite MenuItemSelfSprite;
    public Button MenuItemSelfButton;
    public MenuItemSO MenuItemSelfScriptableObject;

    void Start()
    {
        if (MenuItemSelfButton.image.sprite == null)
            SetImage(MenuItemSelfSprite);
    }

    public void SetImage(Sprite sprite)
    {
        MenuItemSelfButton.image.sprite = sprite;
    }
}