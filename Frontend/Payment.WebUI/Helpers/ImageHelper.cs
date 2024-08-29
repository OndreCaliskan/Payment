namespace Payment.WebUI.Helpers
{
    public static class ImageHelper
    {
        public static string SaveImage(IFormFile file, string folderPath = "wwwroot/img")
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Geçersiz dosya.");

            string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            string extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
                throw new ArgumentException("Sadece resim dosyaları yüklenebilir.");

            string filename = Guid.NewGuid().ToString() + extension;
            var path = Path.Combine(Directory.GetCurrentDirectory(), folderPath, filename);

            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/img/" + filename;
        }

        public static void DeleteImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return;

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath.TrimStart('/'));

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }

}
