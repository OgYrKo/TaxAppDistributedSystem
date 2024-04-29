using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ServerImplementation
{
    public class FileStorage
    {
        private static FileStorage instance;
        private static readonly object lockObject = new object();
        private string DocumentsDirectory;
        private string TempDirectory;
        private string DateDirectory;
        private List<string> fileNames;
        public List<string> FileNames { get { return fileNames; } }


        private FileStorage()
        {
            DocumentsDirectory = "Documents";
            if (!Directory.Exists(DocumentsDirectory))
                Directory.CreateDirectory(DocumentsDirectory);
            TempDirectory = "TempDocuments";
            if (!Directory.Exists(TempDirectory))
                Directory.CreateDirectory(TempDirectory);
            DateDirectory = "LastExecutionDate";
            if (!Directory.Exists(DateDirectory))
                Directory.CreateDirectory(DateDirectory);

            fileNames = GetExistingFiles();

        }

        public void SaveLastExecutionDate(string mailUser, DateTime lastExecutionTime)
        {
            // Путь к файлу, где будет храниться значение lastExecutionTime
            string filePath = Path.Combine(DateDirectory, mailUser);

            // Записываем значение lastExecutionTime в файл
            File.WriteAllText(filePath, lastExecutionTime.ToString());
        }

        public DateTime LoadLastExecutionTime(string mailUser)
        {
            // Путь к файлу, где будет храниться значение lastExecutionTime
            string filePath = Path.Combine(DateDirectory, mailUser);

            // Проверяем, существует ли файл
            if (File.Exists(filePath))
            {
                // Читаем значение lastExecutionTime из файла
                string dateString = File.ReadAllText(filePath);
                if (DateTime.TryParse(dateString, out DateTime lastExecutionTime))
                {
                    return lastExecutionTime;
                }
            }

            // Значение lastExecutionTime по умолчанию при первом запуске
            return DateTime.Now.AddDays(-1).Date;
        }


        public static FileStorage Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            // Инициализация экземпляра, если он еще не создан
                            instance = new FileStorage();
                        }
                    }
                }

                return instance;
            }
        }

        public void StoreFiles(List<(string, byte[])> files)
        {
            try
            {
                // Сохраняем файлы во временную директорию
                foreach (var file in files)
                {
                    string fileName = file.Item1;
                    byte[] fileData = file.Item2;
                    StoreTempFile(fileData, fileName);
                }

            }
            catch (Exception ex)
            {
                foreach (var file in files)
                {
                    DeleteFromTemp(file.Item1);
                }
                throw new Exception("Error from save: " + ex.Message);
            }
            try
            {
                foreach (var file in files)
                {
                    string fileName = file.Item1;
                    MoveFileFromTemp(fileName);
                }
            }
            catch (Exception ex)
            {
                foreach (var file in files)
                {
                    DeleteFromMain(file.Item1);
                    DeleteFromTemp(file.Item1);
                }
                throw new Exception("Error from move: " + ex.Message);
            }


        }

        public void SaveToFile<T>(T obj, string fileName)
        {
            string filePath = Path.Combine(DocumentsDirectory, fileName);
            SerializeToXmlFile(obj, filePath);
        }

        public T ReadFromFile<T>(string fileName)
        {
            string filePath = Path.Combine(DocumentsDirectory, fileName);
            return DeserializeFromXmlFile<T>(filePath);
        }

        private void SerializeToXmlFile<T>(T obj, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fileStream, obj);
            }
        }
        private static T DeserializeFromXmlFile<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return (T)serializer.Deserialize(fileStream);
            }
        }


        private void MoveFileFromTemp(string fileName)
        {
            try
            {
                string sourceFilePath = Path.Combine(TempDirectory, fileName);
                string destinationFilePath = Path.Combine(DocumentsDirectory, fileName);
                File.Move(sourceFilePath, destinationFilePath);
                fileNames.Add(fileName);
            }
            catch (Exception ex)
            {
                // Обработка ошибок перемещения файла
                throw new Exception($"Ошибка при перемещении файла: {ex.Message}");
            }
        }
        private void StoreTempFile(byte[] fileData, string fileName)
        {

            string filePath = Path.Combine(TempDirectory, fileName);
            File.WriteAllBytes(filePath, fileData);
        }
        public void DeleteFromMain(string fileName)
        {
            string filePath = Path.Combine(DocumentsDirectory, fileName);
            DeleteFile(filePath);
            fileNames.Remove(fileName);
        }
        private void DeleteFromTemp(string fileName)
        {
            string filePath = Path.Combine(TempDirectory, fileName);
            DeleteFile(filePath);
        }
        private void DeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

            }
            catch (Exception ex)
            {
                // Обработка ошибок удаления файла
                Console.WriteLine($"Ошибка при удалении файла: {ex.Message}");
            }
        }
        public List<string> GetExistingFiles()
        {
            List<string> existingFiles = new List<string>();

            try
            {
                // Получаем список файлов в целевой директории
                string[] files = Directory.GetFiles(DocumentsDirectory);

                // Добавляем имена файлов в список
                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    existingFiles.Add(fileName);
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок получения списка файлов
                Console.WriteLine($"Ошибка при получении списка файлов: {ex.Message}");
            }

            return existingFiles;
        }

    }
}