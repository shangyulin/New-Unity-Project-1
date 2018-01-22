using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Transform tr;
    private PhotonView pv;
    void Start()
    {
        tr = GetComponent<Transform>();
        pv = GetComponent<PhotonView>();
    }
	// Update is called once per frame
	void Update () {
        if(pv.isMine)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            transform.Rotate(Vector3.up * h * 120 * Time.deltaTime);
            transform.Translate(Vector3.forward * v * 3 * Time.deltaTime);
        }
        else
        {
            tr.position = Vector3.Lerp(tr.position, currPos, Time.deltaTime * 3.0f);
            tr.rotation = Quaternion.Lerp(tr.rotation, currRot, Time.deltaTime * 3.0f);
        }
        
    }
    private Vector3 currPos = Vector3.zero;
    private Quaternion currRot = Quaternion.identity;
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
        }
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
