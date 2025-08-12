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
    public Condition Hunger { get { return uiCondition.hunger; } }
    public Condition Stamina { get { return uiCondition.stamina; } }


    private void Update()
    {
        Health.RemoveValue(Health.passiveValue * Time.deltaTime);
        Stamina.AddValue(Stamina.passiveValue * Time.deltaTime);
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
}
