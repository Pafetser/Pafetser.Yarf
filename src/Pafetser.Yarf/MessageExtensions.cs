namespace Pafetser.Yarf;

public static class MessageExtensions
{
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

    public static T WithMessages<T>(this T result, params Message[] messages) where T : Result
    {
        return result.WithMessages((IEnumerable<Message>)messages);
    }

    public static T WithMessage<T>(this T result, Message message) where T : Result
    {
        if (message is null) throw new ArgumentNullException(nameof(message));
        return result.WithMessages(message);
    }

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
}