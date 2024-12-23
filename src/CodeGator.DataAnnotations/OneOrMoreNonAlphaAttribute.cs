
namespace System.ComponentModel.DataAnnotations;

/// <summary>
/// This class validates a property that should contain one or more non
/// alphanumeric character.
/// </summary>

[AttributeUsage(AttributeTargets.Property)]
public class OneOrMoreNonAlphaAttribute : ValidationAttribute
{
    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="OneOrMoreNonAlphaAttribute"/>
    /// class.
    /// </summary>
    public OneOrMoreNonAlphaAttribute()
        : base("'{0}' must have at least one non alphanumeric character.")
    {

    }

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method determines whether the specified value of the object is 
    /// valid, or not.
    /// </summary>
    /// <param name="value">The value of the object to validate.</param>
    /// <returns>true if the specified value is valid; false otherwise.</returns>
    public override bool IsValid(
        object? value
        )
    {
        if (value is null)
        {
            return false;
        }

#pragma warning disable SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.
        if (!Regex.IsMatch($"{value}", "[^a-zA-Z0-9]+$"))
        {
            return false;
        }
#pragma warning restore SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.

        return true;
    }

    // *******************************************************************

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
