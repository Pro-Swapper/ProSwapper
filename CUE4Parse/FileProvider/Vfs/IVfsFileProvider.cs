﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CUE4Parse.Encryption.Aes;
using CUE4Parse.UE4.IO;
using CUE4Parse.UE4.Objects.Core.Misc;
using CUE4Parse.UE4.Vfs;

namespace CUE4Parse.FileProvider.Vfs
{
    public interface IVfsFileProvider : IFileProvider, IDisposable
    {
        public IReadOnlyCollection<IAesVfsReader> UnloadedVfs { get; }
        public IReadOnlyCollection<IAesVfsReader> MountedVfs { get; }
        
        /// <summary>
        /// Global data from global io store
        /// Will only be used if the game uses io stores (.utoc and .ucas files) 
        /// </summary>
        public IoGlobalData? GlobalData { get; }
        
        public IAesVfsReader.CustomEncryptionDelegate? CustomEncryption { get; set; }
        
        //Aes-Key Management
        public IReadOnlyDictionary<FGuid, FAesKey> Keys { get; }
        public IReadOnlyCollection<FGuid> RequiredKeys { get; }
        
        public int Mount();
        public Task<int> MountAsync();
        public int SubmitKey(FGuid guid, FAesKey key);
        public Task<int> SubmitKeyAsync(FGuid guid, FAesKey key);
        public int SubmitKeys(IEnumerable<KeyValuePair<FGuid, FAesKey>> keys);
        public Task<int> SubmitKeysAsync(IEnumerable<KeyValuePair<FGuid, FAesKey>> keys);
    }
}