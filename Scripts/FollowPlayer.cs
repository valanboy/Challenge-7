using UnityEngine;
using UnityEngine.UI;

public class FollowPlayer : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;
    private float smoothSpeed = 0.04f;

    

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        offset = transform.position - target.position;

        
    }

  
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed);
        transform.position = newPosition;
    }
}
