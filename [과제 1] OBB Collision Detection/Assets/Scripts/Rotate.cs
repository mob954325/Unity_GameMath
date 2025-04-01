using UnityEngine;

public class Rotate : MonoBehaviour
{
    private float rotationSpeed = 45f;

    private void Update() {
        transform.Rotate(new Vector3(0, 0, 1) * -rotationSpeed * Time.deltaTime);
    }
}
