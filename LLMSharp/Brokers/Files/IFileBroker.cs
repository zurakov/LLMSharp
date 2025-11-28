// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Threading.Tasks;

namespace LLMSharp.Brokers.Files
{
    public interface IFileBroker
    {
        ValueTask<byte[]> ReadAllBytesAsync(string path);
        ValueTask<string> ReadAllTextAsync(string path);
        ValueTask WriteAllBytesAsync(string path, byte[] content);
        ValueTask WriteAllTextAsync(string path, string content);
        ValueTask<string[]> ReadAllLinesAsync(string path);
        bool FileExists(string path);
        bool DirectoryExists(string path);
        void CreateDirectory(string path);
        string[] GetFiles(string path, string searchPattern);
    }
}
