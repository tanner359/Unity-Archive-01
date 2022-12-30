using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Character_Select : MonoBehaviour
{
    Coroutine_Manager manager;
    public Launcher launcher;
    public Sprite character_Preview;
    public Sprite prof_Sprite;
    //public Image preview_Box;
    public int char_Index;
    public Sprite default_Prof;
    public Animator animator;

    private void Start()
    {
        manager = transform.parent.GetComponent<Coroutine_Manager>();
        launcher.selected_Character = default_Prof;
    }

    public void Move_Selection()
    {
        if (manager.new_Pos != (Vector2)transform.localPosition)
        {
            manager.new_Pos = transform.localPosition;
            manager.StopCoroutine("Move");
            manager.StartCoroutine("Move");
        }
    }
    

    public void Update_Preview()
    {
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(prof_Sprite.name);
        //preview_Box.sprite = character_Preview;
        launcher.selected_Character = prof_Sprite;
        //preview_Box.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(character_Preview.rect.width, character_Preview.rect.height);
    }
}
