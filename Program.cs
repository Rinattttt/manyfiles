using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string url1 = "https://docs.yandex.ru/docs/view?url=ya-disk%3A%2F%2F%2Fdisk%2FМетодические%20указания%20к%20лабораторным%20работам%2FМДК.01.02%2F2курс%2FЛабораторные%20работы%2FЛабораторная%20работа%20№7-8%20-%20Системное%20тестирование.docx&name=Лабораторная%20работа%20№7-8%20-%20Системное%20тестирование.docx&uid=1611365357&nosw=1"; 
        string outputPath2 = "C:\\Users\\Alena\\1.txt";
        string url2 = "https://get.wallhere.com/photo/landscape-mountains-lake-nature-reflection-grass-sky-river-national-park-valley-wilderness-Alps-tree-autumn-leaf-mountain-season-tarn-loch-mountainous-landforms-mountain-range-590185.jpg"; // URL изображения
        string filePath2 = "C:\\Users\\Alena\\rrr.bmp";
        string audiopath1 = "C:\\Users\\Alena\\Music\\ATL_-_V_unison_47828855.mp3";
        string play1 = "C:\\Program Files\\Windows Media Player\\wmplayer.exe";
        string url3 = "https://rus.hitmotop.com/song/73390019";
        string outputpath = "C:\\Users\\Alena\\ATL.mp3";
        Stopwatch stopWatch1 = new Stopwatch();
        Stopwatch stopWatch2 = new Stopwatch();
        Stopwatch stopWatch3 = new Stopwatch();
        Stopwatch stopWatch4 = new Stopwatch();
        Console.WriteLine("Запуск аудиозаписи");
        stopWatch3.Start();
        await AudioStartAsync(play1, audiopath1);
        stopWatch3.Stop();
        Console.WriteLine("Скачивание файлов запущено...");
        stopWatch1.Start();
        await DownloadFileAsync(url1, outputPath2);
        stopWatch1.Stop();
        stopWatch2.Start();
        await DownloadImageAsync(url2, filePath2);
        stopWatch2.Stop();
        stopWatch4.Start();
        await DownloadAudioAsync(url3, outputpath);
        stopWatch4.Stop();
        Console.WriteLine("Скачивание завершено.");
        Console.WriteLine("Время выполнения первого скачивания и сохранения: " + stopWatch1.Elapsed.ToString());
        Console.WriteLine("Время выполнения второго скачивания и сохранения: " + stopWatch2.Elapsed.ToString());
        Console.WriteLine("Время выполнения третьего скачивания и сохранения: " + stopWatch4.Elapsed.ToString());
        Console.WriteLine("Время выполнения запуска проигрывания аудиозаписи: " + stopWatch3.Elapsed.ToString());
    }
    static async Task DownloadAudioAsync(string url, string outputPath)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                byte[] fileBytes = await httpClient.GetByteArrayAsync(url);
                await File.WriteAllBytesAsync(outputPath, fileBytes);
                Console.WriteLine($"Аудиофайл сохранен как {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при скачивании файла: {ex.Message}");
            }
        }
    }
    static async Task AudioStartAsync(string play, string audiopath)
    {
        try
        {
            if (File.Exists(audiopath))
            {
                await Task.Run(() =>
                Process.Start(play, audiopath));
            }
            else
            {
                Console.WriteLine("Аудиофайл для запуска не найден: " + audiopath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
    static async Task DownloadFileAsync(string url, string outputPath)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                byte[] fileBytes = await httpClient.GetByteArrayAsync(url);
                await File.WriteAllBytesAsync(outputPath, fileBytes);
                Console.WriteLine($"Файл сохранен как {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при скачивании файла: {ex.Message}");
            }
        }
    }
    static async Task DownloadImageAsync(string url, string filePath)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode(); // Проверка ответа на успешность

                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await stream.CopyToAsync(fileStream); // Копирование потока в файл
                    }
                    Console.WriteLine($"Картинка сохранена как {filePath}");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при скачивании файла: {ex.Message}");
            }
        }
    }
}
