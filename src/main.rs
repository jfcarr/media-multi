#![allow(unused_parens)]

use clap::Parser;

mod convert;
mod enums;
mod utils;

#[derive(Parser, Debug)]
#[command(author, version, about, long_about = None)]
struct Args {
    /// Do not check for required installations before performing a conversion
    #[arg(short, long)]
    ignore_utility_check: bool,

    /// Overwrite existing target files
    #[arg(short, long)]
    overwrite: bool,

    /// Convert all .avi files in current directory to .mp4
    #[arg(long)]
    avi_to_mp4: bool, // video

    /// Convert all .flac files in current directory to .mp3
    #[arg(long)]
    flac_to_mp3: bool, // audio

    /// Convert all .jpg files in current directory to .pdf
    #[arg(long)]
    jpg_to_pdf: bool, // image

    /// Convert all .jpg files in current directory to .png
    #[arg(long)]
    jpg_to_png: bool, // image

    /// Convert all .mkv files in current directory to .mp4
    #[arg(long)]
    mkv_to_mp4: bool, // video

    /// Convert all .mp4 files in current directory to .mp3 (extract audio track from video file)
    #[arg(long)]
    mp4_to_mp3: bool, // video

    /// Convert all .png files in current directory to .jpg
    #[arg(long)]
    png_to_jpg: bool, // image

    /// Convert all .tif files in current directory to .jpg
    #[arg(long)]
    tif_to_jpg: bool, // image

    /// Convert all .tif files in current directory to .pdf
    #[arg(long)]
    tif_to_pdf: bool, // image

    /// Convert all .wav files in current directory to .mp3
    #[arg(long)]
    wav_to_mp3: bool, // audio

    /// Convert all .webm files in current directory to .mp4
    #[arg(long)]
    webm_to_mp4: bool, // video

    /// Convert all .webp files in current directory to .jpg
    #[arg(long)]
    webp_to_jpg: bool, // image

    /// Convert all .wma files in current directory to .mp3
    #[arg(long)]
    wma_to_mp3: bool, // audio
}

fn main() {
    let args = Args::parse();

    // This vector holds 3 pieces of information in each row: conversion type (audio,
    // image, or video), source file extension, and target file extension.
    let mut arg_bundle = Vec::new();

    if args.avi_to_mp4 {
        arg_bundle.append(&mut vec![(enums::ConversionType::Video, "avi", "mp4")]);
    }
    if args.flac_to_mp3 {
        arg_bundle.append(&mut vec![(enums::ConversionType::Audio, "flac", "mp3")]);
    }
    if args.jpg_to_pdf {
        arg_bundle.append(&mut vec![(enums::ConversionType::Image, "jpg", "pdf")]);
    }
    if args.jpg_to_png {
        arg_bundle.append(&mut vec![(enums::ConversionType::Image, "jpg", "png")]);
    }
    if args.mkv_to_mp4 {
        arg_bundle.append(&mut vec![(enums::ConversionType::Video, "mkv", "mp4")]);
    }
    if args.mp4_to_mp3 {
        arg_bundle.append(&mut vec![(enums::ConversionType::Video, "mp4", "mp3")]);
    }
    if args.png_to_jpg {
        arg_bundle.append(&mut vec![(enums::ConversionType::Image, "png", "jpg")]);
    }
    if args.tif_to_jpg {
        arg_bundle.append(&mut vec![(enums::ConversionType::Image, "tif", "jpg")]);
    }
    if args.tif_to_pdf {
        arg_bundle.append(&mut vec![(enums::ConversionType::Image, "tif", "pdf")]);
    }
    if args.wav_to_mp3 {
        arg_bundle.append(&mut vec![(enums::ConversionType::Audio, "wav", "mp3")]);
    }
    if args.webm_to_mp4 {
        arg_bundle.append(&mut vec![(enums::ConversionType::Video, "webm", "mp4")]);
    }
    if args.webp_to_jpg {
        arg_bundle.append(&mut vec![(enums::ConversionType::Image, "webp", "jpg")]);
    }
    if args.wma_to_mp3 {
        arg_bundle.append(&mut vec![(enums::ConversionType::Audio, "wma", "mp3")]);
    }

    let mut operation_count = 0;

    // Perform the conversions by walking the arg_bundle vector and calling run_batch with the arguments from each row.
    for x in &arg_bundle {
        convert::run_batch(
            &x.0,
            &x.1.to_string(),
            &x.2.to_string(),
            args.ignore_utility_check,
            args.overwrite,
        );
        operation_count += 1;
    }

    if operation_count == 0 {
        println!(
            "I'm not sure what you want to do.  Use the -h argument to see a list of options."
        );
    }
}
