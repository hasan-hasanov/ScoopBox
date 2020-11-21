using ScoopBox.Scripts.UnMaterialized;
using ScoopBox.Translators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScoopBox.Abstractions
{
    public static class ScriptFactories
    {
        public static async Task<LiteralScript> ProcessLiteralScriptFactory(IList<string> scripts, IPowershellTranslator translator, string scriptName, IOptions options)
        {
            var literalScript = new LiteralScript(scripts, translator, scriptName);
            await literalScript.Process(options);
            return literalScript;
        }
    }
}
