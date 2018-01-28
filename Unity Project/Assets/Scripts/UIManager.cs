using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text messageBox;
    public Text protecteeText;
    public RectTransform car;

    private void Start()
    {
        CleanMessageBox();
    }

    public void ShowMessage(string message, bool useClean = false, float messageTime = 1f)
    {
        messageBox.text = message;
        if (useClean)
        {
            Invoke("CleanMessageBox", messageTime);
        }
    }

    void CleanMessageBox()
    {
        messageBox.text = string.Empty;
    }

    public void ProtecteeTextUpdate(string text)
    {
        protecteeText.text = text;
    }
    public void ProtecteeCarUpdate(float percent)
    {
        car.pivot = new Vector2(percent, car.pivot.y);
    }
}
