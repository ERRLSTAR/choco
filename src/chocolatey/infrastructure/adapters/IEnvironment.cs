﻿// Copyright © 2011 - Present RealDimensions Software, LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
//
// You may obtain a copy of the License at
//
// 	http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace chocolatey.infrastructure.adapters
{
    using System;

    // ReSharper disable InconsistentNaming

    public interface IEnvironment
    {
        /// <summary>
        ///   Gets an <see cref="T:System.OperatingSystem" /> object that contains the current platform identifier and version number.
        /// </summary>
        /// <returns>
        ///   An <see cref="T:System.OperatingSystem" /> object.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">
        ///   This property was unable to obtain the system version.
        ///   -or-
        ///   The obtained platform identifier is not a member of <see cref="T:System.PlatformID" />.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        OperatingSystem OSVersion { get; }

        /// <summary>
        ///   Gets a value indicating whether this is running on a 64bit operating system.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the OS is a 64bit operating system; otherwise, <c>false</c>.
        /// </value>
        bool Is64BitOperatingSystem { get; }

        /// <summary>
        ///   Gets a value indicating whether the current process is running in user interactive mode.
        /// </summary>
        /// <returns>
        ///   true if the current process is running in user interactive mode; otherwise, false.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        bool UserInteractive { get; }

        /// <summary>
        ///   Gets the newline string defined for this environment.
        /// </summary>
        /// <returns>
        ///   A string containing "\r\n" for non-Unix platforms,
        ///   or
        ///   a string containing "\n" for Unix platforms.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        string NewLine { get; }

        /// <summary>
        /// Gets the environment variable.
        /// </summary>
        /// <param name="variable">The variable.</param>
        /// <returns></returns>
        string GetEnvironmentVariable(string variable);

        /// <summary>
        ///   Creates, modifies, or deletes an environment variable stored in the current process.
        /// </summary>
        /// <param name="variable">
        ///   The name of an environment variable.
        /// </param>
        /// <param name="value">
        ///   A value to assign to <paramref name="variable" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="variable" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///   <paramref name="variable" /> contains a zero-length string, an initial hexadecimal zero character (0x00), or an equal sign ("=").
        ///   -or-
        ///   The length of <paramref name="variable" /> or <paramref name="value" /> is greater than or equal to 32,767 characters.
        ///   -or-
        ///   An error occurred during the execution of this operation.
        /// </exception>
        /// <exception cref="T:System.Security.SecurityException">
        ///   The caller does not have the required permission to perform this operation.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission
        ///     class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///     version="1" Unrestricted="true" />
        /// </PermissionSet>
        void SetEnvironmentVariable(string variable, string value);
    }

    // ReSharper restore InconsistentNaming
}
