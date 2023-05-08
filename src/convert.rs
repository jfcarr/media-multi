use std::fs;
use std::process::Command;

use crate::enums;
use crate::utils;
use std::env;

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
    let audio_application = format!(
        "sox{}",
        if env::consts::OS == "windows" {
            ".exe"
        } else {
            ""
        }
    );
    let image_application = format!(
        "convert{}",
        if env::consts::OS == "windows" {
            ".exe"
        } else {
            ""
        }
    );
    let video_application = format!(
        "ffmpeg{}",
        if env::consts::OS == "windows" {
            ".exe"
        } else {
            ""
        }
    );
    let command = match conversion_type {
        enums::ConversionType::Audio => &audio_application,
        enums::ConversionType::Image => &image_application,
        enums::ConversionType::Video => &video_application,
        enums::ConversionType::Unknown => todo!(),
    };

    if ignore_utility_check || utils::is_in_path(command) {
        let paths = fs::read_dir("./").unwrap();

        let mut file_count = 0;

        for path in paths {
            let clean_path = path.unwrap().path().display().to_string();

            if clean_path
                .to_lowercase()
                .contains(&format!(".{}", source_extension))
            {
                let input_file = clean_path;

                let file_stem = utils::extract_file_stem(&input_file);

                let output_file = format!("{}.{}", file_stem, target_extension);

                let target_file_exists = utils::file_exists(&output_file);

                if target_file_exists && !overwrite {
                    println!(
                        "Skipping {}: target file {} exists.",
                        input_file, output_file
                    );
                } else {
                    println!("Converting {} to {}...", input_file, output_file);

                    if target_file_exists {
                        utils::delete_file(&output_file);
                    }

                    let mut command = Command::new(command);

                    match conversion_type {
                        enums::ConversionType::Audio => {
                            command.arg(input_file).arg(output_file);
                        }
                        enums::ConversionType::Image => {
                            command.arg(input_file).arg(output_file);
                        }
                        enums::ConversionType::Video => {
                            command.arg("-i").arg(input_file).arg(output_file);
                        }
                        enums::ConversionType::Unknown => {
                            todo!()
                        }
                    }

                    let mut child = command.spawn().unwrap();

                    let _result = child.wait().unwrap();

                    file_count += 1;
                }
            }
        }

        println!("Processed {} file(s)", file_count);
    } else {
        println!(
            "This conversion is not available: '{}' is not found.",
            command
        );
    }
}
