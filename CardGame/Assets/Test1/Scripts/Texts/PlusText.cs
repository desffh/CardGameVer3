using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlusText : NumTextUpdater
{
    [SerializeField] TextMeshProUGUI plusText;

    private void Awake()
    {
        plusText = GetComponent<TextMeshProUGUI>();
    }
    public override void UpdateText(int text)
    {
        plusText.text = text.ToString();
    }
}
