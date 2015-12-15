using UnityEngine;
using System.Collections;

public class CameraFilter : MonoBehaviour
{

    [SerializeField]
    Material mat;  // かけたい効果のマテリアル
    [SerializeField]
    Material mat2;
    public int life_check = 0;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (life_check == 1)
        {
            Graphics.Blit(src, dest, mat);
        }
        else if (life_check == 0)
        {
            Graphics.Blit(src, dest, mat2);
        }
    }
}
