
namespace System.ComponentModel.DataAnnotations;

/// <summary>
/// This class validates a property that should contain a relative file/folder path.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class RelativePathOnlyAttribute : ValidationAttribute
{
    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="RelativePathOnlyAttribute"/>
    /// class.
    /// </summary>
    public RelativePathOnlyAttribute()
        : base("'{0}' must contain a relative path.")
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
            result = !Path.IsPathRooted(str);
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
