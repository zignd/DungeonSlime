/// <summary>
/// Entry point for the Content Builder project, 
/// which when executed will build content according to the "Content Collection Strategy" defined in the MyContentCollector class.
/// </summary>
/// <remarks>
/// Make sure to validate the directory paths in the "ContentBuilderParams" for your specific project.
/// For more details regarding the Content Builder, see the MonoGame documentation: <tbc.>
/// </remarks>

using Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using MonoGame.Framework.Content.Pipeline.Builder;

// Parse command-line arguments passed from the game's MSBuild target
// This allows the game project to control where content is output
var contentCollectionArgs = ContentBuilderParams.Parse(args);
var contentCollector = new MyContentCollector();
contentCollector.Run(contentCollectionArgs);