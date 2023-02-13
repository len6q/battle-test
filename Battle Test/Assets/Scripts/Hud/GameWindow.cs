using UnityEngine;

public abstract class GameWindow : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    public virtual void Open()
    {
        _canvasGroup.Open();
    }

    public virtual void Close()
    {
        _canvasGroup.Close();
    }
}
