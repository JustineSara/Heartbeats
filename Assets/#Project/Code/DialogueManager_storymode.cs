using UnityEngine;
using System.Collections;
using VIDE_Data; //Access VD class to retrieve node data
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DialogueManager_storymode : MonoBehaviour
{

    public GameObject container_NPC;
    public GameObject container_PLAYER;
    public TMP_Text text_NPC;
    public TMP_Text[] text_Choices;

    // Use this for initialization
    void OnEnable()
    {
        if (!VD.isActive)
        {
            Begin();
        }
    }

    void Begin()
    {
        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += End;
        VD.BeginDialogue(GetComponent<VIDE_Assign>());
    }

    public void SetPlayerChoice(int choice)
    {
        VD.nodeData.commentIndex = choice;
        VD.Next();
    }

    void UpdateUI(VD.NodeData data)
    {
        if (data.isPlayer)
        {
            for (int i = 0; i < text_Choices.Length; i++)
            {
                if (i < data.comments.Length)
                {
                    text_Choices[i].transform.parent.gameObject.SetActive(true);
                    text_Choices[i].text = data.comments[i];
                }
                else
                {
                    text_Choices[i].transform.parent.gameObject.SetActive(false);
                }
            }
            text_Choices[0].transform.parent.GetComponent<Button>().Select();
        }
        else
        {
            text_NPC.text = data.comments[data.commentIndex];
			// I know 'foreach' are _bad_, to switch to 'for' probably use .ElementAt(i) from LINQ
			foreach (var item in data.extraVars)
			{
				if (Variables.ActiveScene.IsDefined(item.Key))
				{
					Variables.ActiveScene.Set(item.Key, item.Value);
				} else {
					Debug.Log($"{item.Key} is not a variable of the Active Scene! We do nothing with it.");
				}
			}
            VD.Next();
        }
    }

    void End(VD.NodeData data)
    {
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        VD.EndDialogue();
    }

    void OnDisable()
    {
        if (container_NPC != null)
            End(null);
    }
}