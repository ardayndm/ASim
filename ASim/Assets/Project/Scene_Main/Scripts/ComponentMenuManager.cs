using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComponentMenuManager : MonoBehaviour
{
    public TMP_InputField SearchInputField;
    public GameObject ItemsContentObject;
    public Image MainComponentImage;
    public Image SecondComponentImage;
    public Image ThirdComponentImage;
    public TextMeshProUGUI ComponentName;
    public GameObject ComponentInfoScrollView;
    public GameObject ComponentAttributesScrollView;
    public GameObject ComponentInfoScrollViewContent;
    public GameObject ComponentAttributesScrollViewContent;
    public Button SelectButton;
    public Button CloseMenuButton;
    public UIHoverHelper ComponentsMenuHoverHelper;
    public static List<MenuItem> MenuItems = new List<MenuItem>();

    void Start()
    {
        ComponentsMenuHoverHelper.OnHovered += () => CameraMoveManager.LockCamera(gameObject, this);
        ComponentsMenuHoverHelper.OnHoverExit += () => CameraMoveManager.UnlockCamera(gameObject, this);
        CloseMenuButton.onClick.AddListener(OnCloseMenuButtonClick);
    }

    void OnDisable()
    {
        CameraMoveManager.UnlockCamera(gameObject, this);
    }

    private void OnCloseMenuButtonClick()
    {
        gameObject.SetActive(false);
    }

    public void SearchItem(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            foreach (var item in MenuItems)
            {
                item.gameObject.SetActive(true);
            }
            return;
        }

        foreach (var item in MenuItems)
        {
            if (item.MenuItemSelfScriptableObject.Name.ToLower().Contains(name.ToLower()))
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