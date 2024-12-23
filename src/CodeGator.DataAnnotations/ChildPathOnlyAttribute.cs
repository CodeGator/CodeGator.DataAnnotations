
namespace System.ComponentModel.DataAnnotations;

/// <summary>
/// This class validates a property that should contain a relative file/folder 
/// path that doesn't climb back up to a parent (or higher).
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ChildPathOnlyAttribute : ValidationAttribute
{
    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="ChildPathOnlyAttribute"/>
    /// class.
    /// </summary>
    public ChildPathOnlyAttribute()
        : base("'{0}' must point to a child folder.")
    {

    }

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method determines whether the specified value of the object is valid, or not.
    /// </summary>
    /// <param name="value">The value of the object to validate.</param>
    /// <returns>true if the specified value is valid; false otherwise.</returns>
    public override bool IsValid(
        object? value
        )
    {
        var result = false;

        if (value is string str)
        {
            result = !str.Contains("..");
        }

        return result;
    }

    /// <summary>
    /// This method applies formatting to an error message.
    /// </summary>
    /// <param name="name">The name to include in the formatted message.</param>
    /// <returns>An instance of the formatted error message.</returns>
    public override string FormatErrorMessage(string name)
    {
        return String.Format(this.ErrorMessageString, name);
    }

    #endregion
}
