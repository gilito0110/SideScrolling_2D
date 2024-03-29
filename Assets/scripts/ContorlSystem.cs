﻿using UnityEngine;
using System;

public class ContorlSystem : MonoBehaviour
{
    [Header("移動速度"), Range(0.5f, 50.0f)]
    public float moveSpeed = 5f;

    [Header("2D 剛體")]
    public Rigidbody2D rig;

    [Header("跳躍力量"), Range(0.5f, 50)]
    public float jumpForce = 10f;// 跳躍力量

    [Header("跳躍高度"), Range(0.75f, 10)]
    public float jumpHeight = 5f;// 跳躍高度

    [Header("動畫控制器")]
    public Animator ani;

    [Header("跑步參數")]
    public string PerRun = "IF_Run";

    [Header("跳躍次數")]
    public int jumpTime;

    private bool isJumping = false;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpTime = 0;
        ani.SetBool("isJump",false);
    }

    private void Update()
    {
        TalkStop();
    }

    //移動&跳躍
    private void Move()
    {
        // 左右移動
        float moveInput = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(moveInput * moveSpeed, rig.velocity.y);


        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        ani.SetBool(PerRun, moveInput != 0);

        // 按下空白鍵跳躍
        if (Input.GetKeyDown(KeyCode.Space) && jumpTime < 2)
        {
            jumpTime++;
            float jumpVelocity = Mathf.Sqrt(2f * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
            rig.velocity = new Vector2(rig.velocity.x, jumpVelocity);
            isJumping = true;
            ani.SetBool("isJump", true);
        }
    }

    //Fungus開始時停止動作
    private void TalkStop()
    {
        if (!FlowChartManager.TalkBlock)
        {
            Move();
        }
    }

    private void FixedUpdate()
    {
        // 執行跳躍
        if (isJumping)
        {
            rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = false;
        }  
    }


}
