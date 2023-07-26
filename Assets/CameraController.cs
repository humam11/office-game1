using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float targetXPosition = 18f;  
    public float transitionSpeed = 2f; 

    public Transform necessaryPurposesBox;

    private bool isAllPurposesSorted = false;

    private Vector3 initialBoxPosition; 

    private void Start()
    {

        if (necessaryPurposesBox != null)
        {
            initialBoxPosition = necessaryPurposesBox.localPosition;
        }
    }

    private void Update()
    {
        if (isAllPurposesSorted)
        {

            Vector3 targetCameraPosition = new Vector3(targetXPosition, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetCameraPosition, transitionSpeed * Time.deltaTime);


            if (necessaryPurposesBox != null)
            {
                Vector3 targetBoxPosition = new Vector3(targetXPosition, necessaryPurposesBox.localPosition.y, necessaryPurposesBox.localPosition.z);
                necessaryPurposesBox.localPosition = Vector3.Lerp(necessaryPurposesBox.localPosition, targetBoxPosition, transitionSpeed * Time.deltaTime);
            }
        }
    }

    public void SetAllPurposesSorted(bool value)
    {
        isAllPurposesSorted = value;
    }
}
