
namespace System.ComponentModel.DataAnnotations;

/// <summary>
/// This class is a validation attribute that indicates when a property is 
/// required by checking the state of an associated boolean property.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class RequiredWhenAttribute : ValidationAttribute
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property indicates when the comparison should use inverse 
    /// logic (false for required, instead of true).
    /// </summary>
    public bool Invert { get; set; }

    /// <summary>
    /// This property contains the display name of the property to compare 
    /// to.
    /// </summary>
    public string? OtherPropertyDisplayName { get; internal set; }

    /// <summary>
    /// This property indicates when an empty string value should be allowed.
    /// </summary>
    public bool AllowEmptyStrings { get; set; }

    /// <summary>
    /// This property contains the name of the property that is decorated
    /// with this attribute.
    /// </summary>
    internal protected string? PropertyName { get; set; } 

    /// <summary>
    /// This property contains the default error message string.
    /// </summary>
    internal protected string DefaultErrorMessage { get; }

    /// <summary>
    /// This property contains the name of the property to compare to.
    /// </summary>
    internal protected string OtherProperty { get; } = null!;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="RequiredWhenAttribute"/>
    /// class.
    /// </summary>
    /// <param name="otherProperty">The property to compare with the current 
    /// property.</param>
    public RequiredWhenAttribute(
        string otherProperty
        )
    {
        OtherProperty = otherProperty;

        if (Invert)
        {
            DefaultErrorMessage = "{0} is required when {1} is false!";
        }
        else
        {
            DefaultErrorMessage = "{0} is required when {1} is true!";
        }
    }

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method performs the validation.
    /// </summary>
    /// <param name="value">The object to use for the operation.</param>
    /// <param name="validationContext">The validation context to use for 
    /// the operation.</param>
    /// <returns>The results of the validation.</returns>
    protected override ValidationResult IsValid(
        object? value, 
        ValidationContext validationContext
        )
    {
        PropertyName = validationContext.DisplayName ?? 
            validationContext.MemberName;

        var otherPropertyInfo = validationContext.ObjectType.GetRuntimeProperty(
            OtherProperty
            );

        if (otherPropertyInfo is null)
        {
            return new ValidationResult($"Property '{OtherProperty}' was not found!");
        }
        
        if (otherPropertyInfo.PropertyType != typeof(bool))
        {
            return new ValidationResult($"Property '{OtherProperty}' must be a boolean type!");
        }

        var otherPropertyGet = otherPropertyInfo.GetGetMethod();

        if (otherPropertyGet is null)
        {
            return new ValidationResult($"Property '{OtherProperty}' must have a public get!");
        }
        
        var otherPropertyValue = otherPropertyGet.Invoke(
                validationContext.ObjectInstance,
                []
                );

        var hasError = false;
        if (bool.Equals(otherPropertyValue, Invert))
        {
            hasError = !AllowEmptyStrings &&
                (value is string stringValue) &&
                string.IsNullOrWhiteSpace(stringValue);
        }            

        if (hasError is false)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return ValidationResult.Success;
#pragma warning restore CS8603 // Possible null reference return.
        }

        var msg = FormatErrorMessage(
            ErrorMessage ?? DefaultErrorMessage
            );

        return new ValidationResult(msg);
    }

    // *******************************************************************

    /// <summary>
    /// This method returns a formatted error message.
    /// </summary>
    /// <param name="name">The name to include in the formatted message.</param>
    /// <returns>The formatted message.</returns>
    public override string FormatErrorMessage(string name)
    {
        var formattedMsg = string.Format(
            CultureInfo.CurrentCulture,
            name ?? DefaultErrorMessage,
            PropertyName,
            OtherPropertyDisplayName ?? OtherProperty
            );

        return formattedMsg;
    }

    #endregion
}
