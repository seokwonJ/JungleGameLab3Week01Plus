using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bullet;
    [SerializeField]int maxAttackCount = 1;
    public int attackCount;
    public bool isGameOver;

    public CameraController cameraController;
    void Start() 
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        isGameOver = false;
        maxAttackCount += StateManager.Instance.SpearCount;
        attackCount = maxAttackCount;
    }
    void Update()
    {
        // 마우스 클릭 시 이동 시작
        if (Input.GetMouseButtonDown(0) && attackCount > 0)
        {
            if (isGameOver) return;
            Attack();
        }
    }

    public void Attack()
    {
        AttackCountDown();
        GameObject bulletObj = Instantiate(bullet, transform.position, Quaternion.Euler(transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        bulletObj.GetComponent<Spear>().targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (cameraController != null) cameraController.StartShake(0.2f, 0.02f);
    }

    public void AttackCountUp()
    {
        attackCount += 1;
    }

    public void AttackCountDown()
    {
        if (attackCount != 0)
        {
            attackCount -= 1;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            AttackCountUp();
            Destroy(other.gameObject);
        }
    }
}
