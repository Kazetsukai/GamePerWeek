using UnityEngine;
using System.Collections;

public class WallBlock : MonoBehaviour
{
    private bool _up;

    public float Delay = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var target = (_up ? 0.5f : -0.55f) * transform.localScale.y;

        if (transform.localPosition.y != target)
        {
            if (Delay > 0)
                Delay -= Time.fixedDeltaTime;
            else
            {
                var randomFactor = Random.Range(0f, 1f);
                transform.localPosition = new Vector3(
                    transform.localPosition.x,
                    transform.localPosition.y + Mathf.Clamp(target - transform.localPosition.y, -Time.fixedDeltaTime, Time.fixedDeltaTime),
                    transform.localPosition.z
                );
            }
        }
    }

    public void Drop()
    {
        _up = false;
    }

    public void Rise()
    {
        _up = true;
    }

    public bool IsUp { get { return _up; } }
}
