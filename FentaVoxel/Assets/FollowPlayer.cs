using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private Vector3 _offset;

    [SerializeField] private float _smoothSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desirePos = _player.position + _offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desirePos, _smoothSpeed * Time.deltaTime);

        transform.position = smoothPos;
        transform.LookAt(_player);
    }
}
