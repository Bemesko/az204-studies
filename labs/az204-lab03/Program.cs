using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

const string blobServiceEndpoint = "https://mediastgberb.blob.core.windows.net/";
const string storageAccountName = "mediastgberb";
const string storageAccountKey;

StorageSharedKeyCredential accountCredentials = new StorageSharedKeyCredential(storageAccountName, storageAccountKey);

BlobServiceClient serviceClient = new BlobServiceClient(new Uri(blobServiceEndpoint), accountCredentials);

AccountInfo accountInfo = await serviceClient.GetAccountInfoAsync();

await Console.Out.WriteLineAsync($"Connected to Azure Storage Account");

await Console.Out.WriteLineAsync($"Account name:\t{storageAccountName}");

await Console.Out.WriteLineAsync($"Account kind:\t{accountInfo?.AccountKind}");

await Console.Out.WriteLineAsync($"Account sku:\t{accountInfo?.SkuName}");