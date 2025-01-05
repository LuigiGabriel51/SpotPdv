using SpotPdv.Application.Enums;
using SpotPdv.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.Helpers
{
    public static class Converters
    {
        public static UnitiType UnityTypeConverter(string value)
        {
            switch (value)
            {
                case "Grama":
                    return UnitiType.Grama;
                case "Kilograma":
                    return UnitiType.Kilograma;
                case "Unidade":
                    return UnitiType.Unidade;
                case "Pacote":
                    return UnitiType.Pacote;
                case "Litro":
                    return UnitiType.Litro;
            }
            return UnitiType.nulo;
        }

        public static List<BackgroundCategoryModel> GenerateRandomColors(int count = 200)
        {
            var colors = new HashSet<string>();
            var models = new List<BackgroundCategoryModel>();
            var random = new Random();

            while (colors.Count < count)
            {
                int red = random.Next(0, 256);
                int green = random.Next(0, 256);
                int blue = random.Next(0, 256);

                string hexColor = $"#{red:X2}{green:X2}{blue:X2}";

                if (colors.Add(hexColor))
                {
                    string nameColor = $"{colors.Count}";

                    models.Add(new BackgroundCategoryModel
                    {
                        Color = hexColor,
                        NameColor = nameColor
                    });
                }
            }

            return models;
        }

        public static async Task<ImageModel> PickAndShow(PickOptions options)
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {
                        using var stream = await result.OpenReadAsync();

                        // Lê o conteúdo do stream em um array de bytes
                        byte[] imageBytes;
                        using (var memoryStream = new MemoryStream())
                        {
                            await stream.CopyToAsync(memoryStream);
                            imageBytes = memoryStream.ToArray();
                        }

                        var imageModel = new ImageModel() { Image = imageBytes, NameImage = result.FileName };
                        return imageModel; // A propriedade `ImageBytes` deve ser definida em sua classe

                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
