using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(CharacterSkinController))]
public class Player : LivingEntity
{
    Camera viewCamera;
    PlayerController playerController;
    CharacterSkinController characterSkinController;

    public static Player Instance { get; protected set; }

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        viewCamera = Camera.main;
        playerController = GetComponent<PlayerController>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        GetInput();
    }

    void GetInput()
    {
        GetMovementInput();
        GetLookInput();
    }

    void GetMovementInput()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        playerController.Move(moveInput);
    }

    void GetLookInput()
    {
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            playerController.LookAt(point);
        }
    }
}
