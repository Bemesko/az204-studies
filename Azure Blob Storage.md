## Sources
- [Preparing for AZ-204 - Develop for Azure storage (segment 2 of 5) | Microsoft Learn](https://learn.microsoft.com/en-us/shows/exam-readiness-zone/preparing-for-az-204-develop-for-azure-storage-segment-2-of-5)
- [Explore Azure Blob storage - Training | Microsoft Learn](https://learn.microsoft.com/en-us/training/modules/explore-azure-blob-storage/)
- [Manage the Azure Blob storage lifecycle - Training | Microsoft Learn](https://learn.microsoft.com/en-us/training/modules/manage-azure-blob-storage-lifecycle/)
- [Aplicativos Web Estáticos do Azure – Serviço de Aplicativo | Microsoft Azure](https://azure.microsoft.com/pt-br/products/app-service/static/)
- [Map a custom domain to an Azure Blob Storage endpoint - Azure Storage | Microsoft Learn](https://learn.microsoft.com/en-us/azure/storage/blobs/storage-custom-domain-name?tabs=azure-portal)
## Key Questions
- What is Azure Blob Storage?
- How does pricing work?
- How can I get properties using REST?
- What's the difference between getting properties from blobs vs containers
- What common properties can I get using REST?
- What is the Blob Client Library?
- What are the main 5 objects in the Blob Client Library? How to I use each one?
- What are the access tiers in blob storage? When should I use each one?
- What is a lifecycle management policy? How do I use it?
- What do I do if I need to create a policy based on the last time a blob was accessed?
- What are the main properties of policies?
- What are filter sets?
- What are action sets?
- What is static site hosting?
- How can I configure static site hosting?
- How does pricing work with blob storage?
- What are the 3 types of Blob and what are the use cases for each one?
- What's the difference between user managed and user provided encryption keys?
- What are the pricing considerations for static site hosting?
- How do I rehydrate data from the access tier?
- What is AzCopy? When should I use it?
### My Answers

---
## Learning Path Notes - Explore Azure Storage
- blob storage is the Azure thing for unstructured data access
- can be accessed via the REST API, Client Libraries, Azure CLI, Powershell and the Portal
- use cases
	- serving images to a browser
	- distributed access to files
	- video streaming
	- log archiving
	- data warehousing
	- backups
- blob types
	- standard
		- general purpose v2: the option for most scenarios
	- premium
		- block: support for block or append blobs with SSD; high transaction rates and consistently low latency
		- page: support for page blocks
- access tiers
	- hot: data accessed frequently, storage costs more
	- cool: data accessed less frequently, storage costs less, storage for at least 30 days
	- cold: data access infrequently, storage costs way less, stored for at least 90 days
	- archive: data rarely accessed, storage costs the least, stored for at least 180 days and needs to be rehydrated to be accesed
### Storage Resources
- account: globally unique namespace to store all data
- container: organizes a set of common blobs, like a directory in a file system
- blob
	- block blob: regular unstructured data
	- append blob: data optimized to be appended on (logs)
	- page blob: random data that is stored as VHD files and can be mounted as disks for VMs
### Security Features
- RBAC roles can be assigned to blobs for resource management and data operations
- Delegated access to objects using [Shared Access Signatures](Shared%20Access%20Signatures)
- SSE (Storage Service Encryption) is always enabled for all blobs and doesn't cost anything extra
- You can control encryption by deciding how the encryption keys should be managed:
	- Microsoft-Managed: Choose this if you don't bother
	- User Managed: Users manage the encryption key and are responsible for rotating it
	- User Provided: More control over the key; Only Blob supports it
### Static Sites
- Can be enabled pretty easily and don't increase costs (?)
- Files need to be in the $web container and are always publicly accessible
- Changing the access level of blobs doesn't interfere with the site, only with direct access to the files themselves
- You can set a default index.html and error.html page for the site
- Custom domains can be setup using HTTP. HTTPS requires Azure CDN to be enabled
- Custom headers can be setup using Azure CDN
- Authorization and authentication aren't supported
- For more complex static sites it is better to use Azure Static Web Apps

## Learning Path Notes - Manage the Storage Lifecycle
- access tiers
	- hot
	- cool
	- cold
	- archive: data is stored offline
- data storage has a set limit per account that can be distributed between the 4 tiers
- the archive tier can only be set at the blob level, the rest can be either at the blob or the entire account
### Lifecycle Policies
- rules: up to 100 rules per account
	- name
	- enabled
	- type: `Lifecycle` always
	- definition
		- filter sets: if there are multiple filters, they are applied using a logical AND
			- blobTypes (required): block blob, page blob, append blob
			- prefixMatch
			- blobIndexMatch
		- action sets: can be separate for base blobs, snapshots and versions
			- tierToCool
			- tierToArchive
			- delete
			- enableAutoTierToHotFromCool
- Conditions
	- daysAfterModificationGreaterThan
	- daysAfterCreationGreaterThan
	- daysAfterLastAccessTimeGreaterThan
	- daysAfterLastTierChangeGreaterThan

---
## Exam Prep notes
![](media/Pasted%20image%2020240202080229.png)
- how to set and get metadata using REST
	- how to format uris for different requests
	- format uris that target containers
	- common properties (e-tag, last modified)
	
![](media/Pasted%20image%2020240202080309.png)
- blob storage client library
- how to perform operations in blobs using the library

![](media/Pasted%20image%2020240202080335.png)
- blob storage lifecycle
- differences between hot cool and sarchive

![](media/Pasted%20image%2020240202080343.png)
- automating the lifecycle of blob storage, lifecycle management policy
- enable last access time tracking

![](media/Pasted%20image%2020240202080403.png)
![](media/Pasted%20image%2020240202080430.png)
- how to setup policies
	- name
	- enabled
	- type
	- definition
		- filter sets
		- action sets

![](media/Pasted%20image%2020240202080448.png)
- implement static site hosting
	- know process and settings
	- everything is stored in $web container