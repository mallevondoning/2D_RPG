using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI LevelText;
    [SerializeField]
    private Image expFill;
    [SerializeField]
    private Image hpFill;

    private void Update()
    {
        LevelText.text = "LV: " + PlayerController.Instance.Level;

        hpFill.fillAmount = PlayerController.Instance.Hp / PlayerController.Instance.MaxHp;
        expFill.fillAmount = (float)PlayerController.Instance.Exp / PlayerController.Instance.ExpCheck;
    }
}
