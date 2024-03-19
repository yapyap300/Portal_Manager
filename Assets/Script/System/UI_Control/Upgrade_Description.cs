using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class Upgrade_Description : MonoBehaviour
{
    private Text iconName;
    private Text description;
    private Text value;
    private Text grade;
    void Awake()
    {
        iconName = transform.GetChild(0).GetComponent<Text>();
        description = transform.GetChild(1).GetComponent<Text>();
        value = transform.GetChild(2).GetComponent<Text>();
        grade = transform.GetChild(3).GetComponent<Text>();
    }
    void Update()
    {
        PointerEventData pointerEventData = new(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new ();
        EventSystem.current.RaycastAll(pointerEventData, results);
        if (results.Count == 0) return;
        if (results[0].gameObject.CompareTag("Icon"))
        {
            Icon_Data iconData = results[0].gameObject.GetComponent<Icon_Data>();
            iconName.text = LocalizationSettings.StringDatabase.GetLocalizedString("Main", iconData.upgradeName, GameManager.Instance.currentLocale);
            description.text = LocalizationSettings.StringDatabase.GetLocalizedString("Main", iconData.description, GameManager.Instance.currentLocale);
            if (iconData.grade == iconData.maxGrade)
            {
                grade.text = "Max ";
                value.text = "";
            }
            else
            {
                grade.text = $"{iconData.grade} / {iconData.maxGrade}";
                value.text = $"{iconData.value[iconData.grade]}";
            }
                
        }
    }
}
