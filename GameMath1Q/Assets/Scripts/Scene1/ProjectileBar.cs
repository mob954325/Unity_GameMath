using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBar : MonoBehaviour
{
    Camera mainCam;

    // 스폰 이후 행동
    // 시작 후 선형 보간으로 최종 이동할 위치를 WarningImagePivot을 늘려서 알려줌
    // 2초 뒤 WarningImage는 없어지고 AttackImage가 해당 위치까지 늘어남
    // 늘어난 뒤 다시 줄어들고 해당 오브젝트 제거

    private void Awake()
    {
        
    }

    private float SetMaxSize()
    {
        return 1f;
    }
}
