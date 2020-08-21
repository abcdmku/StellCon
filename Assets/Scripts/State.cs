﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State // Not used yet
{
    public Action ActiveAction;
    public Action OnEnterAction;
    public Action OnExitAction;

    public State(Action active, Action onEnter, Action onExit)
    {
        ActiveAction = active;
        OnEnterAction = onEnter;
        OnExitAction = onExit;
    }

    public void Execute()
    {
        if (ActiveAction != null)
            ActiveAction.Invoke();
    }

    public void OnEnter()
    {
        if (OnEnterAction != null)
            OnEnterAction.Invoke();
    }
    public void OnExit()
    {
        if (OnExitAction != null)
            OnExitAction.Invoke();
    }
}