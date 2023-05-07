use std::fs;
use std::process::Command;

use crate::enums;
use crate::utils;

/**
 * Search current directory for file names ending with 'source_extension', construct a call to the appropriate
 * external utility, and convert the file with an output of the original file's name, but with 'target_extension'.
 */
pub fn run_batch(
    conversion_type: &enums::ConversionType,
    source_extension: &String,
    target_extension: &String,
    ignore_utility_check: bool,
    overwrite: bool,
) {
    let command = match conversion_type {
        enums::ConversionType::Audio => "sox",
        enums::ConversionType::Image => "convert",
        enums::ConversionType::Video => "ffmpeg",
    };

    if ignore_utility_check || utils::is_in_path(command) {
        let paths = fs::read_dir("./").unwrap();

        for path in paths {
            let clean_path = path.unwrap().path().display().to_string();

            if clean_path
                .to_lowercase()
                .contains(&format!(".{}", source_extension))
            {
                let input_file = clean_path;

                let file_stem = utils::extract_file_stem(&input_file);

                let output_file = format!("{}.{}", file_stem, target_extension);

                println!("Converting {} to {}...", input_file, output_file);

                let mut command = Command::new(command);

                match conversion_type {
                    enums::ConversionType::Audio => {
                        if overwrite {
                            command.arg("--clobber");
                        }

                        command.arg(input_file).arg(output_file);
                    }
                    enums::ConversionType::Image => {
                        command.arg(input_file).arg(output_file);
                    }
                    enums::ConversionType::Video => {
                        if overwrite {
                            command.arg("-y");
                        }

                        command.arg("-i").arg(input_file).arg(output_file);
                    }
                }

                let mut child = command.spawn().unwrap();

                let _result = child.wait().unwrap();
            }
        }
    } else {
        println!(
            "This conversion is not available: '{}' is not found.",
            command
        );
    }
}
