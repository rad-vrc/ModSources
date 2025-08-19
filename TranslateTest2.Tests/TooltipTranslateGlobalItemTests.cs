using System.Collections.Generic;
using System.Reflection;
using TranslateTest2.Items;
using Xunit;

namespace TranslateTest2.Tests;

public class TooltipTranslateGlobalItemTests
{
    [Fact]
    public void SplitProtectedSegments_ReturnsIndependentCopyFromCache()
    {
        var type = typeof(TooltipTranslateGlobalItem);
        var method = type.GetMethod("SplitProtectedSegments", BindingFlags.NonPublic | BindingFlags.Static)!;

        const string input = "[abc]";
        var first = (List<(string text, bool protect)>)method.Invoke(null, new object?[] { input })!;
        first[0] = ("mutated", false);

        var second = (List<(string text, bool protect)>)method.Invoke(null, new object?[] { input })!;

        Assert.Equal("[abc]", second[0].text);
        Assert.True(second[0].protect);
    }
}
