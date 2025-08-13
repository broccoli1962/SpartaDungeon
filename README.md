# SpartaDungeon

## 진행상황
- 필수 기능

캐릭터 기본 이동 및 점프 Input System, Rigidbody ForceMode O

체력바 UI O

동적 환경 조사 Raycast, UI O

점프대 O

아이템 데이터 O

아이템 사용 O
  
- 도전 기능
  
추가 UI O

3인칭 시점 O

움직이는 플랫폼 구현 O

벽 타기 및 매달리기 O

다양한 아이템 구현 O

장비 장착 △

레이저 트랩 O

상호작용 가능한 오브젝트 표시 X

플랫폼 발사기 X

발전된 AI X

## 트러블 슈팅

상황 : Abstract Class를 scriptableObject를 이용하여 Inspector에 표출시키고 해당 class를 상속받는 subObject들의 변수값을 아이템별로 개별 할당을 하고 싶었다.

원인 : Abstract Class는 Inspector에서 뜨지 않는다.

해결 : CustomPropertyDrawer와 SerializableReference 를 이용하여 하위 SubObject들을 표출시켰다.

--

상황 : 벽 점프를 제작하면서 벽에 붙었을 때 앞으로 움직일 경우 벽에 달라붙어서 움직이지 않는 일이 발생하였다.

해결 : bool 변수를 만들어 true일 경우 기존 move() 함수 대신 climb()이라는 함수를 작동시켜서 평지의 움직임과 벽에 붙었을때 움직임을 서로 다르게 처리하였다.
