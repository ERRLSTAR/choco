﻿namespace chocolatey.infrastructure.filesystem
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    ///   File System Interface
    /// </summary>
    public interface IFileSystem
    {
        #region Path

        /// <summary>
        ///   Combines strings into a path.
        /// </summary>
        /// <param name="leftItem">The first path to combine. </param>
        /// <param name="rightItems">string array of all other paths to combine.</param>
        /// <returns>The combined paths.</returns>
        string combine_paths(string leftItem, params string[] rightItems);

        /// <summary>
        ///   Gets the full path.
        /// </summary>
        /// <param name="path">The file or directory for which to obtain absolute path information.</param>
        /// <returns>The fully qualified location of path, such as "C:\MyFile.txt".</returns>
        string get_full_path(string path);

        #endregion

        #region File

        /// <summary>
        ///   Gets a list of files inside of an existing directory, optionally by pattern and recursive search option.
        /// </summary>
        /// <param name="directoryPath">The path to a specified directory.</param>
        /// <param name="pattern">The search pattern.</param>
        /// <param name="option">The option specifies whether the search operation should include all subdirectories or only the current directory.</param>
        /// <returns>Returns the names of files (including their paths).</returns>
        IList<string> get_files(string directoryPath, string pattern = "*.*", SearchOption option = SearchOption.TopDirectoryOnly);

        /// <summary>
        ///   Does the file exist?
        /// </summary>
        /// <param name="filePath">The file to check.</param>
        /// <returns>Boolean - true if the caller has the required permissions and path contains the name of an existing file; otherwise, false.</returns>
        bool file_exists(string filePath);

        /// <summary>
        ///   Returns the file name and extension of the specified path string.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The characters after the last directory character in path. If the last character of path is a directory or volume separator character, this method returns String.Empty. If path is Nothing, this method returns Nothing.</returns>
        string get_file_name(string filePath);

        /// <summary>
        ///   Gets the file name without extension.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The string returned by get_file_name, minus the last period (.) and all characters following it.</returns>
        string get_file_name_without_extension(string filePath);

        /// <summary>
        ///   Gets the extension.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>he extension of the specified path (including the period "."), or Nothing, or String.Empty. If path is Nothing, get_file_extension returns Nothing. If path does not have extension information, get_file_extension returns String.Empty.</returns>
        string get_file_extension(string filePath);

        /// <summary>
        ///   Determines the file information given a path to an existing file
        /// </summary>
        /// <param name="filePath">Path to an existing file</param>
        /// <returns>FileInfo object</returns>
        FileInfo get_file_info_for(string filePath);

        /// <summary>
        ///   Gets the file mod date.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>the modification date of the specified file.</returns>
        DateTime get_file_modified_date(string filePath);

        /// <summary>
        ///   Gets the size of the file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The size, in bytes, of the current file.</returns>
        long get_file_size(string filePath);

        /// <summary>
        ///   Determines the FileVersion of the file passed in
        /// </summary>
        /// <param name="filePath">Relative or full path to a file</param>
        /// <returns>A string representing the FileVersion of the passed in file</returns>
        string get_file_version_for(string filePath);

        /// <summary>
        ///   Determines if a file is a system file
        /// </summary>
        /// <param name="file">File to check</param>
        /// <returns>True if the file has the System attribute marked, otherwise false</returns>
        bool is_system_file(FileInfo file);

        /// <summary>
        ///   Determines if a file is encrypted or not
        /// </summary>
        /// <param name="file">File to check</param>
        /// <returns>True if the file has the Encrypted attribute marked, otherwise false</returns>
        bool is_encrypted_file(FileInfo file);

        /// <summary>
        ///   Determines the older of the file dates, Creation Date or Modified Date
        /// </summary>
        /// <param name="file">File to analyze</param>
        /// <returns>The oldest date on the file</returns>
        string get_file_date(FileInfo file);

        /// <summary>
        ///   Moves a specified file to a new location, providing the option to specify a new file name.
        /// </summary>
        /// <param name="filePath">The name of the file to move. </param>
        /// <param name="newFilePath">The new path for the file. </param>
        void move_file(string filePath, string newFilePath);

        /// <summary>
        ///   Copies an existing file to a new file. Overwriting a file of the same name is allowed.
        /// </summary>
        /// <param name="sourceFilePath">The source file path. Teh File to copy.</param>
        /// <param name="destFilePath">The dest file path.</param>
        /// <param name="overWriteExisting">true if the destination file can be overwritten; otherwise, false.</param>
        void copy_file(string sourceFilePath, string destFilePath, bool overWriteExisting);

        /// <summary>
        ///   Copies a file from one directory to another using FFI
        /// </summary>
        /// <param name="sourceFileName">Where is the file now?</param>
        /// <param name="destinationFileName">Where would you like it to go?</param>
        /// <param name="overwriteTheExistingFile">If there is an existing file already there, would you like to delete it?</param>
        void copy_file_unsafe(string sourceFileName, string destinationFileName, bool overwriteTheExistingFile);

        /// <summary>
        ///   Deletes the specified file.
        /// </summary>
        /// <param name="filePath">The name of the file to be deleted. Wildcard characters are not supported.</param>
        void delete_file(string filePath);

        /// <summary>
        ///   Creates a file
        /// </summary>
        /// <param name="filePath">Path to the file name</param>
        /// <returns>A file stream object for use after creating the file</returns>
        FileStream create_file(string filePath);

        /// <summary>
        ///   Returns the contents of a file
        /// </summary>
        /// <param name="filePath">Path to the file name</param>
        /// <returns>A string of the file contents</returns>
        string read_file(string filePath);

        /// <summary>
        ///   Opens a file
        /// </summary>
        /// <param name="filePath">Path to the file name</param>
        /// <returns>A file stream object for use after accessing the file</returns>
        FileStream open_file_readonly(string filePath);

        /// <summary>
        ///   Writes the file text to the specified path
        /// </summary>
        /// <param name="filePath">The file path</param>
        /// <param name="fileText">The file text</param>
        void write_file(string filePath, string fileText);

        /// <summary>
        ///   Writes the file text to the specified path
        /// </summary>
        /// <param name="filePath">The file path</param>
        /// <param name="fileText">The file text</param>
        /// <param name="encoding">The encoding</param>
        void write_file(string filePath, string fileText, Encoding encoding);

        /// <summary>
        /// Writes a stream to a specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="getStream">A defferred function of getting the stream</param>
        void write_file(string filePath, Func<Stream> getStream);

        #endregion

        #region Directory

        /// <summary>
        ///   Gets the names of subdirectories (including their paths) in the specified directory.
        /// </summary>
        /// <param name="directoryPath">The path for which an array of subdirectory names is returned. </param>
        /// <returns>An array of the names of subdirectories in "directory".</returns>
        IList<string> get_directories(string directoryPath);

        /// <summary>
        ///   Determines whether the given path refers to an existing directory on disk.
        /// </summary>
        /// <param name="directoryPath">The path to test.</param>
        /// <returns>True if path refers to an existing directory; otherwise, false.</returns>
        bool directory_exists(string directoryPath);

        /// <summary>
        ///   Gets the name of the directory.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>Directory information for path, or Nothing if path denotes a root directory or is null. Returns String.Empty if path does not contain directory information.</returns>
        string get_directory_name(string filePath);

        /// <summary>
        ///   Returns a DirectoryInfo object from a string
        /// </summary>
        /// <param name="directoryPath">Full path to the directory you want the directory information for</param>
        /// <returns>DirectoryInfo object</returns>
        DirectoryInfo get_directory_info_for(string directoryPath);

        /// <summary>
        ///   Returns a DirectoryInfo object from a string to a filepath
        /// </summary>
        /// <param name="filePath">Full path to the file you want directory information for</param>
        /// <returns>DirectoryInfo object</returns>
        DirectoryInfo get_directory_info_from_file_path(string filePath);

        /// <summary>
        ///   Creates all directories and subdirectories in the specified path.
        /// </summary>
        /// <param name="directoryPath">The directory path to create. </param>
        void create_directory(string directoryPath);

        /// <summary>
        ///   Creates all directories and subdirectories in the specified path if they have not already been created.
        /// </summary>
        /// <param name="directoryPath">The directory path to create. </param>
        void create_directory_if_not_exists(string directoryPath);

        /// <summary>
        ///   Deletes a directory
        /// </summary>
        /// <param name="directoryPath">Path to the directory</param>
        /// <param name="recursive">Would you like to delete the directories inside of this directory? Almost always true.</param>
        void delete_directory(string directoryPath, bool recursive);

        #endregion
    }
}