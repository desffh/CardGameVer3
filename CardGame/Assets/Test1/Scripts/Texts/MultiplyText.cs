using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiplyText : TextUpdater
{
    [SerializeField] TextMeshProUGUI multiplyText;

    public override void UpdateText(int text)
    {
        multiplyText.text = text.ToString();
    }
}
