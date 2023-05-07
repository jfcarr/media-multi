# media-multi

This is a utility for performing bulk conversions of media (audio, image, and video) files.  You can specify a preset conversion, or custom.  It is tested in Linux only!

This is a convenience utility.  It requires the following applications to be installed to handle the heavy lifting:

Conversion Type | Application
---------|----------
Audio | [SoX](https://sox.sourceforge.net/)
Image | convert _(part of [ImageMagick](https://imagemagick.org/))_
Video | [FFmpeg](https://ffmpeg.org/)

These utilities can be easily installed via your package manager.

I've also included a simple Makefile for handling builds and deployment.  You'll probably want to tweak it.

## Presets

To see a list of available conversion presets:

```bash
media_multi -h
```

Currently available:

Source | Target
---------|----------
 avi | mp4
 flac | mp3
 jpg | pdf
 jpg | png
 mkv | mp4
 mp4 | mp3
 png | jpg
 tif | jpg
 tif | pdf
 wav | mp3
 webm | mp4
 webp | jpg
 wma | mp3

## Example (preset)

Assuming you are in a directory with three AVI files:

```
test1.avi
test2.avi
test3.avi
```

And you want to convert those files to .mp4:

```bash
media_multi -o --avi-to-mp4
```

Result:

```
test1.avi
test1.mp4
test2.avi
test2.mp4
test3.avi
test3.mp4
```

Since this is a Rust project, you can also run it with Cargo, e.g.:

```bash
cargo run -- -o --avi-to-mp4
```

## Example (custom)

If you want to execute a bulk conversion that doesn't have a preset, you can specify all of the parameters yourself.

For example, if you wanted to convert all .ogg files in the current direct to .mp3, you'd use this:

```bash
media_multi --src-ext ogg --tgt-ext mp3 --ctype audio
```

## Multiple Conversions

You can perform multiple conversions at once.  For example, to convert all .avi and .mkv files in the current directory to .mp4, you'd do this:

```bash
media_multi --avi-to-mp4 --mkv-to-mp4
```

_However_, the order of operations is unpredictable.  So if one set of conversions is dependent upon the output of another, don't try to do it as a single step.  Issue multiple commands instead.  For example, if you want to convert all .avi files to .mp4, and then convert the resulting .mp4 files to .mp3, _don't_ do this:

```bash
media_multi --avi-to-mp4 --mp4-to-mp3
```
Do this, instead:

```bash
media_multi --avi-to-mp4
media_multi --mp4-to-mp3
```
