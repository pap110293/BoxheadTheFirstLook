﻿//----------------------------------------------
//      UnitZ Battleground : Online PVP starter kit
//    Copyright © Hardworker studio 2018 
// by Rachan Neamprasert www.hardworkerstudio.com

using UnityEngine;
using UnityEngine.Networking;

public enum BlendAxis
{
    X, Y, Z
}

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    public HumanBodyBones UpperChest = HumanBodyBones.UpperChest;
    public Transform headCamera;
    [HideInInspector]
    public Quaternion UpperBodyRotation;
    public BlendAxis Axis = BlendAxis.Z;
    public Vector3 Offset;
    public float Speed = -1;
    private CharacterSystem character;
    [SyncVar]
    float cameraRotationSync = 0;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        character = this.GetComponent<CharacterSystem>();
        if (headCamera == null)
        {
            FPSCamera fpscam = this.GetComponentInChildren<FPSCamera>();
            headCamera = fpscam.gameObject.transform;
        }
    }

    [Command(channel = 1)]
    void CmdCameraUpdate(float camRotaion)
    {
        cameraRotationSync = camRotaion;
    }

    float timeTmpsending;
    void Update()
    {
        if (animator == null || character == null)
            return;


        // update upper body rotation to host
        UpperBodyRotation = Quaternion.identity;
        float headCameraRotation = headCamera.transform.localRotation.eulerAngles.x;
        if (character.MovementPreset.Length > character.MovementIndex)
            headCameraRotation += character.MovementPreset[character.MovementIndex].UpperChestOffset;
        switch (Axis)
        {
            case BlendAxis.X:
                UpperBodyRotation.eulerAngles = new Vector3(Speed * headCameraRotation + Offset.x, UpperBodyRotation.eulerAngles.y + Offset.y, UpperBodyRotation.eulerAngles.z + Offset.z);
                break;
            case BlendAxis.Y:
                UpperBodyRotation.eulerAngles = new Vector3(UpperBodyRotation.eulerAngles.x + Offset.x, Speed * headCameraRotation + Offset.y, UpperBodyRotation.eulerAngles.z + Offset.z);
                break;
            case BlendAxis.Z:
                UpperBodyRotation.eulerAngles = new Vector3(UpperBodyRotation.eulerAngles.x + Offset.x, UpperBodyRotation.eulerAngles.y + Offset.y, Speed * headCameraRotation + Offset.z);
                break;
        }

        float fps = (1 / Time.deltaTime);
        float delay = (fps / character.currentSendingRate) * Time.deltaTime;
        if (Time.time > timeTmpsending + delay)
        {
            CmdCameraUpdate(headCameraRotation);
            timeTmpsending = Time.time;
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetBoneLocalRotation(UpperChest, UpperBodyRotation);
    }
}
