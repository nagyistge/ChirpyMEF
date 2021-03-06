namespace Chirpy.Engines
{
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using ChirpyInterface;

	[Export(typeof (IEngine))]
	[EngineMetadata("YUI Javascript Compressor", "1.6.0.2", "yui.js", true, Minifier = true)]
	public class YuiJavascriptCompressorEngine : SingleEngineBase
	{
		public override List<string> GetDependencies(string contents, string filename)
		{
			return null;
		}

		public override string Process(string contents, string filename, out string outputExtension)
		{
			outputExtension = "min.js";

			return Yahoo.Yui.Compressor.JavaScriptCompressor.Compress(contents);
		}
	}
}