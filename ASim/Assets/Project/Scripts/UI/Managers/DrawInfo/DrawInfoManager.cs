using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DrawInfoManager : MonoBehaviour
{
    public static DrawInfoManager Instance { get; private set; }
    public Image DrawingInfoImage;
    public Image DrawingStateImage;
    public GameObject DrawingInfoPanel;

    public Sprite DrawingSprite;
    public Sprite SelectedSprite;

    void Awake() => Instance = this;

    public void ShowDrawingInfo(Sprite componentSprite, bool isDrawing)
    {
        DrawingInfoImage.sprite = componentSprite;
        DrawingStateImage.sprite = isDrawing ? DrawingSprite : SelectedSprite;
        DrawingInfoPanel.SetActive(true);
    }

    public void HideDrawingInfo() => DrawingInfoPanel.SetActive(false);
    public void SetDrawMode(bool isDrawing) => DrawingStateImage.sprite = isDrawing ? DrawingSprite : SelectedSprite;


}