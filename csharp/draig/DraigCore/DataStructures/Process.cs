namespace DraigCore.DataStructures;

/// <summary>
/// A 'Process' is the part of the system that groups a flow of instructions.
/// This is somewhere between a 'class' or 'function' in other languages.
/// <p></p>
/// It has a start point, and an end point.
/// The start and end are connected by a sequence of <see cref="Step"/>s
/// (each of which might have its own sequences).
/// <p></p>
/// Processes are owned by a <see cref="Module"/>
/// </summary>
public class Process
{
    /// <summary>
    /// Input parameters to this process. May be an empty set if no parameters are required.
    /// </summary>
    public StateDescription Parameters { get; set; } = new();

    /// <summary>
    /// Variables and constants available to this process at run-time.
    /// </summary>
    public StateDescription RuntimeState { get; set; } = new();
    
    /// <summary>
    /// Top-level steps of this process, each of which may contain their own <see cref="Step"/>s
    /// </summary>
    public List<Step> Steps { get; set; } = new();
}