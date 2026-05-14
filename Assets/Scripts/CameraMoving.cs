using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public float speed = 3f;

    [SerializeField] private Transform minMargin;
    [SerializeField] private Transform maxMargin;

    private float yOffset = 1f;

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, transform.position.z);

        Vector3 nextPos = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);

        float clampedX = Mathf.Clamp(nextPos.x, minMargin.position.x, maxMargin.position.x);
        float clampedY = Mathf.Clamp(nextPos.y, minMargin.position.y, maxMargin.position.y);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
