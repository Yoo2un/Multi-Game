using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Billboard : MonoBehaviourPun
{
    public Text nickName;
    private void Start()
    {
        nickName.text = photonView.Owner.NickName;
    }
    private void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
