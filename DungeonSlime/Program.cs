using System;
using System.IO;
using DungeonSlime;

// Set the working directory to the executable's directory BEFORE creating the game.
// This is necessary for MonoGame's content manager to find relative paths in an app bundle.
Directory.SetCurrentDirectory(AppContext.BaseDirectory);

using var game = new Game1();
game.Run();
