using UnityEngine;

public class FallowPlayer : MonoBehaviour
{
    public Transform player;
    void Update()
    {
        transform.position = player.position + new Vector3(0, 0, -1);
    }
}
