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
}
