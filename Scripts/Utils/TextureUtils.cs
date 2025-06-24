using Godot;

public static class TextureUtils
{
	public static ImageTexture CreateSolidColorTexture(Color color, int width = 100, int height = 10)
	{
		// Create an empty image first (static method)
		var image = Image.CreateEmpty(width, height, false, Image.Format.Rgba8);

		// Fill pixels manually using SetPixel
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				image.SetPixel(x, y, color);
			}
		}

		// Create a texture from the image (static method)
		var texture = ImageTexture.CreateFromImage(image);
		return texture;
	}
}
