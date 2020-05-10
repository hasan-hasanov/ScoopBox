using ScoopBox.Entities;
using ScoopBox.Enums;
using ScoopBox.Scripts.SandboxScripts.Abstract;
using System;
using System.Collections.Generic;

namespace ScoopBox.Scripts.SandboxScripts
{
    public class MappedFoldersBuilder : IMappedFoldersBuilder
    {
        public MappedFolders Build(ScoopBoxOptions options)
        {
            MappedFolders mappedFolders = new MappedFolders
            {
                MappedFolder = new List<MappedFolder>()
            };

            mappedFolders.MappedFolder.Add(new MappedFolder()
            {
                HostFolder = options.UserFilesPath,
                ReadOnly = Enum.GetName(typeof(ReadOnly), ReadOnly.False).ToLower()
            });

            return mappedFolders;
        }
    }
}
