using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour {

    public float speed;
    CharacterController cc;
    Animator anim;
    bool isAlive = true;
    public float turnSpeed;
    public Rigidbody shell;
    public Transform muzzle;
    public float force = 10;
    public AudioSource audios;
    bool attacking;
    public float attackTime;
    public float hp = 100;

    public Slider hpSlider;
    public Image hpFullImage;
    public Color hpColorFull = Color.green;
    public Color hpColorNull = Color.red;
    public float max = 100;
    public ParticleSystem exEffect;
    // Use this for initialization
    void Start() {
        cc = GetComponent<CharacterController>();
        anim =GetComponentInChildren<Animator>();
        hp = max;
        RefreshHUD();
    }

    // Update is called once per frame
    void Update() {

    }
    public void Move(Vector3 v)
    {
        if (attacking) return;
        if (!isAlive)
            return;
        Vector3 movement = v * speed;
        cc.SimpleMove(movement);
        if (anim)
        {
            anim.SetFloat("Speed", cc.velocity.magnitude);
        }
    }
    public void Attack()
    {
        if (!isAlive)
            return;
        if (attacking) return;
        var instance = Instantiate(shell, muzzle.position,muzzle.rotation) as Rigidbody;
        instance.velocity = force * muzzle.forward;
        if (anim)
        {
            anim.SetTrigger("Attack");
        }
        attacking = true;
        audios.Play();
        Invoke("Refresh", attackTime);
    }
    public void Rotate(Vector3 lookDir)
    {
        var target = transform.position + lookDir;
        var characterPos = transform.position;
        target.y = 0;
        characterPos.y = 0;
        var faceToTargetDir = target - characterPos;
        var dir = Quaternion.LookRotation(faceToTargetDir);

        Quaternion slerp = Quaternion.Slerp(transform.rotation, dir, turnSpeed * Time.deltaTime);
        transform.rotation = slerp;
    }
    public void Death()
    {
        isAlive = false;
        exEffect.transform.parent = null;
        exEffect.gameObject.SetActive(true);
        ParticleSystem.MainModule mainMoudule = exEffect.main;
        Destroy(exEffect.gameObject, mainMoudule.duration);
        gameObject.SetActive(false);
    }
    void Refresh()
    {
        attacking = false;
    }
   public  void TakeDamage(float amount)
    {
        hp -= amount;
        if (hp <= 0 && isAlive)
        {
            Death();
        }
        RefreshHUD();
    }
    public void RefreshHUD()
    {
        hpSlider.value = hp;
        hpFullImage.color = Color.Lerp(hpColorNull, hpColorFull, hp / max);

    }
}
