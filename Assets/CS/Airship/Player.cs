using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Airship
{
    [HideInInspector]
    public float setHP;             // HP �ִ�ġ ����
    [HideInInspector]
    public float setOil;            // ���� �ִ�ġ ����

    public float _oil
    {
        get
        {
            return oil;
        }

        set
        {
            if (oil <= 0) { Die();  return; }

            if (oil + value < setOil)
                oil += value;
        }
    }

    [Header("���� ����")]
    public float oil;               // ����
    float oilMinus = 1f;            // ���� ���� �ӵ�
    bool isRefuel = false;          // ���߱���

    [Header("���� ����")]
    public Vector2 moveRestri;      // �̵�����
                                    // 3.8 , 6
    [Header("���� ����")]
    public Attack attack;

    // ��ȭ�� ���õ� ����, ������Ƽ��
    public int _attackReinforce
    {
        get
        {
            return attackReinforce;
        }

        set 
        {
            // ���ݰ�ȭ�� 4�̶��ϸ� ��ȭ
            if(attackReinforce < 4)
            {
                attackReinforce += value;
                GameManager.GM.palyerDamage += 30;
                attack.maxAMMO += 20;
            }
                
        }
    }
    [HideInInspector]
    public int attackReinforce;


    public override void Awake()
    {
        GameManager.GM.ResetGameData();

        base.Awake();

        HP = setHP; oil = setOil;

        attack.isBulletCoolTime = true;
        attack.nowAMMO = attack.maxAMMO;

        // ��ȭ���� �ʱ�ȭ
        attackReinforce = 1;
    }

    public override void Start()
    {
        
    }

    public override void Update()
    {
        // ���ӿ����� ��ü �ߴ�
        if (GameManager.GM.isGameOver) return;

        if (isRefuel) _oil = oilMinus * 10 * Time.deltaTime;
        else _oil = -oilMinus * Time.deltaTime;

       GameManager.GM.runTime += Time.deltaTime;

        PlayerControl();

        GameOverCheck();

        if (Input.GetKeyDown(KeyCode.J)) _attackReinforce = 1;
    }

    void PlayerControl()
    {
        {
            Vector2 nextPos = new Vector2(0, 0);
            nextPos.x = Input.GetAxisRaw("Horizontal");
            nextPos.y = Input.GetAxisRaw("Vertical");

            BordarCheck(nextPos);   // �̵��� �������
        }   // �̵� �Է�

        if(Input.GetKey(KeyCode.R) && attack.isBulletCoolTime)
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
        anime.SetInteger("Move", (int)nextPos.x);
    }

    void GameOverCheck()
    {
        if (HP <= 0)  GameManager.GM.GameOver("Player Destroy\nNo HP");
        if (oil <= 0) GameManager.GM.GameOver("Player Destroy\nNo Oil");
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
            switch (attackReinforce)
            {
                case 1: // ��

                    Fire_Mid();
                    break;

                case 2: // ��

                    Fire_Side();
                    break;

                case 3: // ��, �밢

                    Fire_Side();
                    Fire_Diag();
                    break;

                case 4: // ��, ��, �밢

                    Fire_Mid();
                    Fire_Side();
                    Fire_Diag();

                    break;

                default:
                    break;
            }

            attack.nowAMMO--;   // źâ���� 1�� ����

            yield return new WaitForSeconds(attack.bulletCoolTime);
        }

        if (attack.nowAMMO <= 0)
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

    void Fire_Mid()
    {
        GameObject bullet;

        bullet = Instantiate(attack.bulletObject[0], transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * attack.bulletSpeed, ForceMode2D.Impulse);
    }   // ��� 1��

    void Fire_Side()
    {
        GameObject bullet;

        bullet = Instantiate(attack.bulletObject[0], transform.position + new Vector3(-0.2f, 0f, 0f), transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * attack.bulletSpeed, ForceMode2D.Impulse);
        bullet = Instantiate(attack.bulletObject[0], transform.position + new Vector3(0.2f, 0f, 0f), transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * attack.bulletSpeed, ForceMode2D.Impulse);
    }   // ��� �� 2��

    void Fire_Diag()
    {
        GameObject bullet;

        bullet = Instantiate(attack.bulletObject[0], transform.position + new Vector3(-0.2f, 0f, 0f), transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.15f, 1) * attack.bulletSpeed, ForceMode2D.Impulse);
        bullet = Instantiate(attack.bulletObject[0], transform.position + new Vector3(0.2f, 0f, 0f), transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.15f, 1) * attack.bulletSpeed, ForceMode2D.Impulse);
    }   // �밢�� 2��

    public override void Die()
    {
        //base.Die();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Refuel"))
            isRefuel = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Refuel"))
            isRefuel = false;
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
