using UnityEngine;
using UnityEditor;
using System;

// �� ��Ʈ����Ʈ�� ItemEffect Ÿ���� ��� ������Ƽ�� �� Ŭ������ ����� �׸����� �մϴ�.
[CustomPropertyDrawer(typeof(ItemEffect))]
public class ItemEffectDrawer : PropertyDrawer
{
    // �ν����Ϳ� GUI�� �׸��� ���� �Լ�
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // --- 1. Ÿ�� ���� UI �׸��� ---
        // ������Ƽ�� ���� ��ü(managed reference)�� �����ɴϴ�.
        var targetObject = property.managedReferenceValue;

        // ���� ���õ� Ÿ���� �ε����� ã���ϴ�.
        int selectedTypeIndex = -1;
        if (targetObject != null)
        {
            selectedTypeIndex = Array.IndexOf(GetInheritedTypes(), targetObject.GetType());
        }

        // Ÿ�� ���� �˾��� �׸��ϴ�.
        Rect popupPosition = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        int newTypeIndex = EditorGUI.Popup(popupPosition, "Effect Type", selectedTypeIndex, GetInheritedTypeNames());

        // Ÿ���� ����Ǿ��ٸ� ���ο� �ν��Ͻ��� �����Ͽ� �Ҵ��մϴ�.
        if (newTypeIndex != selectedTypeIndex)
        {
            if (newTypeIndex == -1) // "None" ���� ��
            {
                property.managedReferenceValue = null;
            }
            else
            {
                // Activator�� ����� ���õ� Ÿ���� �� �ν��Ͻ��� ����ϴ�.
                property.managedReferenceValue = Activator.CreateInstance(GetInheritedTypes()[newTypeIndex]);
            }
            property.serializedObject.ApplyModifiedProperties();
            EditorGUI.EndProperty();
            return; // Ÿ���� �ٲ�����Ƿ� �̹� ������ �׸���� ���⼭ ����
        }

        // --- 2. ���õ� Ÿ�Կ� �´� �Ӽ��� �׸��� ---
        if (property.managedReferenceValue != null)
        {
            // �鿩���⸦ �߰��Ͽ� ���� ���� ����ϴ�.
            EditorGUI.indentLevel++;

            // ���� ��ġ�� �� �� �Ʒ��� �̵���ŵ�ϴ�.
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // SerializedProperty�� ��ȸ�ϸ� ��� �ڽ� ������Ƽ�� �׸��ϴ�.
            foreach (SerializedProperty child in property)
            {
                position.height = EditorGUI.GetPropertyHeight(child, true);
                EditorGUI.PropertyField(position, child, true);
                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
            }

            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }

    // ������Ƽ�� ��ü ���̸� ���
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float totalHeight = EditorGUIUtility.singleLineHeight; // Ÿ�� ���� �˾� ����

        if (property.managedReferenceValue != null)
        {
            // �� �ڽ� ������Ƽ�� ���̸� ���մϴ�.
            foreach (SerializedProperty child in property)
            {
                totalHeight += EditorGUI.GetPropertyHeight(child, true) + EditorGUIUtility.standardVerticalSpacing;
            }
        }
        return totalHeight;
    }


    // --- Helper �Լ��� ---
    private static Type[] GetInheritedTypes()
    {
        return new Type[] { typeof(Heal) , typeof(Speed) }; // ���⿡ ItemEffect�� ��ӹ޴� ��� Ŭ������ �߰�
    }

    private static string[] GetInheritedTypeNames()
    {
        return new string[] { "Heal", "Speed" }; // �� Ÿ�� ������ �����ϰ� �̸� �߰�
    }
}