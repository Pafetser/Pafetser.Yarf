namespace Pafetser.Yarf;

public static class MessageExtensions
{
    #region WithMessage extensions

    /// <summary>
    /// Adds all items in <paramref name="messages"/> to <see cref="Result.Messages"/> for <paramref name="result"/>, and updates the <see cref="Result.IsSuccess"/> and <see cref="Result.HasWarnings"/> flags if necessary
    /// </summary>
    /// <typeparam name="T">concrete type of <paramref name="result"/>, which means this extension method will work an all derivatives of <see cref="Result"/></typeparam>
    /// <param name="result"><see cref="Result"/> instance to which the messages must be added</param>
    /// <param name="messages">Collection of messages that must be added to <paramref name="result"/></param>
    /// <returns>the <paramref name="result"/> instance so that fluent method chaining can be used if the desired</returns>
    /// <exception cref="ArgumentNullException"><paramref name="result"/> is null</exception>
    /// <exception cref="ArgumentNullException"><paramref name="messages"/> is null</exception>
    /// <exception cref="InvalidOperationException">any item in <paramref name="messages"/> is null</exception>
    public static T WithMessages<T>(this T result, IEnumerable<Message> messages) where T : Result
    {
        if (result == null) throw new ArgumentNullException(nameof(result));
        if (messages == null) throw new ArgumentNullException(nameof(messages));
        foreach (var message in messages)
        {
            if (message is null) throw new InvalidOperationException("Only non null messages are allowed");
            result._messages.Add(message);
            if (message is Error) result.IsSuccess = false;
            if (message is Warning) result.HasWarnings = true;
        }
        return result;
    }

    /// <summary>
    /// Adds all items in <paramref name="messages"/> to <see cref="Result.Messages"/> for <paramref name="result"/>, and updates the <see cref="Result.IsSuccess"/> and <see cref="Result.HasWarnings"/> flags if necessary
    /// </summary>
    /// <typeparam name="T">concrete type of <paramref name="result"/>, which means this extension method will work an all derivatives of <see cref="Result"/></typeparam>
    /// <param name="result"><see cref="Result"/> instance to which the messages must be added</param>
    /// <param name="messages">Collection of messages that must be added to <paramref name="result"/></param>
    /// <returns>the <paramref name="result"/> instance so that fluent method chaining can be used if the desired</returns>
    /// <exception cref="ArgumentNullException"><paramref name="result"/> is null</exception>
    /// <exception cref="ArgumentNullException"><paramref name="messages"/> is null</exception>
    /// <exception cref="InvalidOperationException">any item in <paramref name="messages"/> is null</exception>
    public static T WithMessages<T>(this T result, params Message[] messages) where T : Result
    {
        return result.WithMessages((IEnumerable<Message>)messages);
    }

    /// <summary>
    /// Adds <paramref name="message"/> to <see cref="Result.Messages"/> for <paramref name="result"/>, and updates the <see cref="Result.IsSuccess"/> and <see cref="Result.HasWarnings"/> flags if necessary
    /// </summary>
    /// <typeparam name="T">concrete type of <paramref name="result"/>, which means this extension method will work an all derivatives of <see cref="Result"/></typeparam>
    /// <param name="result"><see cref="Result"/> instance to which the message must be added</param>
    /// <param name="message">Mmessage that must be added to <paramref name="result"/></param>
    /// <returns>the <paramref name="result"/> instance so that fluent method chaining can be used if the desired</returns>
    /// <exception cref="ArgumentNullException"><paramref name="result"/> is null</exception>
    /// <exception cref="ArgumentNullException"><paramref name="message"/> is null</exception>
    public static T WithMessage<T>(this T result, Message message) where T : Result
    {
        if (message is null) throw new ArgumentNullException(nameof(message));
        return result.WithMessages(message);
    }

    #endregion WithMessage extensions

    #region WithInformation extensions

    public static T WithInformations<T>(this T result, IEnumerable<string> informations) where T : Result
    {
        return result.WithInformations(informations.Select(i => (Information)i));
    }

    public static T WithInformations<T>(this T result, IEnumerable<Information> informations) where T : Result
    {
        return result.WithMessages(informations);
    }

    public static T WithInformations<T>(this T result, params Information[] informations) where T : Result
    {
        return result.WithMessages(informations);
    }

    public static T WithInformation<T>(this T result, Information information) where T : Result
    {
        return result.WithMessage(information);
    }

    #endregion WithInformation extensions

    #region WithWarning extensions

    public static T WithWarnings<T>(this T result, IEnumerable<string> warnings) where T : Result
    {
        return result.WithWarnings(warnings.Select(i => (Warning)i));
    }

    public static T WithWarnings<T>(this T result, IEnumerable<Warning> warnings) where T : Result
    {
        return result.WithMessages(warnings);
    }

    public static T WithWarnings<T>(this T result, params Warning[] warnings) where T : Result
    {
        return result.WithMessages(warnings);
    }

    public static T WithWarning<T>(this T result, Warning warning) where T : Result
    {
        return result.WithMessage(warning);
    }

    #endregion WithWarning extensions

    #region WithError extensions

    public static T WithErrors<T>(this T result, IEnumerable<string> errors) where T : Result
    {
        return result.WithErrors(errors.Select(i => (Error)i));
    }

    public static T WithErrors<T>(this T result, IEnumerable<Error> errors) where T : Result
    {
        return result.WithMessages(errors);
    }

    public static T WithErrors<T>(this T result, params Error[] errors) where T : Result
    {
        return result.WithMessages(errors);
    }

    public static T WithError<T>(this T result, Error error) where T : Result
    {
        return result.WithMessage(error);
    }

    #endregion WithError extensions

    #region WithValidationError extensions

    public static T WithValidationErrors<T>(this T result, IDictionary<string, string[]> validationErrors) where T : Result
    {
        if (validationErrors is null) throw new ArgumentNullException(nameof(validationErrors));
        return result.WithErrors(validationErrors.SelectMany(e => e.Value.Select(v => new ValidationError(e.Key, v))));
    }

    public static T WithValidationErrors<T>(this T result, IEnumerable<ValidationError> validationErrors) where T : Result
    {
        return result.WithErrors(validationErrors);
    }

    public static T WithValidationErrors<T>(this T result, params ValidationError[] validationErrors) where T : Result
    {
        return result.WithErrors(validationErrors);
    }

    public static T WithValidationError<T>(this T result, ValidationError validatonError) where T : Result
    {
        return result.WithError(validatonError);
    }

    public static T WithValidationError<T>(this T result, string source, string error) where T : Result
    {
        return result.WithMessage(new ValidationError(source, error));
    }

    #endregion WithValidationError extensions

    #region WithException extensions

    public static T WithExceptions<T>(this T result, IEnumerable<Exception> exceptions) where T : Result
    {
        return result.WithExceptions(exceptions.Select(e => (ExceptionalError)e));
    }

    public static T WithExceptions<T>(this T result, IEnumerable<ExceptionalError> exceptions) where T : Result
    {
        return result.WithErrors(exceptions);
    }

    public static T WithExceptions<T>(this T result, params ExceptionalError[] exceptions) where T : Result
    {
        return result.WithErrors(exceptions);
    }

    public static T WithException<T>(this T result, ExceptionalError exception) where T : Result
    {
        return result.WithExceptions(exception);
    }

    #endregion WithException extensions
}