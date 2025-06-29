using System.Collections;
using UnityEngine;

/// <summary>
/// 공통 몬스터 기능: 피격 / 공격 / 애니메이션 / 쿨타임
/// </summary>
public abstract class Monster : MonsterBase
{
    [Header("Status")]
    public int currentHp = 1;
    public float atkCoolTime = 3f;

    [Header("State Flags")]
    public bool isHit = false;
    public bool canAtk = true;

    [Header("References")]
    public GameObject hitCircleCollider;
    public Transform atkPoint;
    public float atkRange = 1f;
    public int atkDamage = 1;
    public LayerMask playerLayer;
    public Animator anim;

    private float atkCoolTimeCalc;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        atkCoolTimeCalc = atkCoolTime;

        StartCoroutine(CalcCoolTime());
        StartCoroutine(ResetHit());
    }

    protected override void Update()
    {
        if (currentHp <= 0) Destroy(gameObject); // 죽는거 수정해야함.
        base.Update();

        // 공격 대기 중에는 쿨타임 감소
        if (!canAtk)
        {
            atkCoolTimeCalc -= Time.deltaTime;

            if (atkCoolTimeCalc <= 0f)
            {
                atkCoolTimeCalc = atkCoolTime;
                canAtk = true;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("몬스터 피격");
        currentHp -= damage;
        isHit = true;
        hitCircleCollider.SetActive(false);

        if (anim != null)
            anim.SetBool("isHurt", true);

        StartCoroutine(ResetHurtFlag());
    }

    private IEnumerator ResetHurtFlag()
    {
        yield return new WaitForSeconds(1f);
        if (anim != null)
            anim.SetBool("isHurt", false);
    }

    private IEnumerator ResetHit()
    {
        while (true)
        {
            yield return null;
            if (!hitCircleCollider.activeInHierarchy)
            {
                yield return new WaitForSeconds(0.5f);
                hitCircleCollider.SetActive(true);
                isHit = false;
            }
        }
    }

    private IEnumerator CalcCoolTime()
    {
        while (true)
        {
            yield return null;
            if (!canAtk)
            {
                atkCoolTimeCalc -= Time.deltaTime;
                if (atkCoolTimeCalc <= 0f)
                {
                    atkCoolTimeCalc = atkCoolTime;
                    canAtk = true;
                }
            }
        }
    }

    protected override void OnPlayerDetected()
    {
        TryAttack();
    }

    protected virtual void TryAttack()
    {
        if (!canAtk) return;

        Collider2D player = Physics2D.OverlapCircle(atkPoint.position, atkRange, playerLayer);
        if (player != null)
        {
            canAtk = false;

            if (anim != null)
                anim.SetTrigger("Attack");

            ExecuteAttack(player);
        }
    }

    /// <summary>
    /// 공격 실제 실행 (데미지 적용 등)
    /// </summary>
    protected abstract void ExecuteAttack(Collider2D target);

    protected virtual void OnDrawGizmosSelected()
    {
        if (atkPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(atkPoint.position, atkRange);
        }
    }
}
