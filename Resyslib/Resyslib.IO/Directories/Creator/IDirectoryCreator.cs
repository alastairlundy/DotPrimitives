/*
    IOExtensions
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.IO;

namespace Resyslib.IO.Directories;

public interface IDirectoryCreator
{
    public bool TryCreateDirectory(string directoryPath,string newDirectoryName, UnixFileMode unixFileMode, bool createParentPaths);
    public void CreateDirectory(string directoryPath, string newDirectoryName, UnixFileMode unixFileMode, bool createParentPaths);
    public bool TryCreateParentDirectory(string directoryPath, UnixFileMode unixFileMode);
    public void CreateParentDirectory(string parentDirectory, UnixFileMode unixFileMode);
}