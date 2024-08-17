using UnityEngine;

public class Tripot : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, 0.1f);
        transform.rotation = target.transform.rotation;
    }
}