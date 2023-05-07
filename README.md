# media-multi

This is a utility for performing bulk conversions of media (audio, image, and video) files.  All conversions are currently presets, but a future version will support custom conversions.  It is tested in Linux only!

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

## Example

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
