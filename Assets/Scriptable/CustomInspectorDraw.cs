using UnityEngine;
using UnityEditor;
using System;

// 이 어트리뷰트는 ItemEffect 타입의 모든 프로퍼티를 이 클래스를 사용해 그리도록 합니다.
[CustomPropertyDrawer(typeof(ItemEffect))]
public class ItemEffectDrawer : PropertyDrawer
{
    // 인스펙터에 GUI를 그리는 메인 함수
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // --- 1. 타입 선택 UI 그리기 ---
        // 프로퍼티의 실제 객체(managed reference)를 가져옵니다.
        var targetObject = property.managedReferenceValue;

        // 현재 선택된 타입의 인덱스를 찾습니다.
        int selectedTypeIndex = -1;
        if (targetObject != null)
        {
            selectedTypeIndex = Array.IndexOf(GetInheritedTypes(), targetObject.GetType());
        }

        // 타입 선택 팝업을 그립니다.
        Rect popupPosition = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        int newTypeIndex = EditorGUI.Popup(popupPosition, "Effect Type", selectedTypeIndex, GetInheritedTypeNames());

        // 타입이 변경되었다면 새로운 인스턴스를 생성하여 할당합니다.
        if (newTypeIndex != selectedTypeIndex)
        {
            if (newTypeIndex == -1) // "None" 선택 시
            {
                property.managedReferenceValue = null;
            }
            else
            {
                // Activator를 사용해 선택된 타입의 새 인스턴스를 만듭니다.
                property.managedReferenceValue = Activator.CreateInstance(GetInheritedTypes()[newTypeIndex]);
            }
            property.serializedObject.ApplyModifiedProperties();
            EditorGUI.EndProperty();
            return; // 타입이 바뀌었으므로 이번 프레임 그리기는 여기서 종료
        }

        // --- 2. 선택된 타입에 맞는 속성들 그리기 ---
        if (property.managedReferenceValue != null)
        {
            // 들여쓰기를 추가하여 보기 좋게 만듭니다.
            EditorGUI.indentLevel++;

            // 현재 위치를 한 줄 아래로 이동시킵니다.
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // SerializedProperty를 순회하며 모든 자식 프로퍼티를 그립니다.
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

    // 프로퍼티의 전체 높이를 계산
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float totalHeight = EditorGUIUtility.singleLineHeight; // 타입 선택 팝업 높이

        if (property.managedReferenceValue != null)
        {
            // 각 자식 프로퍼티의 높이를 더합니다.
            foreach (SerializedProperty child in property)
            {
                totalHeight += EditorGUI.GetPropertyHeight(child, true) + EditorGUIUtility.standardVerticalSpacing;
            }
        }
        return totalHeight;
    }


    // --- Helper 함수들 ---
    private static Type[] GetInheritedTypes()
    {
        return new Type[] { typeof(Heal) , typeof(Speed) }; // 여기에 ItemEffect를 상속받는 모든 클래스를 추가
    }

    private static string[] GetInheritedTypeNames()
    {
        return new string[] { "Heal", "Speed" }; // 위 타입 순서와 동일하게 이름 추가
    }
}