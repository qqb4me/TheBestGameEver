using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    public List<PlayerProgressLevel> levels;

    public RectTransform experienceValueRectTransform;
    public TextMeshProUGUI levelValueTMP;

    private int _levelValue = 1;

    private float _experienceCurrentValue = 0;
    private float _experienceTargetValue = 100;

    private void Start()
    {
        SetLevel(_levelValue);
        DrawUI();
    }
    public void AddExperience(float value)
    {
        _experienceCurrentValue += value;
        if (_experienceCurrentValue >= _experienceTargetValue)
        {
            SetLevel(_levelValue + 1);
            _experienceCurrentValue = 0;
        }

        DrawUI();
    }

    private void SetLevel(int value)
    {
        _levelValue = value;

        var currentLevel = levels[value];
        _experienceTargetValue = currentLevel.experienseForTheNextLevel;
        GetComponent<FireballCaster>().damage = currentLevel.fireballDamage;

        var grenadeCaster = GetComponent<GrenadeCaster>();
        grenadeCaster.damage = currentLevel.grenadeDamage;

        if (currentLevel.grenadeDamage < 0)
            grenadeCaster.enabled = false;
        else
            grenadeCaster.enabled = true;     
    }

    private void DrawUI()
    {
        experienceValueRectTransform.anchorMax = new Vector2(_experienceCurrentValue / _experienceTargetValue, 1);
        levelValueTMP.text = _levelValue.ToString();
    }
}
