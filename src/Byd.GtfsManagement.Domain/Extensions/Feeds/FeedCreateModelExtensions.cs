using System;
using System.IO;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Models.Feeds;

namespace Byd.GtfsManagement.Extensions.Feeds;

public static class FeedCreateModelExtensions
{
    public static string CreateDefaultName(this FeedCreateModel model, Guid organizationId, DateTime createdAtUtc)
    {
        ArgumentNullException.ThrowIfNull(model);
        var suffix = $"-{createdAtUtc.ToUniversalTime():yyyyMMddHHmmssfffffff}-{organizationId}";
        var stem = Path.GetFileNameWithoutExtension(model.FileName)?.Trim();
        if (string.IsNullOrWhiteSpace(stem))
            stem = Path.GetFileNameWithoutExtension(GtfsFileNames.Archive);
        return stem[..Math.Min(stem.Length, FeedConsts.MaxNameLength - suffix.Length)] + suffix;
    }
}
