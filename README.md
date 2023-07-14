# Rounded Corners
[![openupm](https://img.shields.io/npm/v/com.gilzoide.rounded-corners?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.gilzoide.rounded-corners/)

Unity UI `MaskableGraphic` components with rounded corners embedded in their meshes.

Since the rounded shape is in the mesh, you don't need to add extra textures for rounded rectangles of any size in your project.
You can also make rounded cuts to textures/sprites without using masks.


## Features
- Independent configurations for radius and number of triangles generated per corner.
- Supports custom materials and masking.
  Rounded corners are embedded in the generated meshes, so the components don't mess with your materials at all.
- Supports mesh modifiers, like Outline and Shadow.


## How to install
This package is available on the [openupm registry](https://openupm.com/) and can be installed using the [openupm-cli](https://github.com/openupm/openupm-cli):

```
openupm add com.gilzoide.rounded-corners
```

Otherwise, you can install directly using the [Unity Package Manager](https://docs.unity3d.com/Manual/upm-ui-giturl.html)
with the following URL:

```
https://github.com/gilzoide/unity-rounded-corners.git#1.0.0
```

Or you can clone this repository or download a snapshot of it directly inside your project's `Assets` or `Packages` folder.


## Components:
- [RoundedRect](Runtime/RoundedRect.cs): colored rectangle with rounded corners.
- [RoundedTexture](Runtime/RoundedTexture.cs): texture with rounded corners and configurable UV.
- [RoundedImage](Runtime/RoundedImage.cs): sprite with rounded corners.
  UVs are automatically fetched from sprite data.
  For now only simple filling is supported, no slicing nor tiling.


## Samples
This project comes with a [sample](Samples~/RoundedGraphics) scene that shows Rounded Corners features.