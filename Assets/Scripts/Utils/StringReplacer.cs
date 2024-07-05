using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class StringReplacer
{
    public static string Replace(string input, Dictionary<string, string> replacement)
    {
        Regex regex = new Regex("{(?<placeholder>[a-z_][a-z0-9_]*?)}",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        return regex.Replace(input, m =>
        {
            string key = m.Groups["placeholder"].Value;
            if (replacement.TryGetValue(key, out string value))
                return value.ToString();

            Debug.LogError($"Variável utilizada em texto não encontrada: {key}");

            return key;
        });
    }
}
