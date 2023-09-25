public class Tutorial_Dialog : Tutorial_Base
{
    private Dialog_System dialog_System;
    public override void Enter()
    {
        dialog_System = GetComponent<Dialog_System>();
        dialog_System.Setup();
    }

    public override void Excute(Tutorial_Control manager)
    {
        bool isEnd = dialog_System.UpdateDialog();

        if (isEnd)
        {
            manager.SetNextTutorial();
        }
    }

    public override void Exit()
    {        
    }

}
