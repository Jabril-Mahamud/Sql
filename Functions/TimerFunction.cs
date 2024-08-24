using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Sql.Interfaces;
using Newtonsoft.Json;
using Azure.Storage.Blobs;
using System.IO;

namespace Sql.Functions
{
    public class TimerFunction
    {
        private readonly ILogger<TimerFunction> _logger;
        private readonly ITtsService _ttsService;
        private readonly IBlobStorageService _blobStorageService;

        // Hardcoded text for demonstration purposes
        private const string SampleText = "This is a sample text for the timer-triggered function.";

        public TimerFunction(ILoggerFactory loggerFactory, ITtsService ttsService, IBlobStorageService blobStorageService)
        {
            _logger = loggerFactory.CreateLogger<TimerFunction>();
            _ttsService = ttsService;
            _blobStorageService = blobStorageService;
        }

        [Function("TimerFunction")]
        public async Task Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }

            try
            {
                _logger.LogInformation("Starting text-to-speech conversion.");

                // Get audio bytes from TTS service
                var audioBytes = await _ttsService.GetTextToSpeechAsync(SampleText);

                // Upload to blob storage
                string blobUrl = await _blobStorageService.UploadAudioAsync(audioBytes, "audio/mpeg");

                _logger.LogInformation($"Audio file uploaded successfully. Blob URL: {blobUrl}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing timer trigger.");
                // Handle error (e.g., retry logic, notifications, etc.)
            }
        }
    }
}
