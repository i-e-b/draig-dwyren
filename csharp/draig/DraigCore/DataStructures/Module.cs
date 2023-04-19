using SkinnyJson;

namespace DraigCore.DataStructures;

/// <summary>
/// A 'Module' is the outer container of the system.
/// <p></p>
/// Modules have:
/// <ul><li>a set of <see cref="Process"/>es (optionally in a hierarchy)</li>
///     <li>a single root <see cref="Manager"/>, which starts and controls process instances</li>
///     <li>a single <see cref="MessageHub"/>, which accepts and distributes <see cref="Message"/>s</li>
///     <li>a single <see cref="Scheduler"/>, which manages the run-time of a module</li>
/// </ul>
/// </summary>
public class Module
{
    /// <summary>
    /// Restore a module from storage data
    /// </summary>
    public static Module FromSerial(string data)
    {
        return Json.Defrost<Module>(data);
    }


    /// <summary>
    /// Write the current module definition to a string, to be stored.
    /// The module can be recovered using <see cref="FromSerial"/>
    /// </summary>
    public string Serialise()
    {
        Json.DefaultParameters.EnableAnonymousTypes = true;
        return Json.Freeze(this);
    }
}