using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakeDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    //�÷��̾� ��ü���� ������� ó���ϱ�
    public UICondition uiCondition;
    public Condition Health { get { return uiCondition.health; } }

    Coroutine testCoroutine;

    private void Update()
    {
        Health.RemoveValue(Health.passiveValue * Time.deltaTime);
        //��� ����
        //��� 0 ���� �Ͻ� ü�� ����
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

            Debug.Log($"���� �ð� {duration - timer}");
            yield return new WaitForSeconds(0.1f);
            timer += tick;
        }
    }
}
