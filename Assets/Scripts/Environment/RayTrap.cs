using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTrap : MonoBehaviour
{
    public float rayDistance;
    public LayerMask layermask;

    private void DrawRay()
    {

    }

    void Update()
    {
        Ray ray = new Ray(gameObject.transform.position, gameObject.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * rayDistance, Color.red);
        if (Physics.Raycast(ray, out hit, rayDistance, layermask)){
            Debug.Log("레이저 트랩에 감지되었습니다.");
        }
    }
}
