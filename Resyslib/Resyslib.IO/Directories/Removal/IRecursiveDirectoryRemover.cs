/*
    IOExtensions 
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;

namespace Resyslib.IO.Directories;

public interface IRecursiveDirectoryRemover
{
    event EventHandler<string> DirectoryDeleted; 
    event EventHandler<string> FileDeleted;
    void DeleteDirectoryRecursively(string directory, bool deleteEmptyDirectory);
    
    bool TryDeleteDirectoryRecursively(string directory, bool deleteEmptyDirectories);
}