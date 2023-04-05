using UnityEngine;

public class HeadContainer : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform headTransform;

    public void UpdateHead()
    {
        headTransform = transform.GetChild(0).transform;
    }

    private void Update()
    {
        if (headTransform == null)
        {
            headTransform = transform.GetChild(0).transform;
            return;
        }
        headTransform.eulerAngles = new Vector3(headTransform.eulerAngles.x, headTransform.eulerAngles.y + Time.deltaTime * speed, headTransform.eulerAngles.z);
    }
}