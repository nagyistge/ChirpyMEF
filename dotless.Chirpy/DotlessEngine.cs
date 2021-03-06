﻿namespace dotless.Chirpy
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using ChirpyInterface;
	using Core.Importers;
	using Core.Parser;
	using Core.Stylizers;

	[Export(typeof (IEngine))]
	[EngineMetadata("Dotless", "1.2.1.0", "less")]
	public class DotlessEngine : SingleEngineBase
	{
		public override List<string> GetDependencies(string contents, string filename)
		{
			return Try(p =>
				{
					p.Parse(contents, filename);
					return p.Importer.Imports;
				});
		}

		public override string Process(string contents, string filename, out string outputExtension)
		{
			outputExtension = "css";

			return Try(p => p.Parse(contents, filename).AppendCSS());
		}


		T Try<T>(Func<Parser, T> action)
		{
			Parser parser = null;
			try
			{
				parser = new Parser(new ChirpyStyaliser(), new Importer());
				return action(parser);
			}
			catch (Exception e)
			{
				if(parser == null)
					throw;

				var stylizer = parser.Stylizer as ChirpyStyaliser;

				if (stylizer == null || stylizer.LastZone == null)
					throw;

				var zone = stylizer.LastZone;

				throw new ChirpyException(zone.Message, zone.FileName, zone.LineNumber, zone.Position, zone.Extract.Line);
			}
		}

		class ChirpyStyaliser : IStylizer
		{
			public Zone LastZone { get; private set; }

			public string Stylize(Zone zone)
			{
				LastZone = zone;

				return new PlainStylizer().Stylize(zone);
			}
		}
	}
}
