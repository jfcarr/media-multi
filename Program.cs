using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using media_multi;
using Spectre.Console;
using Spectre.Console.Cli;

internal class Program
{
	private static void Main(string[] args)
	{
		var app = new CommandApp<ConversionCommand>();

		app.Run(args);
	}
}

internal sealed class ConversionCommand : Command<ConversionCommand.Settings>
{
	public sealed class Settings : CommandSettings
	{
		// General
		[CommandOption("-i|--ignore-utility-check")]
		[DefaultValue(false)]
		[Description("Do not check for required installations before performing a conversion.")]
		public bool IgnoreUtilityCheck { get; init; }

		[CommandOption("-o|--overwrite")]
		[DefaultValue(false)]
		[Description("Overwrite existing target files.")]
		public bool OverwriteExistingTargetFiles { get; init; }

		// Custom Conversion
		[CommandOption("--src-ext")]
		[DefaultValue("")]
		[Description("Extension of source file(s), e.g. mkv")]
		public string SourceExtension { get; init; }

		[CommandOption("--tgt-ext")]
		[DefaultValue("")]
		[Description("Extension of target file(s), e.g. mp4")]
		public string TargetExtension { get; init; }

		[CommandOption("--ctype")]
		[DefaultValue("")]
		[Description("Type of conversion.  One of: audio, image, or video.")]
		public string ConversionType { get; init; }

		// Audio Presets
		[CommandOption("--flac-to-mp3")]
		[DefaultValue(false)]
		[Description("Convert all .flac files in current directory to .mp3")]
		public bool FlacToMp3 { get; init; }

		[CommandOption("--wav-to-mp3")]
		[DefaultValue(false)]
		[Description("Convert all .wav files in current directory to .mp3")]
		public bool WavToMp3 { get; init; }

		[CommandOption("--wma-to-mp3")]
		[DefaultValue(false)]
		[Description("Convert all .wma files in current directory to .mp3")]
		public bool WmaToMp3 { get; init; }

		// Image Presets
		[CommandOption("--fits-to-jpg")]
		[DefaultValue(false)]
		[Description("Convert all .fits files in current directory to .jpg")]
		public bool FitsToJpg { get; init; }

		[CommandOption("--fits-to-png")]
		[DefaultValue(false)]
		[Description("Convert all .fits files in current directory to .png")]
		public bool FitsToPng { get; init; }

		[CommandOption("--jpg-to-fits")]
		[DefaultValue(false)]
		[Description("Convert all .jpg files in current directory to .fits")]
		public bool JpgToFits { get; init; }

		[CommandOption("--jpg-to-pdf")]
		[DefaultValue(false)]
		[Description("Convert all .jpg files in current directory to .pdf")]
		public bool JpgToPdf { get; init; }

		[CommandOption("--jpg-to-png")]
		[DefaultValue(false)]
		[Description("Convert all .jpg files in current directory to .png")]
		public bool JpgToPng { get; init; }

		[CommandOption("--png-to-fits")]
		[DefaultValue(false)]
		[Description("Convert all .png files in current directory to .fits")]
		public bool PngToFits { get; init; }

		[CommandOption("--png-to-jpg")]
		[DefaultValue(false)]
		[Description("Convert all .png files in current directory to .jpg")]
		public bool PngToJpg { get; init; }

		[CommandOption("--tif-to-jpg")]
		[DefaultValue(false)]
		[Description("Convert all .tif files in current directory to .jpg")]
		public bool TifToJpg { get; init; }

		[CommandOption("--tif-to-pdf")]
		[DefaultValue(false)]
		[Description("Convert all .tif files in current directory to .pdf")]
		public bool TifToPdf { get; init; }

		[CommandOption("--webp-to-jpg")]
		[DefaultValue(false)]
		[Description("Convert all .webp files in current directory to .jpg")]
		public bool WebpToJpg { get; init; }

		// Video
		[CommandOption("--avi-to-mp4")]
		[DefaultValue(false)]
		[Description("Convert all .avi files in current directory to .mp4")]
		public bool AviToMp4 { get; init; }

		[CommandOption("--mkv-to-mp4")]
		[DefaultValue(false)]
		[Description("Convert all .mkv files in current directory to .mp4")]
		public bool MkvToMp4 { get; init; }

		[CommandOption("--mp4-to-mp3")]
		[DefaultValue(false)]
		[Description("Convert all .mp4 files in current directory to .mp3 (extract audio track from video file)")]
		public bool Mp4ToMp3 { get; init; }

		[CommandOption("--webm-to-mp4")]
		[DefaultValue(false)]
		[Description("Convert all .webm files in current directory to .mp4")]
		public bool WebmToMp4 { get; init; }
	}

	public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
	{
		var argBundle = new List<ArgBundle>();

		// Audio
		if (settings.FlacToMp3) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Audio, SourceExtension = "flac", TargetExtension = "mp3" });

		if (settings.WavToMp3) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Audio, SourceExtension = "wav", TargetExtension = "mp3" });

		if (settings.WmaToMp3) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Audio, SourceExtension = "wma", TargetExtension = "mp3" });

		// Images
		if (settings.FitsToJpg) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "fits", TargetExtension = "jpg" });

		if (settings.FitsToPng) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "fits", TargetExtension = "png" });

		if (settings.JpgToFits) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "jpg", TargetExtension = "fits" });

		if (settings.JpgToPdf) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "jpg", TargetExtension = "pdf" });

		if (settings.JpgToPng) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "jpg", TargetExtension = "png" });

		if (settings.PngToFits) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "png", TargetExtension = "fits" });

		if (settings.PngToJpg) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "png", TargetExtension = "jpg" });

		if (settings.TifToJpg) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "tif", TargetExtension = "jpg" });

		if (settings.TifToPdf) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "tif", TargetExtension = "pdf" });

		if (settings.WebpToJpg) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Image, SourceExtension = "webp", TargetExtension = "jpg" });

		// Video
		if (settings.AviToMp4) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Video, SourceExtension = "avi", TargetExtension = "mp4" });

		if (settings.MkvToMp4) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Video, SourceExtension = "mkv", TargetExtension = "mp4" });

		if (settings.Mp4ToMp3) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Video, SourceExtension = "mp4", TargetExtension = "mp3" });

		if (settings.WebmToMp4) argBundle.Add(new ArgBundle { TypeOfConversion = ConversionType.Video, SourceExtension = "webm", TargetExtension = "mp4" });

		var operationCount = 0;

		var converter = new Converter();

		if (!string.IsNullOrEmpty(settings.SourceExtension) && !string.IsNullOrEmpty(settings.TargetExtension) && !string.IsNullOrEmpty(settings.ConversionType))
		{
			var conversionType = ConversionType.Unknown;

			switch (settings.ConversionType)
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
					settings.SourceExtension,
					settings.TargetExtension,
					settings.IgnoreUtilityCheck,
					settings.OverwriteExistingTargetFiles
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
				settings.IgnoreUtilityCheck,
				settings.OverwriteExistingTargetFiles
			);

			operationCount++;
		}

		if (operationCount == 0)
			AnsiConsole.MarkupLine("[red]I'm not sure what you want to do.[/] Use --help to see a list of options.");

		return 0;
	}
}