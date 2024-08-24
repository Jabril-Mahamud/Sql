using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IBlobStorageService
{
    Task<string> UploadAudioAsync(byte[] audioData, string contentType);
}