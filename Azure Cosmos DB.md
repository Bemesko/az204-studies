## Sources
- [Preparing for AZ-204 - Develop for Azure storage (segment 2 of 5) | Microsoft Learn](https://learn.microsoft.com/en-us/shows/exam-readiness-zone/preparing-for-az-204-develop-for-azure-storage-segment-2-of-5)
- [AZ-204: Develop solutions that use Azure Cosmos DB - Training | Microsoft Learn](https://learn.microsoft.com/en-us/training/paths/az-204-develop-solutions-that-use-azure-cosmos-db/)
- [Escolher nível certo de consistência - Training | Microsoft Learn](https://learn.microsoft.com/pt-br/training/modules/explore-azure-cosmos-db/5-choose-cosmos-db-consistency-level)
- [Opções de nível de coerência - Azure Cosmos DB | Microsoft Learn](https://learn.microsoft.com/pt-br/azure/cosmos-db/consistency-levels)
## What I need to know
- What is Cosmos DB?
- When should I use cosmos db?
- What is the resource hierarchy?
- What are cosmos db accounts? When should I use it?
- What are cosmos db databases? When should I use it?
- What are cosmos containers? When should I use it?
- What are cosmos items? When should I use it?
- How can I use the sdk to interact with containers
- How can I use the sdk to interact with items?
- What are the different cosmos db apis?
- When should I use each cosmos db api?
- What are request units
- What are consistency levels?
- When should I choose each consistency level?
- What are change feed notifications?
- How do I handle deletes from a change feed?
- How can I integrate an Azure Function with change feed
- How can I use the Change Feed processor?
- How can I process changes in parallel?

## Notes from Microsoft Learn Path
### Overview
- Cosmos' Big Thing: Globally distributed nosql databases that all support reads and writes with very low latency
- Regions can be added or removed without downtime
- 99.999% availability because if one region is unavailable another will answer
- Very low latency because the database closest to the user will respond
### Resource Hierarchy
*Cosmos DB Resource Hierarchy*
![](media/Pasted%20image%2020240124075550.png)
- Accounts
	- Cosmos accounts are pretty much storage accounts but for cosmos
	- When you want new regions you add them to accounts
- Databases
	- Like namespaces that organize containers
	- Organization purposes mainly
- Containers
	- Unit of scalability
	- Containers are like tables in which they are a collection of items
	- Schema-less, so items don't need to have similar properties to be in the same container
	- Globally distributed using partition keys
	- Every item in a container is automatically indexed
	- 2 ways to setup resource provisioning
		- Dedicated: this container will have resources all to itself, backed up by the SLAs
		- Shared: sort of like an elastic pool, all containers using the shared resources will... share the resources
- Items
	- Specific implementation depends on which cosmos API is being used
	- Can be thought as rows for a table
### Consistency Levels
- Each consistency level is a choice between how consistent your databases should be, their cost, latency and throughput
- There are 5 consistency Levels: Strong, Bounded Staleness, Session, Consistent Prefix and Eventual
Some
Bombs
Should
Come
Eventually

- Strong
	- As linear as it can get
	- Everyone always sees the latest version of all the changes
	- No one sees a partial change
	- More latency because everything needs to be replicated before it is committed
- Bounded Staleness
	- Like strong, but updates don't necessarily happen in real time, although their order is preserved
	- You can set how outdaded a read can be using a maximum amount of Time or Versions (changes) that are acceptable to lag behind
	- For a single region account, the minimum value of K and T is 10 write operations or 5 seconds. For multi-region accounts the minimum value of K and T is 100,000 write operations or 300 seconds.
- Session consistency
	- Everyone sharing a session token will see the same data
	- Updates by the session will be guaranteed to be shown
- Consistent Prefix
	- Honors the order of updates
	- Related updates are always replicated together (you can see Av1, Bv1 or Av2 or Bv2 but not Av1,Bv2)
- Eventual Consistency
	- No ordering guarantee whatsoever
	- Replicas eventually converge if there aren't more writes
	- Someone can read values that are older than the values you read before
	- Use case: number of likes in a post

### Cosmos APIs
 NoSQL is the native for CosmosDB and should be the default. Other APIs exist if you already have an application that uses another type of database and you want to migrate to Cosmos

- NoSQL
	- Items are documents
	- Native to Cosmos DB, maintained by Azure and will always receive the newest features first
	- Items can be queried using SQL
- MongoDB
	- Items are also documents, in this case BSON
	- Acts like a mongoDB database
- PostgreSQL
	- Items are rows in a table
	- Data can be stored in a single node or distributed between various nodes
- Cassandra
	- Column database
- Gremlin
	- Graph database
- Table
	- Key/value database
	- Souped up version of an Azure Table Storage, you should migrate to it if Azure Tables doesn't have enough performance
### Request Units
- 1 RU is the CPU, IOPS and memory required to Read an 1KB item using its index
- All operations are measured in RUs
![](media/Pasted%20image%2020240124085644.png)
- There are 3 pricing models for Cosmos
	- Preprovisioned
		- You specify the amount of RUs per second that your application can use
		- You can manually increase or decrease the RUs available by 100 units at a time
		- There isn't downtime to change the amount of RUs provisioned
		- You get billed for the RUs provisioned
		- If more resources than provisioned are necessary, throughput is affected
		- Provisioned RUs can be specified at a container or database level
	- Serverless
		- You pay for what you use
		- Probably the most unpredictable and can cause surprises
	- Autoscale
		- Automatically scales the RUs/s of the database or container based on the usage, without downtime
		- Good for workloads with variable traffic patterns and that requre SLAs on performance and scale
---
## Notes from Exam Prep
- resource hierarchy in cosmos database
	- accounts
	- databases
	- containers
	- items
- use the sdk to perform operations in containers and items
- how to configure request units
![](media/Pasted%20image%2020240124073527.png)

- consistency levels
	- strong: all writes are replicated to all instances before commiting
	- bounded staleness: like strong, but you can configure how much time a document can be outdated (stale)
	- session: everyone with the same session token can see the same operations
	- consistent prefix: all updates will show up in the correct order, eventually
	- eventual: less consistent, primary database doesn't need to wait for replicas to commit
![](media/Pasted%20image%2020240124073642.png)
![](media/Pasted%20image%2020240124073718.png)
- Change feed notifications
	- cosmos db can stream change notifications to a change feed consumer
![](media/Pasted%20image%2020240124074114.png)
## Practice Questions

![](media/Pasted%20image%2020240123085928.png)
![](media/Pasted%20image%2020240123090011.png)
![](media/Pasted%20image%2020240124090144.png)
