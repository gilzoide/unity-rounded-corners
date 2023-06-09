# Rounded Corners
Unity UI `MaskableGraphic` components with rounded corners embedded in their meshes.


## Features
- Independent configurations for radius and number of triangles generated per corner.
- Supports custom materials and masking.
  Rounded corners are embedded in the generated meshes, so the components don't mess with your materials at all.
- Supports mesh modifiers, like Outline and Shadow.


## Components:
- [RoundedRect](Runtime/RoundedRect.cs): colored rectangle with rounded corners.
- [RoundedTexture](Runtime/RoundedTexture.cs): texture with rounded corners and configurable UV.
- [RoundedImage](Runtime/RoundedImage.cs): sprite with rounded corners.
  UVs are automatically fetched from sprite data.
  For now only simple filling is supported, no slicing nor tiling.