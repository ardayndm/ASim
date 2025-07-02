using UnityEngine;
using UnityEngine.UI;

public class DrawingItemStatusManager : MonoBehaviour
{
    public Image DrawingItemImg;
    public Image DrawModeImg;
    public Sprite DrawingSprite;
    public Sprite WaitingSprite;
    public static DrawingItemStatusManager Instance { get; private set; }

    public void Awake() => Instance = this;

    /// <summary>
    /// Drawing item'in gorselini ve çizim modu göstergesini ayarlar
    /// </summary>
    /// <param name="itemSprite">Çizilen item'in gorseli</param>
    /// <param name="isDrawing">Çizim modu</param>
    public void SetDrawing(Sprite itemSprite, bool isDrawing)
    {
        DrawingItemImg.sprite = itemSprite;
        DrawModeImg.sprite = isDrawing ? DrawingSprite : WaitingSprite;
    }

    /// <summary>
    /// Çizim modunu ayarlar
    /// </summary>
    /// <param name="isDrawing">Çizim modu</param>
    public void SetDrawMode(bool isDrawing) => DrawModeImg.sprite = isDrawing ? DrawingSprite : WaitingSprite;
}