using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComponentMenuManager : MonoBehaviour
{
    public static ComponentMenuManager Instance { get; private set; }

    public TMP_InputField SearchInputField;
    public Transform ComponentItemsContent;
    public Button CloseMenuButton, SelectItemButton;

    public Image InfoMainImage, InfoSecondImage, InfoThirdImage;
    public Sprite DefaultSprite;
    public TextMeshProUGUI InfoComponentNameText;
    public GameObject InfoDetailsScrollViewObject, InfoAttributeScrollViewObject;
    public Transform InfoDetailsContent, InfoAttributesContent;

    public MenuItemDetail InfoDetailTextPrefab;
    public List<MenuItemAttribute> MenuItemAttributePrefabs = new();
    public List<BaseMenuItem> MenuItems = new();

    public UIHoverHelper ComponentMenuHoverHelper;

    private BaseMenuItem m_selectedItem = null;

    private void Awake() => Instance = this;

    private void Start()
    {
        InitEvents();
    }

    void InitEvents()
    {
        SearchInputField.onValueChanged.AddListener(SearchItem);
        CloseMenuButton.onClick.AddListener(CloseComponentMenu);

        foreach (BaseMenuItem item in MenuItems)
        {
            item.ItemButton.onClick.AddListener(() => OnMenuItemSelected(item));
        }
    }

    private void OnMenuItemSelected(BaseMenuItem selectedItem)
    {
        m_selectedItem = selectedItem;
        ShowSelectedItemInfo();
    }

    void ShowSelectedItemInfo()
    {
        InfoMainImage.sprite = m_selectedItem.ItemSO.ItemMainSprite;
        InfoSecondImage.sprite = m_selectedItem.ItemSO.ItemSecondSprite;
        InfoThirdImage.sprite = m_selectedItem.ItemSO.ItemThirdSprite;

        InfoComponentNameText.text = m_selectedItem.ItemSO.ItemName;

        if (m_selectedItem.ItemSO.IsItemDetailsActive)
        {
            for (int i = m_selectedItem.ItemSO.ItemDetais.Count - 1; i >= 0; i--)
            {
                MenuItemDetail detail = Instantiate(InfoDetailTextPrefab, InfoDetailsContent);
                detail.DetailText.text = m_selectedItem.ItemSO.ItemDetais[i];
            }
        }

        if (m_selectedItem.ItemSO.IsItemAttributesActive)
        {
            for (int i = m_selectedItem.ItemSO.Attributes.Count - 1; i >= 0; i--)
            {
                MenuItemAttribute attribute = Instantiate(MenuItemAttributePrefabs[i], InfoAttributesContent);
                
            }
        }
    }

    public void ShowComponentMenu()
    {
        CameraManager.LockCamera(gameObject, this);
        ComponentMenuHoverHelper.gameObject.SetActive(true);
    }
    public void CloseComponentMenu()
    {
        CameraManager.UnlockCamera(gameObject, this);
        ComponentMenuHoverHelper.gameObject.SetActive(false);
    }

    void SearchItem(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            foreach (BaseMenuItem item in MenuItems)
            {
                item.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (BaseMenuItem item in MenuItems)
            {
                if (item.ItemSO.ItemName.ToLower().Contains(text.ToLower()))
                {
                    item.gameObject.SetActive(true);
                }
                else
                {
                    item.gameObject.SetActive(false);
                }
            }
        }
    }

}