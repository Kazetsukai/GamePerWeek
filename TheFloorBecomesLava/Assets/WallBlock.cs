using UnityEngine;

public class WallBlock : MonoBehaviour
{
    private bool _up;

    public float Delay = 0;

    public Material Ground;
    public Material Wall;
    public Material Lava;
    public Material AlmostLava;

    public float  LavaTime;

    Renderer renderer;

    // Use this for initialization
    void Start()
    {
    }
    
    void FixedUpdate()
    {
        var target = (_up ? 0.5f : -0.48f) * transform.localScale.y;

        if (transform.localPosition.y != target)
        {
            if (Delay > 0)
                Delay -= Time.fixedDeltaTime;
            else
            {
                transform.localPosition = new Vector3(
                    transform.localPosition.x,
                    transform.localPosition.y + Mathf.Clamp(target - transform.localPosition.y, -Time.fixedDeltaTime, Time.fixedDeltaTime),
                    transform.localPosition.z
                );
            }
        }

        LavaTime -= MazeGenerator.LavaSpeed * Time.fixedDeltaTime;

        if (LavaTime < 3 && LavaTime >= 0 && !_up)
            SetMaterial(AlmostLava);
        if (LavaTime < 0 && !_up)
            SetMaterial(Lava);
    }

    private void SetMaterial(Material material)
    {
        if (renderer == null)
            renderer = GetComponent<Renderer>();

        renderer.material = material;
    }

    public void Drop()
    {
        _up = false;
        SetMaterial(Ground);
    }


    public void Rise()
    {
        _up = true;
        SetMaterial(Wall);
    }

    public bool IsUp { get { return _up; } }
}
