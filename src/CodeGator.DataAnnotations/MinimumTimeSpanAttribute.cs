
namespace System.ComponentModel.DataAnnotations;

/// <summary>
/// This class validates a property that should contains at least the 
/// stated lojng integer value.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class MinimumTimeSpanAttribute : ValidationAttribute
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the minimum acceptable value for the property.
    /// </summary>
    public TimeSpan MinimumValue { get; set; }

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="MinimumTimeSpanAttribute"/>
    /// class.
    /// </summary>
    public MinimumTimeSpanAttribute(TimeSpan minimumValue)
        : base("'{0}' must contain a value of at least '{1}'.")
    {
        MinimumValue = minimumValue;
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
        var result = false;
        
        if (value is null)
        {
            return result;
        }

        var convObj = (TimeSpan)Convert.ChangeType(value, typeof(TimeSpan));
        result = convObj >= MinimumValue;

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
        return String.Format(this.ErrorMessageString, name, MinimumValue);
    }

    #endregion
}
