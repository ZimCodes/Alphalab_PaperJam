﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTransition : MonoBehaviour {
    [SerializeField]
    private CanvasRenderer StartPanel, InstructionPanel, GameplayPanel, GameoverPanel;
    private float startfadeOutInSeconds = 1.4f, instructionfadeOutInSeconds = 1.4f, gameplayfadeOutInSeconds = 1.4f, gameoverfadeOutInSeconds = 1.4f;
    StartPanel start;
    InstructionPanel instruction;
    GamePanel gameplay;
    GameoverPanel gameover;
	// Use this for initialization
	void Start () {
        
        start = new StartPanel(StartPanel);
        instruction = new InstructionPanel(InstructionPanel);
        gameplay = new GamePanel(GameplayPanel);
        gameover = new GameoverPanel(GameoverPanel);

        start.animator= StartPanel.GetComponent<Animator>();
        instruction.animator = InstructionPanel.GetComponent<Animator>();
        gameplay.animator = GameplayPanel.GetComponent<Animator>();
        gameover.animator = GameoverPanel.GetComponent<Animator>();

        start.Start();
        instruction.Start();
        gameplay.Start();
        gameover.Start();
       
	}
    private void Update()
    {
        switch (Panel.CurrentScreen)
        {
            case PanelScreen.Start:
                start.Update();
                if (Panel.CurrentScreen == PanelScreen.Instruction)
                {
                    StartCoroutine(Example());
                }
                break;
            case PanelScreen.Instruction:
                instruction.Update();
                if (Panel.CurrentScreen == PanelScreen.Gameplay)
                {
                    StartCoroutine(Example());
                }
                break;
            case PanelScreen.Gameplay:
                gameplay.Update();
                break;
            case PanelScreen.Gameover:
                gameover.Update();
                break;
        }
        Debug.Log(Panel.CurrentScreen);
    }
    IEnumerator Example()
    {
        switch (Panel.CurrentScreen)
        {
            case PanelScreen.Instruction:
                yield return new WaitForSeconds(startfadeOutInSeconds);
                start.panel.gameObject.SetActive(false);
                instruction.panel.gameObject.SetActive(true);
                instruction.animator.Play("FadeIn");
                break;
            case PanelScreen.Gameplay:
                yield return new WaitForSeconds(instructionfadeOutInSeconds);
                instruction.panel.gameObject.SetActive(false);
                gameplay.panel.gameObject.SetActive(true);
                gameplay.animator.Play("FadeIn");
                break;
            case PanelScreen.Gameover:
                yield return new WaitForSeconds(gameplayfadeOutInSeconds);
                gameplay.panel.gameObject.SetActive(false);
                gameover.panel.gameObject.SetActive(true);
                gameover.animator.Play("FadeIn");
                break;
            //case PanelScreen.Start:
            //    yield return new WaitForSeconds(gameoverfadeOutInSeconds);
            //    gameover.panel.gameObject.SetActive(false);
            //    break;

        }
        
    }
}
