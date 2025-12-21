using UnityEngine;

/// <summary>
/// Dark storm clouds that get illuminated when lightning strikes.
/// Creates a simple cloud layer overhead.
/// </summary>
public class DarkCloudSystem : MonoBehaviour
{
    [Header("Cloud Layer")]
    public float cloudHeight = 80f;
    public float cloudSize = 400f;
    public int cloudCount = 25;

    [Header("Cloud Appearance")]
    public Color cloudColorDark = new Color(0.05f, 0.06f, 0.08f, 0.9f);
    public Color cloudColorLit = new Color(0.4f, 0.45f, 0.5f, 0.9f);
    public float minCloudScale = 30f;
    public float maxCloudScale = 80f;

    [Header("Movement")]
    public float driftSpeed = 2f;
    public Vector2 driftDirection = new Vector2(1f, 0.3f);

    private GameObject[] clouds;
    private Material[] cloudMaterials;
    private float[] cloudLitAmount;
    private Vector3[] cloudStartPositions;

    void Start()
    {
        CreateClouds();
    }

    void CreateClouds()
    {
        clouds = new GameObject[cloudCount];
        cloudMaterials = new Material[cloudCount];
        cloudLitAmount = new float[cloudCount];
        cloudStartPositions = new Vector3[cloudCount];

        GameObject cloudParent = new GameObject("Clouds");
        cloudParent.transform.SetParent(transform);

        for (int i = 0; i < cloudCount; i++)
        {
            // Random position in cloud layer
            float x = Random.Range(-cloudSize / 2f, cloudSize / 2f);
            float z = Random.Range(-cloudSize / 2f, cloudSize / 2f);
            float y = cloudHeight + Random.Range(-10f, 10f);

            // Create cloud (simple quad facing down)
            GameObject cloud = GameObject.CreatePrimitive(PrimitiveType.Quad);
            cloud.name = "Cloud_" + i;
            cloud.transform.SetParent(cloudParent.transform);
            cloud.transform.position = new Vector3(x, y, z);
            cloud.transform.rotation = Quaternion.Euler(90f, Random.Range(0f, 360f), 0f);

            float scale = Random.Range(minCloudScale, maxCloudScale);
            cloud.transform.localScale = new Vector3(scale, scale, 1f);

            // Remove collider
            Destroy(cloud.GetComponent<Collider>());

            // Material
            Material mat = CreateCloudMaterial();
            cloud.GetComponent<Renderer>().material = mat;
            cloud.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            clouds[i] = cloud;
            cloudMaterials[i] = mat;
            cloudLitAmount[i] = 0f;
            cloudStartPositions[i] = cloud.transform.position;
        }
    }

    Material CreateCloudMaterial()
    {
        Material mat = new Material(Shader.Find("Particles/Standard Unlit"));
        mat.SetColor("_Color", cloudColorDark);

        // Create soft cloud texture
        Texture2D tex = CreateCloudTexture();
        mat.mainTexture = tex;

        // Transparency
        mat.SetFloat("_Mode", 2);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.renderQueue = 2999;

        return mat;
    }

    Texture2D CreateCloudTexture()
    {
        int size = 128;
        Texture2D tex = new Texture2D(size, size, TextureFormat.RGBA32, false);

        Color[] pixels = new Color[size * size];
        Vector2 center = new Vector2(size / 2f, size / 2f);

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float dist = Vector2.Distance(new Vector2(x, y), center) / (size / 2f);

                // Soft circular falloff with noise
                float noise = Mathf.PerlinNoise(x * 0.05f, y * 0.05f);
                float alpha = Mathf.Clamp01(1f - dist) * noise;
                alpha = Mathf.Pow(alpha, 0.7f);

                pixels[y * size + x] = new Color(1f, 1f, 1f, alpha);
            }
        }

        tex.SetPixels(pixels);
        tex.Apply();
        tex.wrapMode = TextureWrapMode.Clamp;

        return tex;
    }

    void Update()
    {
        if (clouds == null) return;

        // Move clouds slowly
        Vector3 drift = new Vector3(driftDirection.x, 0, driftDirection.y).normalized * driftSpeed * Time.deltaTime;

        for (int i = 0; i < cloudCount; i++)
        {
            if (clouds[i] == null) continue;

            // Drift
            clouds[i].transform.position += drift;

            // Wrap around when too far
            Vector3 pos = clouds[i].transform.position;
            Vector3 camPos = Camera.main != null ? Camera.main.transform.position : Vector3.zero;

            if (pos.x > camPos.x + cloudSize / 2f) pos.x -= cloudSize;
            if (pos.x < camPos.x - cloudSize / 2f) pos.x += cloudSize;
            if (pos.z > camPos.z + cloudSize / 2f) pos.z -= cloudSize;
            if (pos.z < camPos.z - cloudSize / 2f) pos.z += cloudSize;

            clouds[i].transform.position = pos;

            // Fade lit amount back to dark
            if (cloudLitAmount[i] > 0)
            {
                cloudLitAmount[i] -= Time.deltaTime * 3f;
                cloudLitAmount[i] = Mathf.Max(0, cloudLitAmount[i]);

                Color col = Color.Lerp(cloudColorDark, cloudColorLit, cloudLitAmount[i]);
                cloudMaterials[i].SetColor("_Color", col);
            }
        }
    }

    /// <summary>
    /// Call this when lightning strikes to illuminate nearby clouds
    /// </summary>
    public void IlluminateClouds(Vector3 lightningPosition, float radius = 200f)
    {
        if (clouds == null) return;

        for (int i = 0; i < cloudCount; i++)
        {
            if (clouds[i] == null) continue;

            float dist = Vector3.Distance(clouds[i].transform.position, lightningPosition);
            if (dist < radius)
            {
                float intensity = 1f - (dist / radius);
                cloudLitAmount[i] = Mathf.Max(cloudLitAmount[i], intensity);

                Color col = Color.Lerp(cloudColorDark, cloudColorLit, cloudLitAmount[i]);
                cloudMaterials[i].SetColor("_Color", col);
            }
        }
    }
}