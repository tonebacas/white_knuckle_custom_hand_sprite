# White Knuckle demo: Custom Hand sprite mod

This a mod for White Knuckle demo, made using [BepInEx5](https://github.com/BepInEx/BepInEx).

## Features

This mod loads custom sprite files to be used in the game, in place of the player character's hands.
See the [Usage section](#usage)  to learn how to install and use the mod.

## **Usage requirements

To use this mod, you must have **BepInEx5 x64** installed in the game (currently, latest version
is https://github.com/BepInEx/BepInEx/releases/download/v5.4.23.2/BepInEx_win_x64_5.4.23.2.zip)

Also, you have to configure BepInEx5 and set `HideManagerGameObject` to `true` in
the configuration file at `<game_path>\BepInEx\config\BepInEx.cfg`.

If you've installed BepInEx5 x64 but don’t see the configuration file, start the game once and then exit; the file will
be created at that location.

---

## Releases

If you just want to install the mod, use a pre-built binary.

[Download from Releases.](https://github.com/tonebacas/white_knuckle_custom_hand_sprite/releases/tag/1.0.0)

Check [Usage section](#usage) for usage instructions.

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

The project has a pre-build step to copy the necessary game files mentioned above. This can save you the hassle of
manually copying those files, and in case the game receives an update, this makes sure you're using the latest game
files for your build.

For faster development and testing iteration, this project also has a post-build event that installs the mod by copying
the mod's `.dll` into the appropriate folder, along with the asset files. Note: post-build event target path is relevant
to my machine; **change it to point to the game path in your machine**.

---

## Usage

### Installation

Extract the release file into the game directory.

### Uninstallation

Simply delete the mod's directory `<game_path>\BepInEx\plugins\com.tonebacas.white_knuckle_demo_custom_hand_sprite`.

### Configuration

After installing the mod and running the game once, a configuration file will be created in
`<game_path>\BepInEx\config`, which you can edit in a text editor to configure the mod's options.
Currently, there is only option to enable or disable the mod.

### Making and using custom hand sprites

Custom hand textures can be made by using the base `template.png` image in
`<game_dir>\BepinEx\plugins\com.tonebacas.white_knuckle_demo_custom_hand_sprite\assets`.

The game uses a 512x512 pixels sprite atlas, with 128x128 pixels sprite blocks. This mod
assumes you're using the atlas' base resolution of 512x512 pixels; if you use something else, things will start to
break.

You can use the included GIMP 2.10 project, which features a simple gloved hand sprite work. You can change the color of
the gloves by painting on the `glove-color` layer (make sure you're not painting the layer mask).

Save the image as a `png` file, with the name `left_hand.png` or `right_hand.png`, according to the hand you want to
texture, and save it under the `<game_dir>\BepinEx\plugins\com.tonebacas.white_knuckle_demo_custom_hand_sprite\assets`
directory.

You can use different sprite files for each hand by naming your custom sprite file to `left_hand.png` and
`right_hand.png` (the mod looks for both files, and if any of them doesn't exist, it just uses the default sprite for
that hand). If you want to use the same sprite for both hands, just copy/paste and rename the file.

**Note**: when saving the modified hand sprite, make sure you're exporting with image format RGBA32 (or RGBA 8 bits per
channel, depending on your image editing software; they're the same thing).
This mod loads images in that format, so saving images in other
formats might not work as expected.

**Note 2**: when exporting with GIMP, in the png export options, you can disable every option to reduce file size.

![gimp 2.10 png export settings, with the following settings disabled: Interlacing, Save background color, Save gamma, Save layer offset, Save resolution, Save creation time, Save comment, Save color values from transparent pixels, Save Exif data, Save XMP data, Save IPTC data, Save thumbnail, and Save color profile. Export settings changed to 8bpc RGBA, Compression level 9](doc/gimp-png-export-settings.png)


