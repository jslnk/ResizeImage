# ResizeImage
Resize images in a folder or single image

A simple library to resize an image with a given size and an optional suffix.

For example, the file path is "C:\user\dog.jpg", the library creates a resized image in destination Folder with "dog_test.jpg" ResizeImage.ImageResizer.ResizeImageWithSuffix(filePath, destFolder, 300, 300, "_test");

Below is to resize multiple images by passing a source folder.
ResizeImage.ImageResizer.ResizeMultipleImagesWithSuffix(sourceFolder, destinationFolder, 300, 300, "_test");
