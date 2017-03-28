using System;
using System.IO;
using System.Threading.Tasks;
using PCLStorage;
using FileAccess = PCLStorage.FileAccess;

namespace ConsoleApplication.BinaryFormating
{
    public static class Util
    {
        internal static async Task SerializeToFileAsync<T>(string fileName, Action<Stream, T> objectWriter, T input,
            string objectName)
            where T : class
        {
            await SerializeToFileAsync(
                fileName,
                objectWriter,
                input,
                () => string.Format("Wrote {0}: {1}", objectName, input));
        }

        internal static async Task SerializeToFileAsync<T>(string fileName, Action<Stream, T> objectWriter, T input,
            Func<string> sucessMessage)
            where T : class
        {
            try
            {
                var localStorage = FileSystem.Current.LocalStorage;
                var newActivityStateFile = await localStorage.CreateFileAsync(fileName,
                    CreationCollisionOption.ReplaceExisting);

                using (var stream = await newActivityStateFile.OpenAsync(FileAccess.ReadAndWrite))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    objectWriter(stream, input);
                }
                Logger.Debug(sucessMessage());
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Failed to write to file {0} ({1})", fileName,
                    ExtractExceptionMessage(ex)));
            }
        }

        internal static async Task<T> DeserializeFromFileAsync<T>(string fileName,
            Func<Stream, T> ObjectReader,
            Func<T> defaultReturn,
            string objectName)
            where T : class
        {
            try
            {
                var localStorage = FileSystem.Current.LocalStorage;

                var file = await localStorage.GetFileAsync(fileName);

                if (file == null)
                {
                    throw new PCLStorage.Exceptions.FileNotFoundException(fileName);
                }

                T output;
                using (var stream = await file.OpenAsync(FileAccess.Read))
                {
                    output = ObjectReader(stream);
                }
                Logger.Debug(String.Format("Read {0}: {1}", objectName, output));

                // successful read
                return output;
            }
            catch (Exception ex)
            {
                if (ex.IsFileNotFound())
                {
                    Logger.Verbose(String.Format("{0} file not found", objectName));
                }
                else
                {
                    Logger.Error(String.Format("Failed to read file {0} ({1})", objectName,
                        Util.ExtractExceptionMessage(ex)));
                }
            }

            // fresh start
            return defaultReturn();
        }

        internal static bool IsFileNotFound(this Exception ex)
        {
            // check if the exception type is File Not Found (FNF)
            if (ex is PCLStorage.Exceptions.FileNotFoundException
                || ex is FileNotFoundException)
                return true;

            // if the exception is an aggregate of exceptions
            // we'll check each of them recursively if they are FNF
            var agg = ex as AggregateException;
            if (agg != null)
            {
                foreach (var innerEx in agg.InnerExceptions)
                {
                    if (innerEx.IsFileNotFound())
                        return true;
                }
            }

            // if the exception it's not FNF and doesn't have an inner exception
            // then it's not a FNF
            if (ex.InnerException == null)
                return false;

            // check recursively if the inner exception is FNF
            if (ex.InnerException.IsFileNotFound())
                return true;

            // if all checks fails, the exception must not be a FNF
            return false;
        }

        internal static string ExtractExceptionMessage(Exception e)
        {
            if (e == null)
                return "";

            return e.Message + ExtractExceptionMessage(e.InnerException);
        }
    }
}