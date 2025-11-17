using UnityEngine;

public class MagicMode : MonoBehaviour
{
    private TimeBarManager timeBarManager;
    public bool inMagicMode;
    private void Start()
    {
        timeBarManager = GameObject.FindGameObjectWithTag("TimeBarManager").GetComponent<TimeBarManager>();
    }
    private void Update()
    {
        if (!inMagicMode)
        {
            return;
        }
    }
    public void enterMagicMode()
    {
        inMagicMode = true;
        timeBarManager.SwitchDataset(2);
    }
    public void exitMagicMode()
    {
        inMagicMode = false;
        timeBarManager.SwitchDataset(1);
    }
    public void Magic1()
    {
        
    }
    public void Magic2()
    {
        
    }
    public void Magic3()
    {
        
    }
}
