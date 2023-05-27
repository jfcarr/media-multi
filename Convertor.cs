using System.Runtime.InteropServices;
using System.Diagnostics;

namespace media_multi
{
	public class Converter
	{
		string _audioApplication;
		string _imageApplication;
		string _videoApplication;

		public Converter()
		{
			_audioApplication = $"sox{((RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) ? ".exe" : "")}";
			_imageApplication = $"convert{((RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) ? ".exe" : "")}";
			_videoApplication = $"ffmpeg{((RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) ? ".exe" : "")}";
		}

		/// <summary>
		/// Given a file name, walk the environment PATH to see if it's available.
		/// </summary>
		private bool IsFileInSystemPath(string fileName)
		{
			var splitChar = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? ';' : ':';

			string[] paths = Environment.GetEnvironmentVariable("PATH").Split(splitChar);

			foreach (string path in paths)
			{
				var fullPath = Path.Combine(path, fileName);
				if (File.Exists(fullPath))
					return true;
			}

			return false;
		}

		/// <summary>
		/// Search current directory for file names ending with 'sourceExtension', construct a call
		/// to the appropriate external utility, and convert the file with an output of the original
		/// file's name, but with 'targetExtension'.
		/// </summary>
		public void RunBatch(ConversionType conversionType, string sourceExtension, string targetExtension, bool ignoreUtilityCheck, bool overwrite)
		{
			string command = string.Empty;

			switch (conversionType)
			{
				case ConversionType.Audio:
					command = _audioApplication;
					break;
				case ConversionType.Image:
					command = _imageApplication;
					break;
				case ConversionType.Video:
					command = _videoApplication;
					break;
				default:
					break;
			}

			if (!string.IsNullOrEmpty(command) && (ignoreUtilityCheck || this.IsFileInSystemPath(command)))
			{
				var fileCount = 0;

				var files = Directory.GetFiles(".").OrderBy(f => f);
				foreach (var fileEntry in files)
				{
					if (fileEntry.ToLower().Contains($".{sourceExtension}"))
					{
						var sourceFile = Path.GetFileName(fileEntry);
						var targetFile = $"{Path.GetFileNameWithoutExtension(sourceFile)}.{targetExtension}";

						var targetFileExists = File.Exists(targetFile);
						if (targetFileExists && !overwrite)
						{
							Console.WriteLine($"Skipping {sourceFile}: target file {targetFile} exists.");
						}
						else
						{
							Console.WriteLine($"Converting {sourceFile} to {targetFile}...");

							if (targetFileExists)
							{
								File.Delete(targetFile);
							}

							var arguments = string.Empty;
							switch (conversionType)
							{
								case ConversionType.Audio:
									arguments = $"{sourceFile} {targetFile}";
									break;
								case ConversionType.Image:
									arguments = $"{sourceFile} {targetFile}";
									break;
								case ConversionType.Video:
									arguments = $"-i {sourceFile} {targetFile}";
									break;
								default:
									break;
							}

							Process conversionCommand = new Process();
							conversionCommand.StartInfo.FileName = command;
							conversionCommand.StartInfo.Arguments = arguments;
							conversionCommand.Start();
							conversionCommand.WaitForExit();

							fileCount++;
						}
					}
				}

				Console.WriteLine($"Processed {fileCount} file(s)");
			}
			else
			{
				Console.WriteLine($"This conversion is not available. '{command}' is not found.");
			}
		}
	}
}