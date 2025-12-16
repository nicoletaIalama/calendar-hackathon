namespace Enable.Design.Workflow;

public class WorkflowStep
{
    public WorkflowStep(string label, WorkflowStepState state, IEnumerable<WorkflowStepUser> stepUsers)
    {
        Label = label;
        WorkflowStepState = state;
        WorkflowStepUsers = stepUsers;
    }
    
    public string Label { get; set; }
    public WorkflowStepState WorkflowStepState { get; set; }
    public IEnumerable<WorkflowStepUser> WorkflowStepUsers { get; set; }
}
