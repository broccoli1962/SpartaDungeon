using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    //uicondition을 프리팹화해서 생성
    //어떻게해야지 관리가 편할까? 컬렉션
    public UICondition uiCondition;
}
