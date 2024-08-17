using System.Collections;
using UnityEngine;
using YG;

public class Movement : MonoBehaviour
{
    private string device;
    private float speed = 2.5f;

    [Header("Keys")]
    [SerializeField] private KeyCode forwardKey;
    [SerializeField] private KeyCode backKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode leftKey;

    [Space(10)]
    [Header("Links")]
    [SerializeField] private GameObject barrel;

    private void Start()
    {
        device = YandexGame.EnvironmentData.deviceType;
    }

    private void FixedUpdate()
    {
        if (GetComponent<AttackSystem>().isAlive)
        {
            Move();
        }
    }

    private void Move()
    {
        if (device == "desktop")
        {
            if (Input.GetKey(forwardKey))
            {
                transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
                transform.Rotate(new Vector3(0, barrel.transform.localRotation.y, 0) * speed / 3 * 225 * Time.deltaTime);
            }
            else if (Input.GetKey(backKey))
            {
                transform.Translate(new Vector3(0, 0, -1) * speed * Time.deltaTime);
                transform.Rotate(new Vector3(0, -barrel.transform.localRotation.y, 0) * speed / 3 * 255 * Time.deltaTime);
            }
            if (Input.GetKey(leftKey) && barrel.transform.localRotation.y > -0.8f)
            {
                barrel.transform.Rotate(new Vector3(0, -140, 0) * Time.deltaTime);
            }
            else if (Input.GetKey(rightKey) && barrel.transform.localRotation.y < 0.8f)
            {
                barrel.transform.Rotate(new Vector3(0, 140, 0) * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "boost")
        {
            switch (collision.gameObject.GetComponent<Boost>().type)
            {
                case BoostType.speed:
                    StartCoroutine(AddSpeedBoost());
                    Destroy(collision.gameObject);
                    break;
                default:
                    break;
            }
        }
    }

    private IEnumerator AddSpeedBoost()
    {
        speed = 6.5f;
        yield return new WaitForSeconds(7);
        speed = 2.5f;
    }
}