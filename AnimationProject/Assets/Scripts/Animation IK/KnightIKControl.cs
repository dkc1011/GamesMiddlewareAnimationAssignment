using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class KnightIKControl : MonoBehaviour
{
    protected Animator knightAnimator;
    private bool ikActive = false;
    Transform rightHandObject;
    public GameObject MaulPrefab;
    GameObject newMaul;

    // Start is called before the first frame update
    void Awake()
    {
        knightAnimator = GetComponent<Animator>();
        Transform backObject = GetSpine();

        CreateMaul(backObject);

    }

    // Update is called once per frame
    void OnAnimatorIK()
    {
        if(knightAnimator)
        {
            if(ikActive)
            {
                knightAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                knightAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                knightAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObject.position);
                knightAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObject.rotation);
                Instantiate(MaulPrefab, rightHandObject.position, Quaternion.identity);
            }
            else
            {
                knightAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                knightAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                knightAnimator.SetLookAtWeight(0);
            }
        }
    }

    public void setIKActive(bool active)
    {
        ikActive = active;
    }

    private Transform GetSpine()
    {
        Transform backBone;
        backBone = transform.Find("hips/spine");
        CheckSpine(backBone);

        return backBone;
    }

    private void CheckSpine(Transform spine)
    {
        if(spine)
        {
            Debug.Log("found bone 'SPINE'");
        }
        else
        {
            Debug.Log("ERR - could not find bone 'SPINE'");
        }
    }

    private void CreateMaul(Transform backObject)
    {
        GameObject newMaul = Instantiate(MaulPrefab, backObject.position, backObject.rotation);
        newMaul.transform.parent = backObject;
        newMaul.transform.position = new Vector3(backObject.position.x + 0.42f, backObject.position.y + 0.2f, backObject.position.z + 0.1f);
        newMaul.transform.eulerAngles = new Vector3(backObject.eulerAngles.x + 90, backObject.eulerAngles.y + 90, backObject.eulerAngles.z);
    }

}
