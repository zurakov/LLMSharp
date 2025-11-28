// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.IO;
using System.Threading.Tasks;

namespace LLMSharp.Brokers.Files
{
    public class FileBroker : IFileBroker
    {
        public async ValueTask<byte[]> ReadAllBytesAsync(string path) =>
            await File.ReadAllBytesAsync(path);

        public async ValueTask<string> ReadAllTextAsync(string path) =>
            await File.ReadAllTextAsync(path);

        public async ValueTask WriteAllBytesAsync(string path, byte[] content) =>
            await File.WriteAllBytesAsync(path, content);

        public async ValueTask WriteAllTextAsync(string path, string content) =>
            await File.WriteAllTextAsync(path, content);

        public async ValueTask<string[]> ReadAllLinesAsync(string path) =>
            await File.ReadAllLinesAsync(path);

        public bool FileExists(string path) => File.Exists(path);

        public bool DirectoryExists(string path) => Directory.Exists(path);

        public void CreateDirectory(string path) => Directory.CreateDirectory(path);

        public string[] GetFiles(string path, string searchPattern) =>
            Directory.GetFiles(path, searchPattern);
    }
}
