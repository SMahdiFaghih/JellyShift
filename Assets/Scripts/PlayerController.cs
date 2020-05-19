using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public bool isJumping = false;

    public float moveForce = 1200f;
    public float shiftFactor = 2f;
    public float jumpForce = 12000f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.forward * moveForce * Time.deltaTime);

        float inputValue = Input.GetAxis("Vertical");
        if (inputValue != 0 && !isJumping)
        {
            CalculateNewScale(inputValue);
        }
        float jumpValue = Input.GetAxis("Jump");
        if (jumpValue != 0 && !isJumping)
        {
            rb.AddForce(transform.up * jumpForce * Time.deltaTime);
            isJumping = true;

            AudioSource jumpSound = GetComponents<AudioSource>()[1];
            jumpSound.Play();
        }
    }

    private void CalculateNewScale(float inputValue)
    {
        float xScaleShiftAmout = -inputValue * shiftFactor * Time.deltaTime;
        float yScaleShiftAmout = inputValue * shiftFactor * Time.deltaTime;
        if (transform.localScale.y < 1)
        {
            yScaleShiftAmout = yScaleShiftAmout * 3 / 4;
        }
        else
        {
            xScaleShiftAmout = xScaleShiftAmout * 3 / 4;
        }
        SetNewScale(xScaleShiftAmout, yScaleShiftAmout);
    }

    private void SetNewScale(float xScaleShiftAmout, float yScaleShiftAmout)
    {
        Vector3 newScale = transform.localScale + new Vector3(xScaleShiftAmout, yScaleShiftAmout, 0);
        newScale.x = Mathf.Clamp(newScale.x, 0.25f, 2);
        newScale.y = Mathf.Clamp(newScale.y, 0.25f, 2);
        transform.localScale = newScale;
        float yPosition = (transform.localScale.y + 1) / 2;
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
    }
}