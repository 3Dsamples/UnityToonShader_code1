using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAnimator : MonoBehaviour
{
	Animator anim;
	// Use this for initialization
	void Start () 
	{
		anim.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	//ForGameObject
	public void AnimatorIntParameter(string Name,int Value)
	{
		anim.SetInteger (name,Value);
	}
	public void AnimatorFloatParameter(string Name,float Value)
	{
		anim.SetFloat (name, Value);
	}
	public void AnimatorBoolParameter(string Name,bool Value)
	{
		anim.SetBool (name, Value);
	}
	public void AnimatorSetTriggerParameter(string Name)
	{
		anim.SetTrigger (name);
	}
	public void AnimatorResetTriggerParameter(string Name)
	{
		anim.SetTrigger (name);
	}

	//ForButtons
	public void AnimatorIntParameter_btn(string name)
	{
		anim.SetInteger (name,1);
	}
	public void AnimatorFloatParameter_btn(string name)
	{
		anim.SetFloat (name, 1);
	}
	public void Animator_True_Parameter_btn(string name)
	{
		anim.SetBool (name, true);
	}
	public void Animator_False_Parameter_btn(string name)
	{
		anim.SetBool (name, false);
	}
	public void AnimatorSetTriggerParameter_btn(string name)
	{
		anim.SetTrigger (name);
	}
	public void AnimatorResetTriggerParameter_btn(string name)
	{
		anim.SetTrigger (name);
	}
}
