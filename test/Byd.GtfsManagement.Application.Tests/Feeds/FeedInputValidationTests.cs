using Byd.GtfsManagement.Dtos.Feeds;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Validators.Feeds;
using Xunit;

namespace Byd.GtfsManagement.Feeds;

// Public upload and URL contracts reject malformed inputs before storage or download.
public class FeedInputValidationTests
{
    [Fact]
    public void Upload_RejectsRandomConsonantName()
    {
        var result = new UploadFeedDtoValidator().Validate(new UploadFeedDto
        {
            Name = "fnvkdfnbjsdnbbjksndkb", FileName = "schedule.zip", Data = [1]
        });

        Assert.Contains(result.Errors, error => error.ErrorCode == FeedExceptionCodes.NameInvalid);
    }

    [Fact]
    public void Upload_AllowsMissingNameForGeneratedDefault()
    {
        var result = new UploadFeedDtoValidator().Validate(new UploadFeedDto
        {
            FileName = "schedule.zip", Data = [1]
        });

        Assert.True(result.IsValid);
    }

    [Fact]
    public void UrlImport_RequiresHttpSourceAndAllowsHumanName()
    {
        var validator = new ImportFeedDtoValidator();
        var invalid = validator.Validate(new ImportFeedDto { Url = "file:///schedule.zip", Name = "City routes" });
        var valid = validator.Validate(new ImportFeedDto { Url = "https://example.org/schedule.zip", Name = "City routes" });

        Assert.Contains(invalid.Errors, error => error.ErrorCode == FeedExceptionCodes.SourceUrlInvalid);
        Assert.True(valid.IsValid);
    }

    [Fact]
    public void Draft_UsesNameRuleWithoutRequiringUrl()
    {
        var validator = new CreateDraftFeedDtoValidator();
        var invalid = validator.Validate(new CreateDraftFeedDto { Name = "fnvkdfnbjsdnbbjksndkb" });
        var unnamed = validator.Validate(new CreateDraftFeedDto());

        Assert.Contains(invalid.Errors, error => error.ErrorCode == FeedExceptionCodes.NameInvalid);
        Assert.True(unnamed.IsValid);
    }

    [Theory]
    [InlineData("İzmir Büyükşehir Belediyesi")]
    [InlineData("TCDD 2026 - Marmaray")]
    [InlineData("GTFS")]
    public void Name_AllowsNormalTransitNames(string name)
    {
        var result = new CreateDraftFeedDtoValidator().Validate(new CreateDraftFeedDto { Name = name });

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("12345")]
    [InlineData("ab\ncd")]
    [InlineData("ab😀")]
    public void Name_RejectsInvalidCharactersOrMissingLetters(string name)
    {
        var result = new CreateDraftFeedDtoValidator().Validate(new CreateDraftFeedDto { Name = name });

        Assert.Contains(result.Errors, error => error.ErrorCode == FeedExceptionCodes.NameInvalid);
    }
}
