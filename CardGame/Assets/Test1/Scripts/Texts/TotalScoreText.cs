using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalScoreText : NumTextUpdater
{
    [SerializeField] TextMeshProUGUI totalScoreText;

    private void Awake()
    {
        totalScoreText = GetComponent<TextMeshProUGUI>();
    }

    public override void UpdateText(int text)
    {
        totalScoreText.text = text.ToString();
    }
}
