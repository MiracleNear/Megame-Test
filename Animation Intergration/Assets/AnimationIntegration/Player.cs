using System;
using System.Collections.Generic;
using AnimationIntegration;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public Transform targetBone;
    public Animator Animator;
    public Text Text;
    public LayerMask EnemyLayer;
    public float Radius;
    public GameObject Sword;
    public GameObject Rifle;

    [HideInInspector] public Enemy Enemy;
    
    private bool _isFinishingAnimationPlay;
    
    private Vector3 _input;
    
    private void Update()
    {
        HandleInput();
        
        if (Physics.CheckSphere(transform.position, Radius, EnemyLayer) && !_isFinishingAnimationPlay && Enemy != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 positionToKill = Enemy.KillingPoint.position;
                Quaternion rotation = Quaternion.LookRotation(Enemy.KillingPoint.forward);
                
                transform.position = positionToKill;
                transform.rotation = rotation;

                _input = Vector3.zero;
                Sword.SetActive(true);
                Rifle.SetActive(false);
                Animator.SetTrigger("Kill");
                _isFinishingAnimationPlay = true;
            }
            
            Text.enabled = true;
        }
        else
        {
            Text.enabled = false;
        }

        
    }

    private void HandleInput()
    {
        if (!_isFinishingAnimationPlay)
        {
            _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        
            Animator.SetFloat("inputX", _input.x);
            Animator.SetFloat("inputY", _input.z);
        
            if (Mathf.Abs(_input.x) > 0 || Mathf.Abs(_input.z) > 0)
            {
                Animator.SetBool("run", true);
            }
            else
            {
                Animator.SetBool("run", false);
            }
            
        }
    }
    
    private void FixedUpdate()
    {
        if (!_isFinishingAnimationPlay)
        {
            Vector3 motion = (transform.forward * _input.z + transform.right * _input.x) * 5f;

            Rigidbody.MovePosition(Rigidbody.position + motion * Time.deltaTime);
        }
    }
    
    private void LateUpdate()
    {
        if (!_isFinishingAnimationPlay)
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float enter))
            {
                Vector3 point = ray.GetPoint(enter);

                Vector3 directionLook = point - transform.position;

                float angle = Vector3.SignedAngle(directionLook, transform.forward, Vector3.up);

                targetBone.rotation = Quaternion.AngleAxis(angle, targetBone.right) * targetBone.rotation;
            }
        }

    }

    private void KillEnemy()
    {
        Enemy.Kill();
        
        Enemy = null;
    }
    
    private void OnEndedAnimationFinishing()
    {
        _isFinishingAnimationPlay = false;
        Sword.SetActive(false);
        Rifle.SetActive(true);
        
        transform.rotation = Quaternion.LookRotation(Vector3.forward);
    }
}
