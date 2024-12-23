
namespace System.ComponentModel.DataAnnotations;

/// <summary>
/// This class validates a property that should contain a date that is at 
/// least 18 years ago.
/// </summary>

[AttributeUsage(AttributeTargets.Property)]
public class Over18RequiredAttribute : ValidationAttribute
{
    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="Over18RequiredAttribute"/>
    /// class.
    /// </summary>
    public Over18RequiredAttribute()
        : base("'{0}' date must be at least 18 years ago.")
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

        if (!DateTime.TryParse($"{value}", out var parsedDate))
        {
            return false;
        }

        var result = parsedDate <= DateTime.Now.AddYears(-18).Date;
        return result;
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
