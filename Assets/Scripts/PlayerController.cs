using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public enum PlayerState
{
    Intro, Run, Fire, Win, Dead
}
public class PlayerController : Singleton<PlayerController>
{

    [SerializeField] private PlayerAnimationController animationController;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sideSpeed = .05f;
    [SerializeField] private Transform visualTransform;
    [SerializeField] private BulletController bulletController;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private int currentBullet = 10;
    [SerializeField,Foldout("Shield")] private Transform shield;
    [SerializeField,Foldout("Shield")] private float shieldDuration = 2f;

    

    private Vector3 previousMousePos;
    private PlayerState state;
    private bool isShieldOpen;
    private bool isTouching = false;
    private bool isFireable = true;
    private float shieldTimer;
    private void Start()
    {
        InGamePanelController.Instance.UpdateBulletText(currentBullet);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePos = Input.mousePosition;
            isTouching = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isTouching = false;
        }

        if (state == PlayerState.Run || state == PlayerState.Fire)
        {
            MoveForward();
        }

        if (state == PlayerState.Run && isTouching)
        {
            SideMove();
            animationController.PlayRunAnim();
        }

        if (state == PlayerState.Run && !isTouching && isFireable)
        {
            state = PlayerState.Fire;
            animationController.PlayFireAnim();
        }


        if (state == PlayerState.Fire && isTouching)
        {
            state = PlayerState.Run;
        }

        if (isShieldOpen)
        {
            ShieldTimeCheck();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isShieldOpen)
        {
            animationController.PlayDieAnim();
            state = PlayerState.Dead;
            GameManager.Instance.GameOver();
        }

        if (other.CompareTag("FinishLine"))
        {
            animationController.PlayDanceAnim();
            state = PlayerState.Win;
            GameManager.Instance.LevelCompleted();
        }

        if (other.CompareTag("Coin"))
        {
            Destroy(other.attachedRigidbody.gameObject);
            IncreaseCoinAmount();
            InGamePanelController.Instance.UpdateCoinText();

        }
        
        if (other.CompareTag("BulletBox"))
        {
            var bulletBox = other.attachedRigidbody.GetComponent<BulletBoxController>();
            currentBullet += bulletBox.BulletCount;
            Destroy(bulletBox.gameObject);

            isFireable=true;

            if (state !=PlayerState.Fire && !isTouching)
            {
                state = PlayerState.Fire;
                animationController.PlayFireAnim();
            }

            InGamePanelController.Instance.UpdateBulletText(currentBullet);
        }

        if (other.CompareTag("ShieldBoost"))
        {
            shield.gameObject.SetActive(true);
            isShieldOpen=true;
            shieldTimer = 0;
            Destroy(other.attachedRigidbody.gameObject);
        }

        if (other.CompareTag("Wall"))
        {
            if (isShieldOpen)
            {
                var wall = other.attachedRigidbody.GetComponent<WallController>();
                wall.DestuructWall();
            }
            else
            {
                animationController.PlayDieAnim();
                state = PlayerState.Dead;
                GameManager.Instance.GameOver();
            }
        }

    }

    public void StartToRun()
    {
        state = PlayerState.Run;
    }

    private void MoveForward()
    {
        transform.position += new Vector3(0, 0, moveSpeed) * Time.deltaTime;
    }
    private void SideMove()
    {
        var difference = Input.mousePosition - previousMousePos;
        visualTransform.position += new Vector3(difference.x * sideSpeed, 0, 0) * Time.deltaTime;

        var currentPos = visualTransform.position.x;
        currentPos = Mathf.Clamp(currentPos, -4f, 4f);
        visualTransform.position = new Vector3(currentPos, visualTransform.position.y, visualTransform.position.z);

        previousMousePos = Input.mousePosition;
    }


    public void Shoot()
    {
        var bullet = Instantiate(bulletController);
        bullet.transform.position = shootPoint.position;
        bullet.SetPosition(shootPoint.position);
        DecreaseAmountBullet();
        bullet.Shoot();
       
    }

    private void IncreaseCoinAmount()
    {
        var currentCoinAmount = PlayerPrefs.GetInt("CoinAmount", 0);
        currentCoinAmount += 1;
        PlayerPrefs.SetInt("CoinAmount", currentCoinAmount);
    }

    public void DecreaseAmountBullet()
    {

        if (currentBullet > 0)
        {
            currentBullet -= 1;
            Debug.Log("MevcutMermi" + currentBullet);
        }

        if (currentBullet <= 0)
        {
            isFireable = false;
            animationController.PlayRunAnim();
            state=PlayerState.Run;
        }

        InGamePanelController.Instance.UpdateBulletText(currentBullet);
    }

    private void ShieldTimeCheck()
    {
        shieldTimer+= Time.deltaTime;
       
        if (shieldTimer >= shieldDuration)
        {
            shield.gameObject.SetActive(false);
            isShieldOpen = false;
            shieldTimer = 0;
        }
    }
}
