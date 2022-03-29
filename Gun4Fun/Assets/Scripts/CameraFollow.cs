using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private Vector3 currentVelocity = Vector3.zero;
    public bool bounds;
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    void LateUpdate(){
        Vector3 desiredPos = target.position + offset;
        if (bounds) {
            desiredPos.x = Mathf.Clamp(desiredPos.x, minCameraPos.x, maxCameraPos.x);
            desiredPos.y = Mathf.Clamp(desiredPos.y, minCameraPos.y, maxCameraPos.y);
        }
        Vector3 smoothedPos = Vector3.SmoothDamp(transform.position, desiredPos, ref currentVelocity, smoothSpeed);
        transform.position = smoothedPos;
    }
}
