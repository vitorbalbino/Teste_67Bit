using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] Material _CharSkinMaterial;
    [SerializeField] SkinnedMeshRenderer _CharRenderer;
    float h, s, v;



    void Start()
    {
        _CharSkinMaterial = _CharRenderer.material = new Material(_CharRenderer.material);
    }



    public void UpdateMatColor(int upgrade)
    {
        Color.RGBToHSV(_CharSkinMaterial.color, out float h, out float s, out float v);
        float newH = (h + (0.05f * upgrade)) % 1;
        Debug.Log(newH);
        _CharSkinMaterial.color = Color.HSVToRGB(newH, s, v);
    }
}
