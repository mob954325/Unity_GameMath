using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스폰 이후 행동
// 시작 후 선형 보간으로 최종 이동할 위치를 WarningImagePivot을 늘려서 알려줌
// 2초 뒤 WarningImage는 없어지고 AttackImage가 해당 위치까지 늘어남
// 늘어난 뒤 다시 줄어들고 해당 오브젝트 제거

public class ProjectileBar : MonoBehaviour
{
    Camera mainCam;                     // 메인 카메라
    Transform attackImagePivot;         // 공격 이미지 피벗
    Transform warningImagePivot;        // 공격 예정 이미지 피벗

    private float targetViewportPositionX;    // 이 투사체 목표로하는 x좌표값
    private bool isLeft = false;        // 이 오브젝트가 카메라 중심을 기준으로 왼쪽에 있는지 확인하는 변수

    private void Awake()
    {
        mainCam = Camera.main;
        attackImagePivot = transform.GetChild(0);
        warningImagePivot = transform.GetChild(1);        
    }

    private void Start()
    {
        Vector2 objectViewportPosition = mainCam.WorldToViewportPoint(transform.position);
        isLeft = objectViewportPosition.x < 0.5f ? true : false;
        if(!isLeft)
        {
            attackImagePivot.localScale = new Vector3(-1f, 1f, 1f);
            warningImagePivot.localScale = new Vector3(-1f, 1f, 1f);
        }

        targetViewportPositionX = UnityEngine.Random.Range(0.4f, 0.6f);
        //targetViewportPositionX = 0.8f;

        StartCoroutine(AttackProcess()); // 공격 코루틴 시작 (4초)
    }

    private IEnumerator AttackProcess()
    {
        yield return null;
    }
}
