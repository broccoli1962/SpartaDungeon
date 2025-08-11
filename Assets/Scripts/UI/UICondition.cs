using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;

    private void Awake()
    {
        UIManager.Instance.uiCondition = this;
    }




    //Dictionary<ConditionType, Condition> conditions = new();
    //동적 생성 시도
    //private void Start()
    //{
    //    UIManager.Instance.uiCondition = this;

    //    Condition[] childConditions = GetComponentsInChildren<Condition>();
    //    foreach (Condition cond in childConditions)
    //    {
    //        if (!conditions.ContainsKey(cond.conditionType))
    //        {
    //            conditions.Add(cond.conditionType, cond);
    //        }
    //    }
    //}

    //public Condition GetCondition(ConditionType cond)
    //{
    //    if (conditions.ContainsKey(cond))
    //    {
    //        return conditions[cond];
    //    }
    //    return null;
    //}
}
