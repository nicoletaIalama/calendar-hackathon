namespace Enable.Design.Workflow;

public class WorkflowStepUser
{
    public WorkflowStepUser(string name, WorkflowStepUserApprovalState approvalState)
    {
        Name = name;
        ApprovalState = approvalState;
    }
    
    public string Name { get; set; }
    public WorkflowStepUserApprovalState ApprovalState { get; set; }
}
