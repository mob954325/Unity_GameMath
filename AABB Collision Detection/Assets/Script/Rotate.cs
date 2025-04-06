using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float turnSpeed = 30f;

    private void Update() {
        transform.Rotate(new Vector3(0, 0, 1) * turnSpeed * Time.deltaTime);
    }
}
