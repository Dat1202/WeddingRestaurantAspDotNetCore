namespace WeddingRestaurant.Heplers
{
    public static class MyUtil
    {
        public static string UploadHinh(IFormFile Hinh, string folder, string Id)
        {
            try
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", folder);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var extension = Path.GetExtension(Hinh.FileName);
                var fileName = $"{Id}{extension}";
                var fullPath = Path.Combine(folderPath, fileName);

                using (var myfile = new FileStream(fullPath, FileMode.CreateNew))
                {
                    Hinh.CopyTo(myfile);
                }

                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return string.Empty;
            }
        }

        public static string ToVnd(this decimal giaTri)
        {
            return $"{giaTri:#,##0}đ";
        }
    }
}
