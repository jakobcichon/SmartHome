namespace SmartHome.Common.Regex;

public static class RegexAuxiliaryFunctions
{
    public static string GetGuidRegexPattern() => 
        @"[0-9a-fA-F]{8}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{12}";
}