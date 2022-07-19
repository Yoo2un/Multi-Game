using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PhotonControl : MonoBehaviourPun
{
    public float speed;
    public float angleSpeed;

    public Camera cam;

    public void Start()
    {
        //현재 플레이어가 나 자신이라면
        if(photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else //자신이 아닌 카메라는 다 끔/오디오도 끔
        {
            cam.enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }
    private void Update()
    {
        if (!photonView.IsMine) return; //이동/회전 X
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        transform.Translate(dir.normalized * speed * Time.deltaTime);
        transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * angleSpeed * Time.deltaTime, 0);
    }
}
