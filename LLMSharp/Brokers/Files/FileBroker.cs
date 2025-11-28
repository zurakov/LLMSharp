// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

namespace LLMSharp.Brokers.Files
{
    public class FileBroker : IFileBroker
    {
        public async Task<byte[]> ReadAllBytesAsync(string path)
        {
            return await File.ReadAllBytesAsync(path);
        }

        public async Task<string> ReadAllTextAsync(string path)
        {
            return await File.ReadAllTextAsync(path);
        }

        public async Task WriteAllBytesAsync(string path, byte[] content)
        {
            await File.WriteAllBytesAsync(path, content);
        }

        public async Task WriteAllTextAsync(string path, string content)
        {
            await File.WriteAllTextAsync(path, content);
        }

        public async Task<string[]> ReadAllLinesAsync(string path)
        {
            return await File.ReadAllLinesAsync(path);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public string[] GetFiles(string path, string searchPattern)
        {
            return Directory.GetFiles(path, searchPattern);
        }
    }
}
