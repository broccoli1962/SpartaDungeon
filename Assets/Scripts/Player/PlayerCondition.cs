using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakeDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    //플레이어 객체에서 컨디션을 처리하기
    public UICondition uiCondition;
    public Condition Health { get { return uiCondition.health; } }

    private void Update()
    {
        //test
        if(uiCondition == null)
        {
            uiCondition = UIManager.Instance.uiCondition;
            Debug.Log("uiCondition이 null인데요");
            return;
        }
        Health.RemoveValue(Health.passiveValue * Time.deltaTime);
        //허기 감소
        //허기 0 이하 일시 체력 감소
    }

    public void Heal(float value)
    {
        Health.AddValue(value);
    }

    public void TakeDamage(int damage)
    {
        Health.RemoveValue(damage);
    }

    public void UseItem(ItemData data)
    {
        foreach (var usable in data.usables)
        {
            switch (usable.Usable) {
                case EUsableType.Health:
                    Heal(usable.value);
                    break;
            }
        }

        foreach (var duration in data.usableDurations)
        {
            switch (duration.UsableDuration)
            {
                case EUsableDurationType.Regenation:
                    StartCoroutine(HealthRegen(duration.durationValue));
                    break;
            }
        }
    }

    IEnumerator HealthRegen(float duration)
    {
        float time = duration;
        while(time > 0)
        {
            time -= Time.deltaTime;
            Heal(2);
            Debug.Log("회복");
            if (time <= 0)
            {
                yield return null;
            }
        }
    }
}
