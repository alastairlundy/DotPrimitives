/*
    Resyslib
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


// ReSharper disable InconsistentNaming
namespace AlastairLundy.Primitives.Text;

/// <summary>
/// 
/// </summary>
public enum LineEndingFormat
{
    /// <summary>
    /// 
    /// </summary>
    CR,
    /// <summary>
    /// 
    /// </summary>
    LF,
    /// <summary>
    /// 
    /// </summary>
    CR_LF,
    /// <summary>
    /// 
    /// </summary>
    LF_CR,
    /// <summary>
    /// 
    /// </summary>
    NotDetected
}