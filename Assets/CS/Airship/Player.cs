using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Airship
{
    public float oil;           // ����
    public float oilMinus;      // ���� ���� �ӵ�

    public Vector2 moveRestri;  // �̵�����
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

            BordarCheck(nextPos);   // �̵��� �������
        }   // �̵� �Է�

        if(Input.GetKeyDown(KeyCode.R) && attack.isBulletCoolTime)
        {
            if (attack.nowAMMO >= attack.maxAMMO) return;   // ź�� �ִ��� �� ���� �Ұ�

            attack.nowAMMO = 0;
            StartCoroutine(Fire());
        }   // ������

        ShotBullet();   // �Ѿ� �߻�
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
            attack.nowAMMO--;   // źâ���� 1�� ����

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
            // ������
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
        public GameObject[] bulletObject;       // �Ѿ� ������
        public float bulletSpeed;               // �Ѿ� �ӵ�
        public float bulletCoolTime;            // �Ѿ� �߻� ����
        [HideInInspector]
        public bool isBulletCoolTime;           // �Ѿ� �߻� ��Ÿ��

        public int nowAMMO;                     // ���� �Ѿ� ����
        public int maxAMMO;                     // �ִ� �Ѿ� ����
        public float reloadTime;                // ������ �ð�
        public float reloadBar;                 // �������ٸ� ���� ����
    }
}
