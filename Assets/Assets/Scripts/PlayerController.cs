using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json.Bson;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float crouchSpeed;
    [SerializeField]
    private float jumpForce;

    private bool isWalk = false;
    private bool isRun = false;
    private bool isGround = true;
    private bool isCrouch = false;

    private Vector3 lastPos;

    [SerializeField]
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;

    private CapsuleCollider capsuleCollider;
    //민감도
    [SerializeField]
    private float lookSensitivity;
    [SerializeField]
    private float cameraRotationLimit;
    [SerializeField]
    private Camera theCamera;
    private float currentCameraRotationX = 0;
    private float applySpeed;
    [SerializeField]
    private Gun_Controller theGunController;
    private Crosshair theCrosshair;
    private StatusController theStatusController;

    private Rigidbody myRigid;
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
        theCrosshair = FindObjectOfType<Crosshair>();
        applySpeed = walkSpeed;
        originPosY = theCamera.transform.localPosition.y;
        applyCrouchPosY = originPosY;
        theGunController = FindObjectOfType<Gun_Controller>();
        theStatusController = FindObjectOfType<StatusController>();
    }

    void Update()
    {
        IsGround();
        TryJump();
        TryRun();
        TryCrouch();
        Move();
        MoveCheck();
        if (Inventory.inventoryActivated)
        {
            CameraRotation();
            CharacterRotation();
        }
    }

    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized*applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }
    private void CameraRotation()
    {
        //상하 카메라 회전
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0, 0);
    }
    private void CharacterRotation()
    {
        //좌우 캐리거 회전
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0, _yRotation, 0) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }
    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift)&& theStatusController.GetCurrentSP() > 0)
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)|| theStatusController.GetCurrentSP() <= 0)
        {
            RunningCancel();
        }
    }
    private void Running()
    {
        if(isCrouch)
        {
            Crouch();
        }
        theGunController.CancelFineSight();

        isRun = true;
        theCrosshair.RunningAnimation(isRun);
        theStatusController.DecreaseStamina(10);
        applySpeed = runSpeed;
    }
    private void RunningCancel()
    {
        isRun = false;
        theCrosshair.RunningAnimation(isRun);
        applySpeed = walkSpeed;
    }
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& isGround&&theStatusController.GetCurrentSP()>0)
        {
            Jump();
        }
    }
    private void Jump()
    {
        if (isCrouch)
        {
            Crouch();
        }
        theStatusController.DecreaseStamina(100);
        myRigid.velocity = transform.up * jumpForce;
    }
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y+0.1f);
        theCrosshair.JumpingAnimation(!isGround);
    }
    private void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }
    private void Crouch()
    {
        isCrouch = !isCrouch;
        theCrosshair.CrouchingAnimation(isCrouch);
        if (isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }
        StartCoroutine(CrouchCoroutine());
    }
    IEnumerator CrouchCoroutine()
    {
        float _posY = theCamera.transform.localPosition.y;
        int count = 0;

        while (_posY != applyCrouchPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, applyCrouchPosY, 0.3f);
            theCamera.transform.localPosition = new Vector3(0, _posY, 0);
            if (count > 15)
            {
                break;
            }
            yield return null;
        }
        theCamera.transform.localPosition = new Vector3(0, crouchPosY, 0);
    }
    private void MoveCheck()
    {
        if (!isRun && !isCrouch&& isGround)
        { 
            if (Vector3.Distance(lastPos,transform.position)>=0.01f)
            {
                isWalk = true;
            }
            else
                isWalk = false;
            theCrosshair.WalkingAnimation(isWalk);
            lastPos = transform.position;
        }
    }
}
