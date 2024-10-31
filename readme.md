# White Knuckle demo custom Hand sprite mod

This mod for the demo version of White Knuckle was made using [BepInEx5](https://github.com/BepInEx/BepInEx).

## Mod features

Loading of custom hand sprite (see Custom hand sprites section)

## Mod usage requirements

To use this mod, you must have **BepInEx5 x64** installed in the game, and set `HideManagerGameObject` to `true` in
BepInEx5's configuration file at `<game_path>\BepInEx\config\BepInEx.cfg`.

If you've installed BepInEx5 x64 but don’t see the configuration file, start the game once and then exit; the file will
be created at that location.

---

## Releases

If you just want to install the mod, use a pre-built binary.

See releases section of this repository.

Check Mod Usage section for usage instructions.

---

## Building

This C# project was created using BepInEx5's dotnet template (see the plugin development tutorial
at https://docs.bepinex.dev/articles/dev_guide/plugin_tutorial/index.html), with target framework (TFM) `netstandard2.1`
and Unity version `2022.3.32`. This translates into the command
`dotnet new bepinex5plugin -n <plugin_name> -T netstandard2.1 -U 2022.3.32`

Requires .NET SDK 2.1 x64 (download from Microsoft: https://dotnet.microsoft.com/en-us/download/dotnet/2.1)

When cloning this repository, you will be missing some files which are generated automatically by dotnet. Simply run
`dotnet restore .` in the solution directory to generate those files.

Use `dotnet build` inside the project's root directory to build the project; the built binary will be under the bin
folder. This command builds the binary, and also performs the same action as `dotnet restore .` prior to building.

Use `dotnet clean` to clean the solution of its `bin` and `obj` directories.

Alternative, you can use your favorite IDE to open the project's `.csproj` file.

**Required game files**: this project uses the game's `Assembly-CSharp.dll` file, which cannot be
distributed via this repository; copy that file from `<game_path>\White Knuckle_Data\Managed`
directory into the project's `lib` directory.

For faster development and testing iteration, this project has a post-build event that installs the mod by copying the
mod's `.dll` into the appropriate folder (note: post-build event target path is relevant to my machine; **change to
point to the game path in your machine**).

---

## Mod usage

### Installation

Place the compiled mod's `.dll` into the `<game_path>\BepInEx\plugins` directory.

### Mod configuration

After installing the mod and running the game once, a configuration file will be created in
`<game_path>\BepInEx\config`, which you can edit in a text editor to configure the mod's options.
Currently, the only option is to enable or disable the mod.

### Custom hand sprites

Custom hand textures can be made by using the base `template.png` image in
`<game_dir>\BepinEx\plugins\com.tonebacas.white_knuckle_demo_custom_hand_sprite\assets`.

The game uses a 512x512 pixels sprite atlas, with 128x128 pixels sprite blocks. This mod
assumes you're using the atlas' base resolution of 512x512 pixels; if you use something else, things will start to
break.

You can use the included GIMP 2.10 project, which features a simple gloved hand sprite work. You can change the color of
the gloves by painting on the `glove-color` layer (make sure you're not painting the layer mask).

Save the image as a `png` file, with the name `left_hand.png` or `right_hand.png` accordingly.

**Note**: when saving the modified hand sprite, make sure you're exporting with image format RGBA32 (or RGBA 8 bits per
channel, depending on your image editing software). This mod loads images in that format, so saving images in other
formats might not work as expected.

**Note 2**: when exporting with GIMP, in the png export options, you can disable every option to reduce file size.

![gimp 2.10 png export settings, with the following settings disabled: Interlacing, Save background color, Save gamma, Save layer offset, Save resolution, Save creation time, Save comment, Save color values from transparent pixels, Save Exif data, Save XMP data, Save IPTC data, Save thumbnail, and Save color profile. Export settings changed to 8bpc RGBA, Compression level 9](doc/gimp-png-export-settings.png)

You can use different sprite files for each hand by naming your custom sprite file to `left_hand.png` and
`right_hand.png` (the game looks for both files; if any of them doesn't exist, the mod just defaults to the original
sprite atlas for that hand). If you want to use the same sprite for both hands, just make a copy of and rename the file.
