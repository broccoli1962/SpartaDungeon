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

    Coroutine testCoroutine;

    private void Update()
    {
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

        foreach (var effect in data.usableEffects)
        {
            switch (effect.UsableEffect)
            {
                case EUsableEffectType.Regenation:
                    testCoroutine = StartCoroutine(HealthRegen(effect.value ,effect.durationValue));
                    break;
            }
        }
    }

    IEnumerator HealthRegen(float value, float duration)
    {
        float timer = 0f;
        float tick = 0.1f;

        while(timer < duration)
        {
            Heal(value);

            Debug.Log($"남은 시간 {duration - timer}");
            yield return new WaitForSeconds(0.1f);
            timer += tick;
        }
    }
}
