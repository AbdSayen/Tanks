using UnityEngine;

public class Menu : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3(0, 10, 0) * Time.deltaTime);
    }
}