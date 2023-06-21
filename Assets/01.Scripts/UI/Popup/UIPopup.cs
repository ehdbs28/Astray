using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIPopup : UIScreen
{
    private bool _isOpenPopup = false;
    public bool IsOpenPopup => _isOpenPopup;

    public void SetUp(UIDocument document, bool clearScreen = true, bool timeStop = true){
        _isOpenPopup = true;

        _documentRoot = document.rootVisualElement.Q("main-container");

        if(clearScreen){
            _documentRoot.Clear();
        }

        if(timeStop)
            TimeManager.Instance.TimeScale = 0f;

        VisualElement generatedRoot = GenerateRoot();

        if(generatedRoot != null){
            AddEvent(generatedRoot);
            _documentRoot.Add(generatedRoot);
        }
    }

    protected override VisualElement GenerateRoot()
    {
        _root = _treeAsset.Instantiate();
        _root = _root.ElementAt(0);
        
        FindElement(_root);

        return _root;
    }

    public virtual void RemoveRoot(){
        if(_documentRoot == null || _root == null){
            return;
        }

        RemoveEvent();

        _documentRoot.Remove(_root);

        _documentRoot = null;
        _root = null;

        TimeManager.Instance.TimeScale = 1f;

        _isOpenPopup = false;
    }
}
