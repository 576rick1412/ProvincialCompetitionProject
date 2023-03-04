using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Airship
{
    public float oil;           // 연료
    public float oilMinus;      // 연료 연소 속도

    public Vector2 moveRestri;  // 이동제한
                                // 3.8 , 6

    public Attack attack;

    public override void Awake()
    {
        base.Awake();

        attack.isBulletCoolTime = true;
        attack.nowAMMO = attack.maxAMMO;
    }

    public override void Start()
    {
        
    }

    public override void Update()
    {
        if (oil <= 0) Die();
        else oil -= oilMinus * Time.deltaTime;

        PlayerControl();
    }

    void PlayerControl()
    {
        {
            Vector2 nextPos = new Vector2(0, 0);
            nextPos.x = Input.GetAxisRaw("Horizontal");
            nextPos.y = Input.GetAxisRaw("Vertical");

            BordarCheck(nextPos);   // 이동도 들어있음
        }   // 이동 입력

        if(Input.GetKeyDown(KeyCode.R) && attack.isBulletCoolTime)
        {
            if (attack.nowAMMO >= attack.maxAMMO) return;   // 탄약 최대일 시 장전 불가

            attack.nowAMMO = 0;
            StartCoroutine(Fire());
        }   // 재장전

        ShotBullet();   // 총알 발사
    }
    void BordarCheck(Vector2 nextPos)
    {
        if (transform.position.x + nextPos.x >=  moveRestri.x ||
            transform.position.x + nextPos.x <= -moveRestri.x)
            nextPos.x = 0f;

        if (transform.position.y + nextPos.y >=  moveRestri.y ||
            transform.position.y + nextPos.y <= -moveRestri.y)
            nextPos.y = 0f;

        Move(nextPos);
    }


    void ShotBullet()
    {
        if (Input.GetKey(KeyCode.Space) && attack.isBulletCoolTime)
            StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        attack.isBulletCoolTime = false;

        if (attack.nowAMMO > 0)
        {
            var bullet = Instantiate(attack.bulletObject[0], transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * attack.bulletSpeed, ForceMode2D.Impulse);
            attack.nowAMMO--;   // 탄창에서 1발 차감

            yield return new WaitForSeconds(attack.bulletCoolTime);
        }
        else
        {
            attack.reloadBar = attack.reloadTime;
            int times = 40;
            for (int i = 0; i < times; i++)
            {
                attack.reloadBar -= (attack.reloadTime / times);
                yield return new WaitForSeconds(attack.reloadTime / times);
            }
            
            attack.nowAMMO = attack.maxAMMO;
            // 재장전
        }

        attack.isBulletCoolTime = true;

        yield return null;
    }

    public override void Die()
    {
        //base.Die();
    }

    [System.Serializable]
    public struct Attack
    {
        public GameObject[] bulletObject;       // 총알 프리펩
        public float bulletSpeed;               // 총알 속도
        public float bulletCoolTime;            // 총알 발사 간격
        [HideInInspector]
        public bool isBulletCoolTime;           // 총알 발사 쿨타임

        public int nowAMMO;                     // 현재 총알 개수
        public int maxAMMO;                     // 최대 총알 개수
        public float reloadTime;                // 재장전 시간
        public float reloadBar;                 // 재장전바를 위한 변수
    }
}
