using NewsApp.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NewsApp.UWP
{
    public class LocalStorage : ILocalStorage
    {
        private StorageFolder folder;
        private IReadOnlyList<StorageFile> files;

        public LocalStorage()
        {
            folder = ApplicationData.Current.LocalFolder;         
        }
        public async Task<string> Load()
        {
            files = await folder.GetFilesAsync();
            var desiredFile = files.FirstOrDefault(x => x.Name == "selectedSection.txt");
            if (desiredFile != null)
            {
                var textContent = await FileIO.ReadTextAsync(desiredFile);
                return textContent;
            }
            return null;
        }

        public async Task Save(string sectionName)
        {
            files = await folder.GetFilesAsync();
            StorageFile desiredFile = files.FirstOrDefault(x => x.Name == "selectedSection.txt");

            if (desiredFile == null)
            {
                //make the file
                desiredFile = await folder.CreateFileAsync("selectedSection.txt");
            }
            
                //write the section to the file
                await FileIO.WriteTextAsync(desiredFile, sectionName);          
            Debug.WriteLine(sectionName);
        }
    }
}
