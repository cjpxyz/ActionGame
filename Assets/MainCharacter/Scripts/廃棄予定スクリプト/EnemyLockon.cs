/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class EnemyLockon : MonoBehaviour
{

    //ロックオンできる範囲
    public float range;
    public Transform emptyTarget;

    public CinemachineTargetGroup group;
    //範囲内のロックオンできる敵の数
    public int enemyCount;
    public List<Transform> enemiesToLock;
    public Transform closestEnemy;
    public Transform selectedEnemy;
    public Transform priorityEnemy;
    public bool foundPriorityEnemy;

    public float xScaleIncrement;
    public float yScaleIncrement;
    public float zScaleIncrement;
    public float xScalemax;
    public float zScaleMax;
    public GameObject targetingCone;
    public GameObject targetingConePivot;
    public Transform coneHolder;
    private Vector3 selecterDirection;
    private bool parentChangeInitializationPerformed;

    //public bool canShoot;
    private bool temp;

    private TargetLockCamera cam;
    private CharacterMovement characterMovement;
    private TargettingConeTrigger trigger;

    private void Awake()
    {
        cam = GetComponent<TargetLockCamera>();
        characterMovement = GetComponent<CharacterMovement>();
        trigger = targetingCone.GetComponent<TargetingConeTrigger>();
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (InputManager.CameraButton()) temp = !temp;
        if (temp)
        {
            RunEnemySearchSphereCollider();
        }
        else
        {
            enemiesToLock.Clear();
            cam.targetLockCam = false;
        }

        enemyCount = enemiesToLock.Count;

        if(enemyCount == 0 || priorityEnemy == null)
        {
            InitializeTargetGroup();
            cam.targetLockCam = false;
            foundPriorityEnemy = false;
            closestEnemy = null;
            selectedEnemy = null;
            priorityEnemy = null;
            InitializeConeParent();
            ResetTargetingCone();
            //canShoot = false;
        }

        if(enemyCount != 0)
        {
            cam.targetLockCam = true;
            FindClosestEnemy();
            if (closestEnemy != null && foundPriorityEnemy == false) SetPriorityEnemy(closestEnemy);
            SwitchTarget();
            if (selectedEnemy != null) SetPriorityEnemy(selectedEnemy);
            if (priorityEnemy != null) BuildTargetGroup();
            //canShhot = true;
        }
        
    }

    private void RunEnemySearchSphereCollider()
    {
        Collider[] enemyDetect = Physics.OverlapSphere(transform.position, range);
        enemiesToLock = new List<Transform>();
        foreach (Collider col in enemyDetect) 
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null) enemiesToLock.Add(col.transform);
        }
    }

    private void FindClosestEnemy()
    {
        float closest = range;
        closestEnemy = null;
        for (int i = 0; 1 < enemyCount; i++)
        {
            float distanceToPlayer = Vector3.Distance(enemiesToLock[i].position, transform.position);
            if(distanceToPlayer < closest)
            {
                closest = distanceToPlayer;
                closestEnemy = enemiesToLock[i];
            }
        }
    }

    private void SetPriorityEnemy(Transform pEnemy)
    {
        priorityEnemy = pEnemy;
        foundPriorityEnemy = true;
        SetConeParentToTarget();
    }

    private void BuildTargetGroup()
    {
        CinemachineTargetGroup.Target enemy;
        enemy.target = priorityEnemy;
        enemy.weight = priorityEnemy.GetComponent<Enemy>().camWeight;
        enemy.radius = priorityEnemy.GetComponent<Enemy>().camRadius;

        group.m_Targets[1].target = enemy.target;
        group.m_Targets[1].weight = enemy.weight;
        group.m_Targets[1].radius = enemy.radius;
    }

    private void InitializeTargetGroup()
    {
        CinemachineTargetGroup.Target defaultTarget;
        defaultTarget.target = emptyTarget;
        defaultTarget.weight = emptyTarget.GetComponent<Enemy>().camWeight;
        defaultTarget.radius = emptyTarget.GetComponent<Enemy>().camRadius;

        group.m_Targets[1].target = defaultTarget.target;
        group.m_Targets[1].weight = defaultTarget.weight;
        group.m_Targets[1].radius = defaultTarget.radius;
    }

    private void SwitchTarget()
    {
        if (InputManager.SubVertical() == 0 && InputManager.SubHorizontal() == 0) ResetTargetingCone();
        else
        {
            selecterDirection = ((characterMovement.camForward * -InputManager.SubVertical()) + (characterMovement.camRight * InputManager.SubHorizontal())).normalized;
            targetingConePivot.transform.rotation = Quaternion.LookRotation(selecterDirection);
            targetingCone.SetActive(true);

            if(trigger.selectedEnemy != null && trigger.selectedEnemy != priorityEnemy)
            {
                parentChangeInitializationPerformed = false;
                selectedEnemy = trigger.selectedEnemy.transform;
            }
            else
            {
                if (targetingCone.transform.localScale.y <= range) targetingCone.transform.localScale += new Vector3(0, yScaleIncrement, 0);
                if (targetingCone.transform.localScale.x <= xScalemax) targetingCone.transform.localScale += new Vector3(xScaleIncrement, 0, 0);
                if (targetingCone.transform.localScale.z <= zScaleMax) targetingCone.transform.localScale += new Vector3(0, 0, zScaleIncrement);
            }
        }
    }
    private void SetConeParentToTarget()
    {
        targetingConePivot.transform.SetParent(priorityEnemy);
        if (!parentChangeInitializationPerformed)
        {
            targetingConePivot.transform.localPosition = Vector3.zero;
            targetingCone.transform.localScale = new Vector3(trigger.coneScaleX, trigger.coneScaleY, trigger.coneScaleZ);
            parentChangeInitializationPerformed = true;
        }
    }

    private void InitializeConeParent()
    {
        targetingConePivot.transform.SetParent(coneHolder);
        targetingConePivot.transform.localPosition = Vector3.zero;
        targetingConePivot.transform.localPosition = Quaternion.identity;
        parentChangeInitializationPerformed = false;
    }

    private void ResetTargetingCone()
    {
        trigger.selectedEnemy = null;
        targetingCone.SetActive(false);
        targetingConePivot.transform.position = transform.rotation;
        targetingCone.transform.localScale = new Vector3(trigger.coneScaleX, trigger.coneScaleY, trigger.coneScaleZ);
    }


}
*/