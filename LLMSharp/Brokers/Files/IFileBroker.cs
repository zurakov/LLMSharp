// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

namespace LLMSharp.Brokers.Files
{
    public interface IFileBroker
    {
        Task<byte[]> ReadAllBytesAsync(string path);
        Task<string> ReadAllTextAsync(string path);
        Task WriteAllBytesAsync(string path, byte[] content);
        Task WriteAllTextAsync(string path, string content);
        Task<string[]> ReadAllLinesAsync(string path);
        bool FileExists(string path);
        bool DirectoryExists(string path);
        void CreateDirectory(string path);
        string[] GetFiles(string path, string searchPattern);
    }
}
