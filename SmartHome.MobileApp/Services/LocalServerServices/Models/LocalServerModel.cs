using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using static SmartHome.Common.Regex.RegexAuxiliaryFunctions;

namespace SmartHome.MobileApp.Services.LocalServerServices.Models;

public record LocalServerModel: IParsable<LocalServerModel?>
{
    public Guid Guid { get; init; }
    
    public static LocalServerModel Parse(string s, IFormatProvider? provider)
    {
        var match = MatchGuidPattern(s);

        if (!match.Success)
            throw new FormatException("Wrong Guid format");

        if (!Guid.TryParse(match.Value, out var guid))
            throw new FormatException("Invalid Guid value");
        
        return new LocalServerModel
        {
            Guid = guid
        };
    }
    public static bool TryParse([NotNullWhen(true)] string? s, 
        IFormatProvider? provider, [MaybeNullWhen(false)] out LocalServerModel result)
    {
        result = null;
        if (string.IsNullOrWhiteSpace(s)) return false;
        
        var match = MatchGuidPattern(s);
        
        if (!match.Success || !Guid.TryParse(match.Value, out var guid)) return false;
        
        result = new LocalServerModel
        {
            Guid = guid
        };
        return true;
    }
    
    private static Match MatchGuidPattern(string s)
    {
        Regex regex = new(GetGuidRegexPattern());
        var match = regex.Match(s);
        return match;
    }

}
