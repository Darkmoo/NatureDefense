using UnityEngine;

public class CameraController : MonoBehaviour {

    public float scrollSpeed = 2f;

    public float minZoom = 20f;

    public float maxZoom = 40f;

    public float panSpeed = 40f;

    public float panBorederThicknes = 15f;

    //Statics
    public float minX, maxX;
    public float minZ, maxZ;


    // Update is called once per frame
    void Update () {

        if (GameManager.GameIsOver)
            return;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorederThicknes)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorederThicknes)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorederThicknes)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorederThicknes)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if (transform.position.x <= minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }

        if (transform.position.z <= minZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
        }
        else if (transform.position.z >= maxZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxZ);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 500 * scrollSpeed * Time.deltaTime;
        if (pos.y <= minZoom)
        {
            pos.y = minZoom;
        }
        else if (pos.y >= maxZoom)
        {
            pos.y = maxZoom;
        }



        transform.position = pos;
    }
}
