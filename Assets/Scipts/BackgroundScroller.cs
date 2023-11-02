using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float scrollspeed = 0.5f;
    private float offset;
    private Material mat;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //scrollspeed = Movement.Instance.GetMove()/5f;
        offset += (Time.deltaTime * scrollspeed) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
