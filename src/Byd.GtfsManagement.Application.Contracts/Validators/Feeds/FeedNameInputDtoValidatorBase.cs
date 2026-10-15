using System;
using System.Globalization;
using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Feeds;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Feeds;

public abstract class FeedNameInputDtoValidatorBase<TInput> : AbstractValidator<TInput>
    where TInput : FeedNameInputDto
{
    protected FeedNameInputDtoValidatorBase()
    {
        RuleFor(input => input.Name)
            .MaximumLength(FeedConsts.MaxNameLength).WithErrorCode(FeedExceptionCodes.NameTooLong)
            .Must(HasValidNameShape).WithErrorCode(FeedExceptionCodes.NameInvalid);
    }

    private static bool HasValidNameShape(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return true;

        var letterCount = 0;
        var wordLength = 0;
        foreach (var character in name.Trim())
        {
            if (char.IsLetter(character))
            {
                if (++wordLength > FeedConsts.MaxNameWordLength)
                    return false;
                letterCount++;
                continue;
            }

            if (!char.IsDigit(character) &&
                char.GetUnicodeCategory(character) != UnicodeCategory.SpaceSeparator &&
                !char.IsPunctuation(character))
                return false;
            wordLength = 0;
        }

        return letterCount >= 2;
    }
}
