using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptScript_IXH : MonoBehaviour
{
    /*    public TMP_Text textBox;
        public AudioClip typingClip;
        public AudioSourceGroup audioSourceGroup;

        public Button X;
        public Button Next;

        private DialogueVertexAnimator dialogueVertexAnimator;
        void Awake()
        {
            dialogueVertexAnimator = new DialogueVertexAnimator(textBox, audioSourceGroup);
            PlayDialogue(dialogue1);
        }*/
    private Queue<string> sentences;
    void Start ()
    {
        sentences = new Queue<string>();
    }

}
