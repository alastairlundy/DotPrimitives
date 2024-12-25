/*
    IOExtensions 
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;

namespace Resyslib.IO.Directories;

public interface IRecursiveDirectoryExplorer
{
    bool AreSubdirectoriesEmpty(string directory);

    (IEnumerable<string> files, IEnumerable<string> directories) GetRecursiveDirectoryContents(string directory);

    (IEnumerable<string> files, IEnumerable<string> directories, IEnumerable<string> emptyDirectories) GetRecursiveDirectoryContents(string directory, bool includeEmptyDirectories);

    IEnumerable<string> GetRecursiveEmptyDirectories(string directory);

    IEnumerable<string> GetFilesRecursively(string directory);
    IEnumerable<string> GetFolderRecursively(string directory);
}