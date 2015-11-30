using UnityEngine;
using System.Collections;

public class WallBlock : MonoBehaviour
{
    private bool _up;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var target = (_up ? 0.5f : -0.55f) * transform.localScale.y;

        if (transform.localPosition.y < 0.5)
        {
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                transform.localPosition.y + Mathf.Clamp(target - transform.localPosition.y, -Time.fixedDeltaTime, Time.fixedDeltaTime),
                transform.localPosition.z
            );
        }
    }

    void Drop()
    {
        _up = false;
    }

    void Rise()
    {
        _up = true;
    }
}
