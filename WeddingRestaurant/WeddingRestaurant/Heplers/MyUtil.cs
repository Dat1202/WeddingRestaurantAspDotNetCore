namespace WeddingRestaurant.Heplers
{
    public static class MyUtil
    {
        public static string UploadHinh(IFormFile Hinh, string folder)
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", folder, Hinh.FileName);
                using (var myfile = new FileStream(fullPath, FileMode.CreateNew))
                {
                    Hinh.CopyTo(myfile);
                }
                return Hinh.FileName;
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
