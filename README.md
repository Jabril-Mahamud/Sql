# ElevenLabs TTS in C#

This repository contains a series of Azure Functions that handle text-to-speech (TTS) processing using the ElevenLabs service. The functions convert text to speech and store the resulting audio files in Azure Blob Storage.

## Functions 
HTTP Trigger Function

    Trigger: HTTP POST requests
    Function: Receives a JSON payload with text, converts the text to speech using the ElevenLabs TTS service, and uploads the resulting audio file to Azure Blob Storage.
    Endpoint: Secured with function-level authorization. Use the function key for access.
    Output: Returns the URL of the uploaded audio file.

Queue Trigger Function

    Trigger: Azure Storage Queue (queue-items)
    Function: Processes text messages from the queue, converts the text to speech using the ElevenLabs TTS service, and uploads the resulting audio file to Azure Blob Storage.
    Output: Logs the URL of the uploaded audio file. Messages that fail processing are moved to a poison queue.

Timer Trigger Function

    Trigger: Timer-based trigger (runs every 5 minutes)
    Function: Converts a hardcoded sample text to speech using the ElevenLabs TTS service and uploads the resulting audio file to Azure Blob Storage.
    Output: Logs the URL of the uploaded audio file. Handles errors and logs them.

QueueMessageProcessor

    Purpose: Contains logic for processing queue messages, interacting with the ElevenLabs TTS service, and uploading audio files to Azure Blob Storage.
    Dependencies:
        ITtsService: Interface for ElevenLabs' text-to-speech service.
        IBlobStorageService: Interface for Azure Blob Storage operations.

Setup
Prerequisites

    Azure Functions Core Tools: Install to run and debug Azure Functions locally.
    Azure Storage Account: Set up for queue and blob storage.
    ElevenLabs API Access: Obtain credentials for the ElevenLabs text-to-speech service.

## Configuration

Configure your connection strings and API keys in local.settings.json for local development:



    {
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "QString": "<your-storage-connection-string>",
        "TtsServiceApiKey": "<your-elevenlabs-api-key>",
        "TtsServiceEndpoint": "<your-elevenlabs-endpoint>"
        }
    }

## Dependencies

Install the necessary NuGet packages:

    bash

    dotnet add package Azure.Storage.Blobs
    dotnet add package Azure.Storage.Queues
    dotnet add package Newtonsoft.Json
    dotnet add package Microsoft.Azure.Functions.Worker

## Usage

HTTP Trigger

    Endpoint: https://<your-function-app>.azurewebsites.net/api/HttpFunction
    Method: POST
    Request Body:


    {
    "Text": "This is a sample text."
    }

    Response: Returns a JSON object with the URL of the uploaded audio file.

Queue Trigger

    Queue Name: queue-items
    Message Format:


    {
    "Text": "This is a sample text."
    }

    Output: Logs the URL of the uploaded audio file. Failed messages are moved to a poison queue (queue-items-poison).

Timer Trigger

    Trigger: Runs every 5 minutes
    Function: Converts a hardcoded sample text to speech and uploads the audio file to Azure Blob Storage.
    Output: Logs the URL of the uploaded audio file and handles errors.

Error Handling

    HTTP Trigger: Returns appropriate HTTP status codes and messages for errors such as missing or invalid text.
    Queue Trigger: Logs errors and moves messages to a poison queue if they fail to process after multiple attempts.
    Timer Trigger: Logs errors encountered during processing.

Development and Testing

    Local Testing: Use Azure Functions Core Tools to run and debug functions locally.
    Unit Testing: Implement unit tests for the QueueMessageProcessor and TTS service integration.
    Integration Testing: Test the full workflow with HTTP, queue, and timer triggers to ensure end-to-end functionality.

Contributing

    Fork the Repository: Create a fork to make your changes.
    Create a Branch: Create a new branch for your changes.
    Submit a Pull Request: Submit a pull request for review.

License

This project is licensed under the MIT License. See the LICENSE file for details.
Contact

For issues or questions, please contact not me :).
just kidding, feel free to contact me.