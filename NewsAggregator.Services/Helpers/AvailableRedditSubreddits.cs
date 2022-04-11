using System.Collections.Generic;

namespace NewsAggregator.Services.Helpers
{
    public static class AvailableRedditSubreddits
    {
        public const string All = "all";
        public const string Csharp = "csharp";
        public const string Dotnet = "dotnet";

        public static IEnumerable<string> GetAll() => new List<string> { All, Csharp, Dotnet };
    }
}
