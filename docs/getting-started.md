# Getting Starting
Before creating an element, there are a couple of things you need to consider prior to starting:

## Experience level & IDE
This guide assumes you already know the following:
- You have a basic understanding of C#
- You have an IDE already set up
- You have created a directory Dependants/ with the _Status Hud Continued_ `.dll` included
- You are able to compile and run this example mod without issues

If you are unable to do any of the following, please refer to the [Vintage Story wiki](https://wiki.vintagestory.at/Special:MyLanguage/Modding:Getting_Started) for assistance.

## Licences & Permission
As of writing, both this mod & _Status Hud Continued_ are licensed under CC-BY-4.0.
- If the element you are making refers to code from the base game, or a mod you are an author of, then you are in the clear. Just remember to provide credit!
- If you are referring to code from someone _else's_ mod, ensure you have checked the mods licence AND/OR gotten permission from the mod author.

---

If you are able to follow the above, then we can get into the fun stuff!

# Creating A Custom Element
At the end of this documentation, you should be able to get something like the following:
![example-element.png](/assets/example-element.png)

## The Element Code
See [example_element.cs](https://github.com/Gravydigger/statushud-element-example/blob/main/StatusHudAddonExample/example_element.cs).
This `.cs` file contains 2 classes, the StatusHudElement and the StatusHudRenderer, which is the brain of the element. You can make as many elements as you wish, but make sure each element is self contained to just one `.cs` file.

## The Assets Folder
The following files and directories _**MUST**_ be included with your mod:
```
assets
└── statushudcont
    ├── lang
    │   ├── en.json
    │   └─ ...
    └── textures
        ├── <texture>.png
        └─ ...
```
_Status HUD Continued_ looks for files under the `statushudcont` domain and process them internally

### Language Files
_Status HUD Continued_ used the elements internal `Name` to dynamically parse the language files, such as below:
```json
{
  "example-name": "Example",
  "example-desc": "An example element to show a proof of concept"
}
```
This is the minimum needed for a single element.

If you have multiple options for your element, you'll need to add the following aswell:
```json
{
  "example-opt-text": "Base",
  "example-opt-tooltip": "Pick if the text should be zero based or one based.",
  "example-opt-1": "Zero Based",
  "example-opt-2": "One Based"
}
```
Note that `"example-opt-1"` starts at `1`, and can go up to `n` as needed.

See [en.json](https://github.com/Gravydigger/statushud-element-example/blob/main/StatusHudAddonExample/assets/statushudcont/lang/en.json) for a more up-to-date example.

### Texture Files & Text
To maintain visual consistancy with _Status HUD Continued_, each file is 64x64, with each texture is limited to the 48x48 center most pixels, with the remaining space used for a 'shadow'.
The shadow is achieved using [GIMP](https://www.gimp.org/downloads/) by creating a black version of the texture layer underneath, with a gaussian blur filter applied with 3.0 strength.

Text colour and opacity can be specified by a `Vec4f()` as seen in [example_element.cs](https://github.com/Gravydigger/statushud-element-example/blob/main/StatusHudAddonExample/example_element.cs). It will used the default colour if none is specified

**NOTE:** The colour and opacity of the icon is baked into the file. Both the text and the texture is unable to dynamically shift between colour / opacity without reloading the element.

## Adding The Element To _Status HUD Continued_
StatusHudSystem.AddElementType(typeof(StatusHudAddonExampleElement));