using UnityEngine;

public class Platform : MonoBehaviour
{
    public Vector3 [] waypoints;
    public float speed = 3f;
    public int currentPoint = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentPoint], speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, waypoints[currentPoint])< 0.2f)
        {
            currentPoint ++;
            if(currentPoint >= waypoints.Length)
            {
                currentPoint = 0;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
            other.transform.SetParent(null);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        foreach(var item in waypoints)
        {
            Gizmos.DrawSphere(item, 0.2f);
        }
    }
}
