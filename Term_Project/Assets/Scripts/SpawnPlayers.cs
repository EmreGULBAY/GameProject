﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public Cinemachine.CinemachineFreeLook CMFreeLook;
    public Camera mainCamera;

    // boundaries to generate random location for the player
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    Vector3 targetPosition = new Vector3(0, 37.6f, 0);

    private Quaternion _lookRotation;
    private Vector3 _direction;
    private PhotonAnimatorView photonAnimatorView;
    // Start is called before the first frame update
    void Start()
    {
        // pick random ponint for x and z
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        // generate Vector3 for random position
        Vector3 randomPosition = new Vector3(randomX, 40f, randomZ);

        // find the vector pointing from our position to the target
        _direction = (targetPosition - randomPosition).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);
        _lookRotation.eulerAngles = new Vector3(0, _lookRotation.eulerAngles.y, 0);


        // initiate player 
        GameObject clonePlayer = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, _lookRotation, 0);

        // sync animations
        photonAnimatorView = clonePlayer.GetComponent<PhotonAnimatorView>();
        photonAnimatorView.SetParameterSynchronized("walk", PhotonAnimatorView.ParameterType.Bool, PhotonAnimatorView.SynchronizeType.Discrete);
        photonAnimatorView.SetParameterSynchronized("jump", PhotonAnimatorView.ParameterType.Bool, PhotonAnimatorView.SynchronizeType.Discrete);
        photonAnimatorView.SetParameterSynchronized("isDead", PhotonAnimatorView.ParameterType.Bool, PhotonAnimatorView.SynchronizeType.Discrete);
        photonAnimatorView.SetParameterSynchronized("Blend", PhotonAnimatorView.ParameterType.Float, PhotonAnimatorView.SynchronizeType.Discrete);
        photonAnimatorView.SetParameterSynchronized("onGround", PhotonAnimatorView.ParameterType.Bool, PhotonAnimatorView.SynchronizeType.Discrete);
        photonAnimatorView.SetParameterSynchronized("roll", PhotonAnimatorView.ParameterType.Bool, PhotonAnimatorView.SynchronizeType.Discrete);
        photonAnimatorView.SetParameterSynchronized("up", PhotonAnimatorView.ParameterType.Bool, PhotonAnimatorView.SynchronizeType.Discrete);
        photonAnimatorView.SetParameterSynchronized("right", PhotonAnimatorView.ParameterType.Bool, PhotonAnimatorView.SynchronizeType.Discrete);
        photonAnimatorView.SetParameterSynchronized("down", PhotonAnimatorView.ParameterType.Bool, PhotonAnimatorView.SynchronizeType.Discrete);
        photonAnimatorView.SetParameterSynchronized("left", PhotonAnimatorView.ParameterType.Bool, PhotonAnimatorView.SynchronizeType.Discrete);
        photonAnimatorView.SetParameterSynchronized("shieldUp", PhotonAnimatorView.ParameterType.Bool, PhotonAnimatorView.SynchronizeType.Discrete);
        photonAnimatorView.SetLayerSynchronized(0, PhotonAnimatorView.SynchronizeType.Discrete);
        photonAnimatorView.SetLayerSynchronized(1, PhotonAnimatorView.SynchronizeType.Discrete);
        //// set main camera to inactive
        //mainCamera.gameObject.SetActive(false);

        // check if player is local player
        if (clonePlayer.GetComponent<PhotonView>().IsMine)
        {
            // set the player to follow the camera
            InitializeCMFreeLook(clonePlayer, CMFreeLook, mainCamera);

            //// set main camera to inactive
            //mainCamera.gameObject.SetActive(true);
        }


    }


    void InitializeCMFreeLook(GameObject player, Cinemachine.CinemachineFreeLook CMFreeLook, Camera mainCamera)
    {
        CMFreeLook.Follow = player.transform;
        CMFreeLook.LookAt = player.transform;

        player.GetComponent<ThirdPersonMovement>().cam = mainCamera.transform;

    }
}
