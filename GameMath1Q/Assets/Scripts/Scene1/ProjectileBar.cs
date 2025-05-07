using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBar : MonoBehaviour
{
    Camera mainCam;
    Transform attackImagePivot;
    Transform warningImagePivot;

    public float maxViewportValueX; // 이 투사체 목표로하는 x좌표값
    // 스폰 이후 행동
    // 시작 후 선형 보간으로 최종 이동할 위치를 WarningImagePivot을 늘려서 알려줌
    // 2초 뒤 WarningImage는 없어지고 AttackImage가 해당 위치까지 늘어남
    // 늘어난 뒤 다시 줄어들고 해당 오브젝트 제거

    private void Awake()
    {
        mainCam = Camera.main;
        attackImagePivot = transform.GetChild(0);
        warningImagePivot = transform.GetChild(1);        
    }

    private void Start()
    {
        maxViewportValueX = UnityEngine.Random.value; // 0.0 - 1.0
        StartCoroutine(AttackProcess());
    }

    private IEnumerator AttackProcess()
    {
        Vector2 viewportPosition = mainCam.WorldToViewportPoint(transform.position);
        viewportPosition.x += maxViewportValueX;
        Vector2 worldPosition = mainCam.ViewportToWorldPoint(viewportPosition);

        float timeElapsed = 0.0f;
        while(timeElapsed < 1.0f)
        {
            timeElapsed += Time.deltaTime;
            Vector3 localScale = warningImagePivot.localScale;
            float x = Mathf.Lerp(localScale.x, worldPosition.x, timeElapsed);
            warningImagePivot.localScale = new Vector3(x, warningImagePivot.localScale.y, warningImagePivot.localScale.z);
            yield return null;
        }
        warningImagePivot.localScale = new Vector3(viewportPosition.x, warningImagePivot.localScale.y, warningImagePivot.localScale.z);

        yield return new WaitForSeconds(2f);

        attackImagePivot.localScale = new Vector3(viewportPosition.x, warningImagePivot.localScale.y, warningImagePivot.localScale.z);

        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
