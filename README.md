# DungeonSlime

A 2D game built with MonoGame featuring a snake-like slime character navigating dungeons.

## Requirements

- **macOS**: arm64 or x64
- **.NET SDK**: 9.0 or higher
- **MonoGame**: 3.8.5 (via NuGet)
- **Build Tools**: ImageMagick (`magick`), macOS `sips`, `iconutil`, `SetFile`

## Building

Navigate to the `DungeonSlime/` project directory:

```bash
cd DungeonSlime
```

Build a macOS arm64 app bundle:

```bash
make release-macos-arm64
```

The app bundle will be created at `bin/Release/DungeonSlime.app`.

Install to ~/Applications for local testing:

```bash
make install-macos-arm64
```

## Project Structure

```
DungeonSlime/
├── Content/              # MonoGame content project (fonts, images, audio)
├── DungeonSlime/         # Main game project
│   ├── GameObjects/      # Game entities (Slime, Bat, etc.)
│   ├── Scenes/           # Scene management (TitleScene, GameScene)
│   ├── UI/               # Gum UI components
│   ├── Makefile          # Build automation
│   └── Icon.ico          # App icon source
└── MonoGameLibrary/      # Shared game engine utilities
```

## Development

- **Run in development**: Open `DungeonSlime.sln` in your IDE and run the `DungeonSlime` project
- **Clean build artifacts**: `make clean`
- **Build targets**: See `make help` for all available targets

## Features

- High score persistence (saved to JSON)
- Custom UI built with Gum
- Texture atlas and tilemap support
- Audio system with theme music
- macOS app bundle with custom icon

## Notes

- The icon pipeline converts `Icon.ico` to `.icns` format during the release build
- Content is copied to both `MacOS/` and `Resources/` folders in the app bundle for proper path resolution
