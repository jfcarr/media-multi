using CommandLine;
using media_multi;

internal class Program
{
	public class Options
	{
		// General
		[Option('i', "ignore-utility-check", Required = false, Default = false, HelpText = "Do not check for required installations before performing a conversion.")]
		public bool IgnoreUtilityCheck { get; set; }

		[Option('o', "overwrite", Required = false, Default = false, HelpText = "Overwrite existing target files.")]
		public bool OverwriteExistingTargetFiles { get; set; }

		// Custom Conversion
		[Option("src-ext", Required = false, Default = "", HelpText = "Extension of source file(s), e.g. mkv")]
		public string SourceExtension { get; set; }

		[Option("tgt-ext", Required = false, Default = "", HelpText = "Extension of target file(s), e.g. mp4")]
		public string TargetExtension { get; set; }

		[Option("ctype", Required = false, Default = "", HelpText = "Type of conversion.  One of: audio, image, or video.")]
		public string ConversionType { get; set; }

		// Audio
		[Option("flac-to-mp3", Required = false, Default = false, HelpText = "Convert all .flac files in current directory to .mp3")]
		public bool FlacToMp3 { get; set; }

		[Option("wav-to-mp3", Required = false, Default = false, HelpText = "Convert all .wav files in current directory to .mp3")]
		public bool WavToMp3 { get; set; }

		[Option("wma-to-mp3", Required = false, Default = false, HelpText = "Convert all .wma files in current directory to .mp3")]
		public bool WmaToMp3 { get; set; }

		// Images
		[Option("fits-to-jpg", Required = false, Default = false, HelpText = "Convert all .fits files in current directory to .jpg")]
		public bool FitsToJpg { get; set; }

		[Option("fits-to-png", Required = false, Default = false, HelpText = "Convert all .fits files in current directory to .png")]
		public bool FitsToPng { get; set; }

		[Option("jpg-to-fits", Required = false, Default = false, HelpText = "Convert all .jpg files in current directory to .fits")]
		public bool JpgToFits { get; set; }

		[Option("jpg-to-pdf", Required = false, Default = false, HelpText = "Convert all .jpg files in current directory to .pdf")]
		public bool JpgToPdf { get; set; }

		[Option("jpg-to-png", Required = false, Default = false, HelpText = "Convert all .jpg files in current directory to .png")]
		public bool JpgToPng { get; set; }

		[Option("png-to-fits", Required = false, Default = false, HelpText = "Convert all .png files in current directory to .fits")]
		public bool PngToFits { get; set; }

		[Option("png-to-jpg", Required = false, Default = false, HelpText = "Convert all .png files in current directory to .jpg")]
		public bool PngToJpg { get; set; }

		[Option("tif-to-jpg", Required = false, Default = false, HelpText = "Convert all .tif files in current directory to .jpg")]
		public bool TifToJpg { get; set; }

		[Option("tif-to-pdf", Required = false, Default = false, HelpText = "Convert all .tif files in current directory to .pdf")]
		public bool TifToPdf { get; set; }

		[Option("webp-to-jpg", Required = false, Default = false, HelpText = "Convert all .webp files in current directory to .jpg")]
		public bool WebpToJpg { get; set; }

		// Video
		[Option("avi-to-mp4", Required = false, Default = false, HelpText = "Convert all .avi files in current directory to .mp4")]
		public bool AviToMp4 { get; set; }

		[Option("mkv-to-mp4", Required = false, Default = false, HelpText = "Convert all .mkv files in current directory to .mp4")]
		public bool MkvToMp4 { get; set; }

		[Option("mp4-to-mp3", Required = false, Default = false, HelpText = "Convert all .mp4 files in current directory to .mp3 (extract audio track from video file)")]
		public bool Mp4ToMp3 { get; set; }

		[Option("webm-to-mp4", Required = false, Default = false, HelpText = "Convert all .webm files in current directory to .mp4")]
		public bool WebmToMp4 { get; set; }

	}

	private static void Main(string[] args)
	{
		CommandLine.Parser.Default.ParseArguments<Options>(args)
			.WithParsed(RunOptions)
			.WithNotParsed(HandleParseError);
	}

	static void RunOptions(Options opts)
	{
		var argBundle = new List<ArgBundle>();

		// Audio
		if (opts.FlacToMp3) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Audio, SourceExtension = "flac", TargetExtension = "mp3" });

		if (opts.WavToMp3) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Audio, SourceExtension = "wav", TargetExtension = "mp3" });

		if (opts.WmaToMp3) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Audio, SourceExtension = "wma", TargetExtension = "mp3" });

		// Images
		if (opts.FitsToJpg) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "fits", TargetExtension = "jpg" });

		if (opts.FitsToPng) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "fits", TargetExtension = "png" });

		if (opts.JpgToFits) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "jpg", TargetExtension = "fits" });

		if (opts.JpgToPdf) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "jpg", TargetExtension = "pdf" });

		if (opts.JpgToPng) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "jpg", TargetExtension = "png" });

		if (opts.PngToFits) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "png", TargetExtension = "fits" });

		if (opts.PngToJpg) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "png", TargetExtension = "jpg" });

		if (opts.TifToJpg) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "tif", TargetExtension = "jpg" });

		if (opts.TifToPdf) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "tif", TargetExtension = "pdf" });

		if (opts.WebpToJpg) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "webp", TargetExtension = "jpg" });

		// Video
		if (opts.AviToMp4) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Video, SourceExtension = "avi", TargetExtension = "mp4" });

		if (opts.MkvToMp4) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Video, SourceExtension = "mkv", TargetExtension = "mp4" });

		if (opts.Mp4ToMp3) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Video, SourceExtension = "mp4", TargetExtension = "mp3" });

		if (opts.WebmToMp4) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Video, SourceExtension = "webm", TargetExtension = "mp4" });

		var operationCount = 0;

		var converter = new Converter();

		if (!string.IsNullOrEmpty(opts.SourceExtension) && !string.IsNullOrEmpty(opts.TargetExtension) && !string.IsNullOrEmpty(opts.ConversionType))
		{
			var conversionType = ConversionType.Unknown;

			switch (opts.ConversionType)
			{
				case "audio":
					conversionType = ConversionType.Audio;
					break;
				case "image":
					conversionType = ConversionType.Image;
					break;
				case "video":
					conversionType = ConversionType.Video;
					break;
				default:
					break;
			}

			if (conversionType != ConversionType.Unknown)
			{
				converter.RunBatch(
					conversionType,
					opts.SourceExtension,
					opts.TargetExtension,
					opts.IgnoreUtilityCheck,
					opts.OverwriteExistingTargetFiles
				);

				operationCount++;
			}
		}

		foreach (var argItem in argBundle)
		{
			converter.RunBatch(
				argItem.TypeOfConversion,
				argItem.SourceExtension,
				argItem.TargetExtension,
				opts.IgnoreUtilityCheck,
				opts.OverwriteExistingTargetFiles
			);

			operationCount++;
		}

		if (operationCount == 0)
			Console.WriteLine("I'm not sure what you want to do. Use --help to see a list of options.");
	}

	static void HandleParseError(IEnumerable<Error> errs)
	{
		foreach (var err in errs)
		{
			if (!err.ToString().Contains("HelpRequestedError"))
				Console.WriteLine($"  {err}");
		}
	}
}