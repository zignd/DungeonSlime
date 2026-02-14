# DungeonSlime

A MonoGame project for macOS with a custom content pipeline and app bundling via Make.

## Requirements

- .NET SDK 9.0
- MonoGame 3.8.5 (via NuGet)
- ImageMagick (`magick`) and macOS `sips`/`iconutil` tools

## Build and Run

Build the macOS arm64 app bundle:

```
make release-macos-arm64
```

Install for local testing:

```
make install-macos-arm64
```

## Project Structure

- `DungeonSlime.csproj` - game project
- `Content/` - MonoGame content project and assets
- `MonoGameLibrary/` - shared engine code
- `Makefile` - build and release automation

## Notes

- The release target builds a macOS app bundle in `bin/Release/DungeonSlime.app`.
- The icon pipeline uses `Icon.ico` as the source and generates the `.icns` during release.
