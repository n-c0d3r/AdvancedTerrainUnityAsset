using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{

    public partial class ShaderBuilder
    {

        public static string PlaceholderReplace(string raw, KeyValuePair<string, string>[] placeholders)
        {

            string result = raw;

            foreach (KeyValuePair<string, string> placeholder in placeholders)
            {

                result = result.Replace(placeholder.Key, placeholder.Value);

            }

            return result;

        }

    }

}