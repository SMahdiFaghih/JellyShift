using UnityEngine;

public class CycleBackgroundColor : MonoBehaviour
{
    private Camera mainCamera;
    public Color[] colors;
    private int currentColorIndex = 0;
    private float lerpScale = 0;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        mainCamera.backgroundColor = colors[currentColorIndex];
    }

    // Update is called once per frame
    void Update()
    {
        lerpScale += Time.deltaTime;
        mainCamera.backgroundColor = Color.Lerp(colors[currentColorIndex], colors[(currentColorIndex + 1) % colors.Length], speed * lerpScale);
        CheckCircumstancesToSetColors();
        if(speed * lerpScale >= 1)
        {
            lerpScale = 0;
        }
    }

    private void CheckCircumstancesToSetColors()
    {
        if(mainCamera.backgroundColor.Equals(colors[(currentColorIndex + 1) % colors.Length]))
        {
            currentColorIndex = (currentColorIndex + 1) % colors.Length;
        }
    }
}
