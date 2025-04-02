using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlusText : TextUpdater
{
    [SerializeField] TextMeshProUGUI plusText;

    public override void UpdateText(int text)
    {
        plusText.text = text.ToString();
    }
}
