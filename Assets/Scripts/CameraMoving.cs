using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    public float _speed = 3f;

    [SerializeField] private Transform _minMargin;
    [SerializeField] private Transform _maxMargin;

    private float yOffset = 1f;

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 targetPos = new Vector3(_player.transform.position.x, _player.transform.position.y + yOffset, transform.position.z);

        Vector3 nextPos = Vector3.Lerp(transform.position, targetPos, _speed * Time.deltaTime);

        float clampedX = Mathf.Clamp(nextPos.x, _minMargin.position.x, _maxMargin.position.x);
        float clampedY = Mathf.Clamp(nextPos.y, _minMargin.position.y, _maxMargin.position.y);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
